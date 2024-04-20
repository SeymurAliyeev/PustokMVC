namespace PustokMVC.CustomExceptions.GenreExceptions;

public class GenreNameAlreadyExistException : Exception
{
    public string? PropertyName { get; set; }
    public GenreNameAlreadyExistException() { }
    public GenreNameAlreadyExistException(string? message) : base(message) { }
    public GenreNameAlreadyExistException(string? propertyName, string? message) : base(message)
    {
        PropertyName = propertyName;
    }
}