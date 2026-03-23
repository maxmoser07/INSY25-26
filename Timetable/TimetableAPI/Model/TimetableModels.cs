using System.Text.Json.Serialization;

public enum Teacher { Smith, Jones, Williams, Brown }
public enum Weekday { Monday, Tuesday, Wednesday, Thursday, Friday }
public enum Subject { Mathematics, Science, History, English, Art }
public enum LessonHour { First = 1, Second, Third, Fourth, Fifth, Sixth }

public class TimetableEntry
{
    public int Id { get; set; }
    public Teacher Teacher { get; set; }
    public Weekday Day { get; set; }
    public Subject Subject { get; set; }
    public LessonHour Hour { get; set; }
}