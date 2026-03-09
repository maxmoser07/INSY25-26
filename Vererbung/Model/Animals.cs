namespace Vererbung;

using System.Text.Json.Serialization;

[JsonDerivedType(typeof(Dog), "dog")]
[JsonDerivedType(typeof(Bird), "bird")]
public abstract class Animals 
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
}

public class Dog : Animals
{
    public string Breed { get; set; } = string.Empty;
}

public class Bird : Animals
{
    public string Wingspan { get; set; } = string.Empty;
}