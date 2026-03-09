namespace Vererbung;

using System.Text.Json.Serialization;

[JsonDerivedType(typeof(Dog), "dog")]
[JsonDerivedType(typeof(Bird), "bird")]
public abstract class Animals // Standardize to plural if that's what you chose
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
}

public class Dog : Animals // Must match the base class name
{
    public string? Breed { get; set; } = string.Empty;
}

public class Bird : Animals // Must match the base class name
{
    public string? Wingspan { get; set; } = string.Empty;
}