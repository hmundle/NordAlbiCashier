namespace Nac.Lib.Exceptions;

public class LibCommonException : LibException
{
    public LibCommonException() { }
    public LibCommonException(string message)
    : base(message) { }
    public LibCommonException(
    string message, Exception innerException)
    : base(message, innerException) { }
}

public class FtpUploadException : LibException
{
    public string StdOut { get; init; }
    public string StdErr { get; init; }
    public int ReturnCode { get; init; }
    public FtpUploadException(string stdOut, string stdErr, int returnCode)
    : base($"{stdErr} - lftp error code: {returnCode}")
    {
        StdOut = stdOut;
        StdErr = stdErr;
        ReturnCode = returnCode;
    }
    public FtpUploadException(
    string stdOut, string stdErr, int returnCode, Exception innerException)
    : base(stdErr, innerException)
    {
        StdOut = stdOut;
        StdErr = stdErr;
        ReturnCode = returnCode;
    }
}

public class TargetAlreadyExistsException : LibException
{
    public TargetAlreadyExistsException() { }
    public TargetAlreadyExistsException(string message)
    : base(message) { }
    public TargetAlreadyExistsException(
    string message, Exception innerException)
    : base(message, innerException) { }
}

public class ZipFileException : LibException
{
    public ZipFileException() { }
    public ZipFileException(string message)
    : base(message) { }
    public ZipFileException(
    string message, Exception innerException)
    : base(message, innerException) { }
}

public class SourceMissingException : LibException
{
    public SourceMissingException() { }
    public SourceMissingException(string message)
    : base(message) { }
    public SourceMissingException(
    string message, Exception innerException)
    : base(message, innerException) { }
}

public class SourceInvalidException : LibException
{
    public SourceInvalidException() { }
    public SourceInvalidException(string message)
    : base(message) { }
    public SourceInvalidException(
    string message, Exception innerException)
    : base(message, innerException) { }
}