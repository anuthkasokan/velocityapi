namespace Domain.Entity;

public class Developer
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public ICollection<Game> Games { get; set; } = [];
}
