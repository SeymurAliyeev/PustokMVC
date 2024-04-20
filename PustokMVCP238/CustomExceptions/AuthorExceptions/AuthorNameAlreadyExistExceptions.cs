namespace PustokMVC.CustomExceptions.AuthorExceptions;

public class AuthorNameAlreadyExistException : Exception
{
    public string? PropertyName { get; set; }
    public AuthorNameAlreadyExistException() { }
    public AuthorNameAlreadyExistException(string? message) : base(message) { }
    public AuthorNameAlreadyExistException(string? propertyName, string? message) : base(message)
    {
        PropertyName = propertyName;
    }
}