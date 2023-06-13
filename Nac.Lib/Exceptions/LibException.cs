namespace Nac.Lib.Exceptions;

public class LibException : ApplicationException
{
    public LibException() { }
    public LibException(string message) : base(message) { }
    public LibException(string message, Exception innerException)
    : base(message, innerException) { }
}
