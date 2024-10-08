using System.Collections;

namespace VideoLib.Models;

public class PlayList : IEnumerable<Video>
{
    private readonly List<Video> _list = [];

    public void Add(Video item) => _list.Add(item);
    public bool Remove(Video item) => _list.Remove(item);

    public IEnumerator<Video> GetEnumerator() => _list.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}