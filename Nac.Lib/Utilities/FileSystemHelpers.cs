using Nac.Lib.Exceptions;
using System.ComponentModel;

namespace Nac.Lib.Utilities;

public class MountedDriveInfo
{
    [DisplayName("Drive Name")]
    public string Name { get; set; } = string.Empty;
    public long TotalFreeSpace { get; set; } = 0;
    [DisplayName("Free Disk Space")]
    public double FreeSpacePercent { get; set; }
}

public static class FileSystemHelpers
{
    public static bool Exists(FileSystemInfo pathFileName)
    {
        // after several tests: either you need to catch a exceptions or you
        // will not get a clear result (even Exists()). In .NET7 Path.Exists() may work.
        // But the strange Attributes value -1 seems to work.
        return pathFileName.Attributes != (FileAttributes)(-1);
    }

    public static bool IsDirectoryWithAccess(FileSystemInfo pathFileName)
    {
        if (!Exists(pathFileName))
        {
            return false;
        }
        return pathFileName.Attributes.HasFlag(FileAttributes.Directory);
    }

    public static bool IsFileWithAccess(FileSystemInfo pathFileName)
    {
        if (!Exists(pathFileName))
        {
            return false;
        }
        return !pathFileName.Attributes.HasFlag(FileAttributes.Directory);
    }

    public static IEnumerable<string> ListFileNamesRecursively(DirectoryInfo source)
    {
        return Directory.EnumerateFiles(source.FullName, "*", SearchOption.AllDirectories)
                        .Select(n => Path.GetRelativePath(source.FullName, n).Replace('\\', '/'));
    }

    public static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
    {
        if (source.FullName.ToLower() == target.FullName.ToLower())
        {
            // nothing to be done here
            return;
        }

        bool isNewDirectory = false;
        // Check if the target directory exists, if not, create it.
        if (!Directory.Exists(target.FullName))
        {
            Directory.CreateDirectory(target.FullName);
            isNewDirectory = true;
        }

        try
        {
            // Copy each file into it's new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyFilesRecursively(diSourceSubDir, nextTargetSubDir);
            }
        }
        catch (Exception)
        {
            if (isNewDirectory)
            {
                // is it too risky, to automatically remove the new directory again?
                Directory.Delete(target.FullName, true);
            }
            throw;
        }
    }

    public static void MoveFilesRecursively(DirectoryInfo source, DirectoryInfo target)
    {
        if (source.FullName.ToLower() == target.FullName.ToLower())
        {
            // nothing to be done here
            return;
        }

        if (Directory.Exists(target.FullName))
        {
            throw new TargetAlreadyExistsException($"Destination already exists: '{target.FullName}'");
        }
        if (!FileSystemHelpers.Exists(source))
        {
            throw new SourceMissingException($"Source folder missing: '{source.FullName}'");
        }
        if (!FileSystemHelpers.IsDirectoryWithAccess(source))
        {
            throw new SourceInvalidException($"Source has to be a readable folder, but is not: '{source.FullName}'");
        }

        bool parentIsNewDirectory = false;
        // Check if the target directory exists, if not, create it.
        var targetParent = target.Parent!;
        if (!Directory.Exists(targetParent.FullName))
        {
            Directory.CreateDirectory(targetParent.FullName);
            parentIsNewDirectory = true;
        }

        try
        {
            Directory.Move(source.FullName, target.FullName);
        }
        catch (Exception)
        {
            if (parentIsNewDirectory)
            {
                // is it too risky, to automatically remove the new directory again?
                Directory.Delete(targetParent.FullName, true);
            }
            throw;
        }
    }

    public static void DeleteFilesRecursively(DirectoryInfo source, int cleanupNumberOfParentIfEmpty = 0)
    {
        // Check if the directory exists, if not, we are done.
        if (Directory.Exists(source.FullName) == false)
        {
            return;
        }

        Directory.Delete(source.FullName, true);
        if (cleanupNumberOfParentIfEmpty > 0)
        {
            var parent = source.Parent!;
            for (int i = 0; i < cleanupNumberOfParentIfEmpty; i++)
            {
                if (!parent.EnumerateFileSystemInfos().Any())
                {
                    Directory.Delete(parent.FullName, false);
                    parent = parent.Parent!;
                }
                else
                {
                    break;
                }
            }
        }
    }

    public static DriveInfo GetDrive(string driveName)
    {
        var drive = new DriveInfo(driveName);
        return drive;
    }

    public static List<MountedDriveInfo> GetMountedDriveInfos(IEnumerable<string> driveNames)
    {
        List<MountedDriveInfo> mounteddriveList = new();

        foreach (string driveName in driveNames)
        {
            var drive = GetDrive(driveName);
            var freeSpace = drive.TotalFreeSpace;
            var totalSpace = drive.TotalSize;
            double freeSpacePerc = (float)(((float)freeSpace / (float)totalSpace) * 100);
            var mount = new MountedDriveInfo()
            {
                Name = drive.Name,
                TotalFreeSpace = drive.TotalFreeSpace,
                FreeSpacePercent = freeSpacePerc
            };

            mounteddriveList.Add(mount);
        }

        return mounteddriveList;
    }
}

