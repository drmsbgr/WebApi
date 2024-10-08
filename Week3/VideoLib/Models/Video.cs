namespace VideoLib.Models;

public class Video(int id, string? title, double duration)
{
    public int Id { get; set; } = id;
    public string? Title { get; set; } = title;
    public double Duration { get; set; } = duration;
}
