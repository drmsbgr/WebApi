namespace VideoLib.Models;

public class Video(int id, string? title, double duration)
{
    public int Id { get; set; } = id;
    public string? Title { get; set; } = title;
    public double Duration { get; set; } = duration;

    public static PlayList Videos =
    [
        new(1, "Heavy Blanket", 3.52),
        new(2, "The Summoning", 4.20),
        new(3, "Lovebite", 3.24),
        new(4, "Like A Villain", 3.54),
        new(5, "Bring Me To Life", 3.49)
    ];
}
