namespace AsyncAwaitSample;
public class WorkItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public List<int> DependentIds { get; set; } = new List<int>();
}
