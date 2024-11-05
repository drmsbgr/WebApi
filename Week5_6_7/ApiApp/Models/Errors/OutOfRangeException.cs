namespace ApiApp.Models.Errors;

public class OutOfRangeException(string message) : Exception(message)
{
}