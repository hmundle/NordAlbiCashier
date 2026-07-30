# SSH configuration

Generate a SSH key with the name `id_ed25519_ansible_nac`:

```bash
ssh-keygen -t ed25519 -C "wsl-ansible-key"
```

Put the private key file in your `$HOME/.ssh/` directory.

Extend your ssh configuration in `$HOME/.ssh/config` as follows:

```
Host *
    ServerAliveInterval 60

###
# NAC environment
###

Host nordalbi02
     HostName nordalbi02
     User nordalbi
     IdentitiesOnly yes
     IdentityFile ~/.ssh/id_ed25519_ansible_nac

Host nordalbi03
     HostName nordalbi03
     User nordalbi
     IdentitiesOnly yes
     IdentityFile ~/.ssh/id_ed25519_ansible_nac

```

4. Remove the access rights for the group and for others from config and private key file, e.g. with  
   `chmod go-rwx $HOME/.ssh/config $HOME/.ssh/id_v2*`
5. Test your ssh configuration. You should be able to login to the
   servers without any interaction (after confirming the fingerprints):
   Development environment

```bash
$ ssh nordalbi03
Welcome to Ubuntu 26.04 LTS (GNU/Linux 7.0.0-28-generic x86_64)
...
Last login: Wed Jul 22 21:33:20 2026 from 192.168.178.36
nordalbi@nordalbi03:~$
```

6. Copy the SSH key to target host

```bash
ssh-copy-id -i ~/.ssh/id_ed25519_ansible_nac.pub nordalbi03
```

7. avoid using sudo password for user nordalbi

Unfortunately I failed to setup ansible to use a sudo password, therefore this workaround to do `sudo` without password.

```bash
sudo visudo
```

and attach to the end:

```
nordalbi ALL=(ALL) NOPASSWD: ALL
```

# Ansible client installation on Windows

(from AI):
Start WSL ubuntu and then

```bash
# Update packages and install pipx
sudo apt update && sudo apt install -y pipx

# Ensure pipx path is active
pipx ensurepath

# Install Ansible CLI
pipx install --include-deps ansible
```

4. test ansible

```bash
ANSIBLE_CONFIG=./ansible.cfg ansible nac_servers -m ping
```

# Deployment

1. Configure your favorite editor:

```bash
% export EDITOR=vi
```

3. Run the playbook for deployment

```bash
ANSIBLE_CONFIG=./ansible.cfg ansible-playbook playbook.yml
```

4. Run the playbook for cleanup

```bash
ANSIBLE_CONFIG=./ansible.cfg ansible-playbook cleanup-playbook.yml
```

# Restoring the NAC Database Backup on Host 2

This runbook restores the latest `pg_dump` backup (taken every 2 minutes from
host 1) into host 2's local `nac_db` container. Use this when host 1 has
failed and host 2 needs to take over with the most recent available data.

**Data loss window:** up to 2 minutes (the backup interval) plus however long
host 1 was down before the last successful backup.

---

## 1. Stop the app on host 2 (avoid writes during restore)

```bash
sudo systemctl stop nac.service
```

## 2. Pick the backup file to restore

```bash
ls -lt /var/backups/nac_db/nac_db_*.dump | head -5
```

Choose the newest file (or an older one if the newest is suspected corrupt).
Note the full filename, e.g. `nac_db_20260730211801.dump`.

## 3. Make sure `nac_db` is running

```bash
sudo systemctl status nac_db.service
# if not running:
sudo systemctl start nac_db.service
```

## 4. Copy the dump into the container and restore

`pg_restore` runs inside the `nac_db` container, so the dump file needs to be
reachable there. `podman cp` copies it in without needing a bind mount.

```bash
BACKUP_FILE=/var/backups/nac_db/nac_db_20260730211801.dump   # <-- adjust

podman cp "$BACKUP_FILE" nac_db:/tmp/restore.dump

podman exec nac_db pg_restore \
  -U postgres \
  -d NacDB \
  --clean \
  --if-exists \
  --no-owner \
  --verbose \
  /tmp/restore.dump
```

- `--clean --if-exists` drops existing objects before recreating them, so the
  restore fully replaces whatever is currently in `NacDB` (safe even if
  host 2's DB already has stale/partial data).
- `--no-owner` avoids errors if role names ever differ between hosts.
- Expect some warnings in the output (e.g. about the `postgres` role already
  existing) — these are normal with `--clean` and can be ignored as long as
  the command finishes without fatal errors.

## 5. Clean up the temp file in the container

```bash
podman exec nac_db rm -f /tmp/restore.dump
```

## 6. Verify the data

```bash
podman exec nac_db psql -U postgres -d NacDB -c "\dt"
podman exec nac_db psql -U postgres -d NacDB -c "SELECT count(*) FROM <a_known_table>;"
```

Compare row counts / recent records against what you'd expect from host 1
before it failed.

## 7. Start the app on host 2

```bash
sudo systemctl start nac.service
sudo systemctl status nac.service
```

## 8. Redirect clients to host 2

Update DNS / load balancer / reverse proxy so the app URL clients use points
to host 2.

---

## Notes

- This restores into the **existing** `NacDB` database inside the running
  `nac_db` container — it does not recreate the container or volume.
- If `nac_db`'s volume is corrupted or missing entirely, first let the
  playbook (or a manual `podman_container`/quadlet start) recreate the
  container so Postgres initializes a fresh `NacDB`, _then_ run this runbook.
- Once host 1 is repaired, decide whether to keep host 2 as the primary going
  forward (update `db_role` in `hosts.ini` and re-run the playbook so the
  backup cron job direction flips) or fail back to host 1 after resyncing it
  from a fresh dump of host 2.
