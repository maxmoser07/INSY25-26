namespace Vererbung;

public class Animals
{
    public int  Id { get; set; }
    public DateTime Recorded { get; set; }
}

public class Dog : Animals
{
    public string Breed { get; set; }
}

public class Bird : Animals
{
    public string Wingspan { get; set; }
}