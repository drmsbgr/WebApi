using System.Collections;

namespace VideoLib.Models;

public class PlayList : IEnumerable<Video>
{
    private readonly List<Video> _list = [];

    public void Add(Video item) => _list.Add(item);
    public int RemoveById(int id) => _list.RemoveAll(x => x.Id == id);

    public IEnumerator<Video> GetEnumerator() => _list.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}