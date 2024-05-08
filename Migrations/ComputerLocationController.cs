using ClientLocalizationAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClientLocalizationAPI.Migrations
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputerLocationController : ControllerBase
    {
        private readonly ComputerLocationContext _context;

        public ComputerLocationController(ComputerLocationContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComputerLocation>>> GetComputerLocations()
        {
            if (_context.ComputerLocations == null)
            {
                return NotFound();
            }

            return await _context.ComputerLocations.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ComputerLocation>> GetComputerLocation(int id)
        {
            if (_context.ComputerLocations == null)
            {
                return NotFound();
            }

            var computerLocation = await _context.ComputerLocations.FindAsync(id);

            if (computerLocation == null)
            {
                return NotFound();
            }

            return computerLocation;
        }

        [HttpPost]
        public async Task<ActionResult<ComputerLocation>> PostComputerLocation(ComputerLocation computerLocation)
        {
            if (_context.ComputerLocations == null)
            {
                _context.ComputerLocations.Add(computerLocation);
                await _context.SaveChangesAsync();
            }
            

            //eğer ikiden fazla aynı bilgisyar ismi varsa en eski tarihli olanı parametre olarak gelen ile değiştirir
            var computerLocations = _context.ComputerLocations.Where(x => x.ComputerName == computerLocation.ComputerName).ToList();
            if (computerLocations.Count > 2)
            {
                var oldestComputerLocation = computerLocations.OrderBy(x => x.SavedTime).First();
                oldestComputerLocation.Latitude = computerLocation.Latitude;
                oldestComputerLocation.Longitude = computerLocation.Longitude;
                oldestComputerLocation.SavedTime = computerLocation.SavedTime;
                oldestComputerLocation.UserName = computerLocation.UserName;
                _context.Entry(oldestComputerLocation).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetComputerLocation), new { id = oldestComputerLocation.ID }, oldestComputerLocation);
            }
            else
            {
                _context.ComputerLocations.Add(computerLocation);
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(GetComputerLocation), new { id = computerLocation.ID }, computerLocation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutComputerLocation(int id, ComputerLocation computerLocation)
        {
            if (id != computerLocation.ID)
            {
                return BadRequest();
            }

            _context.Entry(computerLocation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComputerLocationExists(id))
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

        private bool ComputerLocationExists(int id)
        {
            return _context.ComputerLocations.Any(e => e.ID == id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComputerLocation(int id)
        {
            var computerLocation = await _context.ComputerLocations.FindAsync(id);
            if (computerLocation == null)
            {
                return NotFound();
            }

            _context.ComputerLocations.Remove(computerLocation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
