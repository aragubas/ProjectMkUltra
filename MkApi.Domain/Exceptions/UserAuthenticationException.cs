namespace MkApi.Domain.Exceptions;

public class UserAuthenticationException : Exception
{
    public UserAuthenticationException() : base() { }
    public UserAuthenticationException(string message) : base(message) { }
    public UserAuthenticationException(string message, Exception inner) : base(message, inner) { }
}