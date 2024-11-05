namespace ApiApp.Models.Errors;

public abstract class NotFoundException(string message) : Exception(message)
{
}