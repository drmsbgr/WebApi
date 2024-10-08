namespace WebApiApp.Models;

public class Info(DateTime dateTime)
{
    private readonly DateTime dateTime = dateTime;

    public string Date => dateTime.ToString("d");
    public string Time => dateTime.ToString("t");
    public string Day => dateTime.ToString("dddd");
    public string Month => dateTime.ToString("MMMM");
}