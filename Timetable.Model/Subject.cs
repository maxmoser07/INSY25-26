namespace Timetable.Model;

public class Subject
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;

    // Navigation Property für M:N
    public ICollection<SchoolClass> Classes { get; set; } = new List<SchoolClass>();
}