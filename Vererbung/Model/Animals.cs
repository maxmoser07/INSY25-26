namespace Vererbung;

public class Animals
{
    public int  Id { get; set; }
    public DateTime Recorded { get; set; }
}

public abstract class Dog : Animals
{
    public string Breed { get; set; }
}

public abstract class Bird : Animals
{
    public string Wingspan { get; set; }
}