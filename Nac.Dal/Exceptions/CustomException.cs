namespace Nac.Dal.Exceptions;

public class CustomException : ApplicationException
{
    public CustomException() { }
    public CustomException(string message) : base(message) { }
    public CustomException(string message, Exception innerException)
    : base(message, innerException) { }
}
