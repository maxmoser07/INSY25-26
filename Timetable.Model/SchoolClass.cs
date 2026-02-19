namespace Timetable.Model;

public class SchoolClass
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    // Navigation Property für M:N
    public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}