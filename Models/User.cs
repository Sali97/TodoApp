namespace TaskAPI.Models
{
    public class User
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required int LevelId { get; set; }
    }
}
