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

5. ??? Update all collections with (takes ~10 min) or just update the required collections.

```bash
for module in $(ansible-galaxy collection list | grep -E '[a-z._0-9]+ +[0-9]' | awk '{print $1}') ; do ansible-galaxy collection install -U $module ; done
```

# Deployment

1. Configure your favorite editor:

```bash
% export EDITOR=vi
```

2. Create an ansible vault which stores your sudo password:

```bash
% ansible-vault create jaekel-private-vault.yaml
```

Set the password for the vault and store set the file content like this:

```
cluster_sudo_pass: <your sudo password>
```

3. Run the playbook for deployment

```bash
ANSIBLE_CONFIG=./ansible.cfg ansible-playbook playbook.yml
```

You have to enter two passwords: one for the common vault and another password
for your personal vault you created.

# Command list

This is a list of potential helpful commands.

- Show an ansible vault content:

```bash
ansible-vault view $BASE_PATH/${USER_NAME}-private-vault.yaml
ansible-vault view $BASE_PATH/common-vault.yaml
```

- Modify an ansible vault content:

```bash
ansible-vault edit $BASE_PATH/${USER_NAME}-private-vault.yaml
ansible-vault edit $BASE_PATH/common-vault.yaml
```

- Encrypt an ansible vault with new password:

```bash
ansible-vault rekey $BASE_PATH/${USER_NAME}-private-vault.yaml
ansible-vault rekey $BASE_PATH/common-vault.yaml
```

- Create new encryption key/iv pair for appsettings encryption

```bash
% head -c 32 /dev/urandom | base64
% head -c 16 /dev/urandom | base64
```

- Do Basic auth encoding

```bash
% echo -n 'rrd-onboarding@gaf.de:blah}S$4++blah}S$4++'|base64
```

- View the base64 content

```bash
% echo 'bIZS01NpCD9ZJXzL6hRwi/WsSdc7vC6ch1d3MlFnN9g=' | base64 -d | hexdump -C
```

- Create Ansible common vault password  
  Generate with Password Depot -> Lowercase, Numbers, Uppercase, Length: 32, exclude characters: oO0LlI1,´`'"
