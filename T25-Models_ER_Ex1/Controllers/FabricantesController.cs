using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T25_Models_ER_Ex1.dto;
using T25_Models_ER_Ex1.Models;

namespace T25_Models_ER_Ex1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FabricantesController : ControllerBase
    {
        private readonly APIContext _context;

        public FabricantesController(APIContext context)
        {
            _context = context;
        }
        private static readonly Expression<Func<Fabricantes, FabricantesDto>> AsFabricanteDto =
            x => new FabricantesDto
            {
                Nombre = x.Nombre
            };

        // GET: api/Fabricantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fabricantes>>> GetFabricantes()
        {
            return await _context.Fabricantes.ToListAsync();
        }

        // GET: api/Fabricantes/5
        [HttpGet("{codigo}")]
        public async Task<ActionResult<Fabricantes>> GetFabricantes(int id)
        {
            var fabricantes = await _context.Fabricantes.FindAsync(id);

            if (fabricantes == null)
            {
                return NotFound();
            }

            return fabricantes;
        }

        // PUT: api/Fabricantes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutFabricantes(int id, Fabricantes fabricantes)
        {
            if (id != fabricantes.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(fabricantes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FabricantesExists(id))
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

        // POST: api/Fabricantes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Fabricantes>> PostFabricantes(Fabricantes fabricantes)
        {
            _context.Fabricantes.Add(fabricantes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFabricantes", new { id = fabricantes.Codigo }, fabricantes);
        }

        // DELETE: api/Fabricantes/5
        [HttpDelete("{codigo}")]
        public async Task<ActionResult<Fabricantes>> DeleteFabricantes(int id)
        {
            var fabricantes = await _context.Fabricantes.FindAsync(id);
            if (fabricantes == null)
            {
                return NotFound();
            }

            _context.Fabricantes.Remove(fabricantes);
            await _context.SaveChangesAsync();

            return fabricantes;
        }

        private bool FabricantesExists(int id)
        {
            return _context.Fabricantes.Any(e => e.Codigo == id);
        }

        /*
         * 
         * 
         * DTOs
         * 
         * 
         */

        // GET api/Fabricante/nombre
        // optiene el nombre de  todos los fabricantes
        [HttpGet("nombre")]
        public IQueryable<FabricantesDto> GetFabricanteNombre()
        {
            return _context.Fabricantes.Select(AsFabricanteDto);
        }
    }
}
