using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vererbung;

namespace AnimalsController.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly MyDbContext _context;

    public AnimalsController(MyDbContext context) => _context = context;

    // GET: api/animals (Returns all Dogs and Birds combined)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Animals>>> GetAnimals()
    {
        return await _context.Animals.ToListAsync();
    }

    // POST: api/animals/dog
    [HttpPost("dog")]
    public async Task<ActionResult<Dog>> PostDog(Dog dog)
    {
        _context.Dogs.Add(dog);
        await _context.SaveChangesAsync();
        return Ok(dog);
    }

    // DELETE: api/animals/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnimal(Guid id)
    {
        var animal = await _context.Animals.FindAsync(id);
        if (animal == null) return NotFound();

        _context.Animals.Remove(animal);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}