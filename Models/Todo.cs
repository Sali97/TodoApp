namespace TaskAPI.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public bool IsDone { get; set; }
        public int CreatedBy { get; set; }
        public int WaitingFor { get; set; }
    }
}
