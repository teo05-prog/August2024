namespace WebApi.Entities;

public class Profile
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
    public string? Gender { get; set; }
    public List<Like>? Likes { get; set; }
}