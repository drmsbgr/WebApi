namespace WebApiApp.Models;

public class Person(string firstName, string lastName, int age)
{
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string FullName => $"{FirstName} {LastName?.ToUpper()}";
    public int Age { get; set; } = age;
}
