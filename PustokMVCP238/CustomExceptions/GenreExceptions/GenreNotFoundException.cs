namespace PustokMVC.CustomExceptions.GenreExceptions;

public class GenreNotFoundException : Exception
{
    public GenreNotFoundException() { }
    public GenreNotFoundException(string? message) : base(message) { }
}