namespace PustokMVC.CustomExceptions.SliderExceptions;

public class SliderNotFoundException : Exception
{
    public SliderNotFoundException() { }
    public SliderNotFoundException(string? message) : base(message) { }
}