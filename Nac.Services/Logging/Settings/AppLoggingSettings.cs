namespace Nac.Services.Logging.Settings;

public class AppLoggingSettings
{
    public GeneralSettings General { get; set; } = new();
    public FileSettings File { get; set; } = new();
    public DbServerSettings DbServer { get; set; } = new();

    public class GeneralSettings
    {
        public string RestrictedToMinimumLevel { get; set; } = string.Empty;
    }
    public class DbServerSettings
    {
        public string TableName { get; set; } = string.Empty;
        public string Schema { get; set; } = string.Empty;
        public string ConnectionStringName { get; set; } = string.Empty;
    }

    public class FileSettings
    {
        public string Drive { get; set; } = string.Empty;
        public string FileBasePath { get; set; } = string.Empty;
        public string FileSubPath { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string SubPathAndFileName =>
            $"{FileSubPath}{Path.DirectorySeparatorChar}{FileName}";
        public string FullLogPathAndFileName =>
            $"{Drive}{Path.VolumeSeparatorChar}{Path.DirectorySeparatorChar}{FileBasePath}{Path.DirectorySeparatorChar}{FileSubPath}{Path.DirectorySeparatorChar}{FileName}";
    }

}
