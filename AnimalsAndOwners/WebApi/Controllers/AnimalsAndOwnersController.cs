using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimalsAndOwnersController : ControllerBase
    {
        private readonly AnimalsAndOwnersDbContext _context;

        public AnimalsAndOwnersController(AnimalsAndOwnersDbContext context)
        {
            _context = context;
        }

        // GET: AnimalsAndOwners/owners
        [HttpGet("owners")]
        public async Task<ActionResult<IEnumerable<Owner>>> GetOwners()
        {
            var owners = await _context.Owners.ToListAsync();

            foreach (var owner in owners)
            {
                owner.Animals = await _context.Animals.Where(a => a.OwnerId == owner.Id).ToListAsync();
            }

            return owners;
        }

        // GET: AnimalsAndOwners/owners/5
        [HttpGet("owners/{id}")]
        public async Task<ActionResult<Owner>> GetOwner(int id)
        {
            var owner = await _context.Owners.FindAsync(id);
            
            if (owner == null)
            {
                return NotFound();
            }

            owner.Animals = await _context.Animals.Where(a => a.OwnerId == owner.Id).ToListAsync();

            return owner;
        }

        // POST: AnimalsAndOwners/owners
        [HttpPost("owners")]
        public async Task<ActionResult<Owner>> PostOwner(Owner owner)
        {
            _context.Owners.Add(owner);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOwner), new { id = owner.Id }, owner);
        }

        // PUT: AnimalsAndOwners/owners/5
        [HttpPut("owners/{id}")]
        public async Task<IActionResult> PutOwner(int id, Owner owner)
        {
            if (id != owner.Id)
            {
                return BadRequest();
            }

            _context.Entry(owner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OwnerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: AnimalsAndOwners/owners/5
        [HttpDelete("owners/{id}")]
        public async Task<IActionResult> DeleteOwner(int id)
        {
            var owner = await _context.Owners.FindAsync(id);
            if (owner == null)
            {
                return NotFound();
            }

            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: AnimalsAndOwners/animals
        [HttpGet("animals")]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals()
        {
            return await _context.Animals.ToListAsync();
        }

        // GET: AnimalsAndOwners/animalsbyowner/5
        [HttpGet("animalsbyowner/{id}")]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimalsByOwner(int id)
        {
            var animals = await _context.Animals.Where(a => a.OwnerId == id).ToListAsync();
            return animals;
        }

        // GET: AnimalsAndOwners/animals/5
        [HttpGet("animals/{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
            var animal = await _context.Animals.FindAsync(id);

            if (animal == null)
            {
                return NotFound();
            }

            return animal;
        }        

        // POST: AnimalsAndOwners/animals
        [HttpPost("animals")]
        public async Task<ActionResult<Animal>> PostAnimal(Animal animal)
        {
            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAnimal), new { id = animal.Id }, animal);
        }

        // PUT: AnimalsAndOwners/animals/5
        [HttpPut("animals/{id}")]
        public async Task<IActionResult> PutAnimal(int id, Animal animal)
        {
            if (id != animal.Id)
            {
                return BadRequest();
            }

            _context.Entry(animal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: AnimalsAndOwners/animals/5
        [HttpDelete("animals/{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OwnerExists(int id)
        {
            return _context.Owners.Any(e => e.Id == id);
        }

        private bool AnimalExists(int id)
        {
            return _context.Animals.Any(e => e.Id == id);
        }

    }
}
