using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T25_Models_ER_Ex1.Models;
using T25_Models_ER_Ex1.dto;
using System.Linq.Expressions;
using System.Web.Http.Description;

namespace T25_Models_ER_Ex1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly APIContext _context;
        private static readonly Expression<Func<Articulos, ArticulosDto>> AsArticuloDto =
            x => new ArticulosDto
            {
                Nombre = x.Nombre,
                Precio = x.Precio,
            };


        public ArticulosController(APIContext context)
        {
            _context = context;
        }

        // GET: api/Articulos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Articulos>>> GetArticulos()
        {
            return await _context.Articulos.ToListAsync();
        }

        // GET: api/Articulos/5
        [HttpGet("{codigo}")]
        public async Task<ActionResult<Articulos>> GetArticulos(int codigo)
        {
            var articulos = await _context.Articulos.FindAsync(codigo);

            if (articulos == null)
            {
                return NotFound();
            }

            return articulos;
        }

        // PUT: api/Articulos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutArticulos(int codigo, Articulos articulos)
        {
            if (codigo != articulos.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(articulos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticulosExists(codigo))
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

        // POST: api/Articulos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Articulos>> PostArticulos(Articulos articulos)
        {
            _context.Articulos.Add(articulos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticulos", new { id = articulos.Codigo }, articulos);
        }

        // DELETE: api/Articulos/5
        [HttpDelete("{codigo}")]
        public async Task<ActionResult<Articulos>> DeleteArticulos(int codigo)
        {
            var articulos = await _context.Articulos.FindAsync(codigo);
            if (articulos == null)
            {
                return NotFound();
            }

            _context.Articulos.Remove(articulos);
            await _context.SaveChangesAsync();

            return articulos;
        }

        private bool ArticulosExists(int codigo)
        {
            return _context.Articulos.Any(e => e.Codigo == codigo);
        }


        /*
         * 
         * 
         * DTOS
         * 
         * 
         */

        // GET api/Articulos/fabricante/{codigoFabricante}
        // optiene todos los articulos de un fabricante mostrando nombre y precio
        [HttpGet("fabricante/{codigoFabricante}")]
        public IQueryable<ArticulosDto> GetArticulosFabricante(int codigoFabricante)
        {
            return _context.Articulos.Include(t => t.Fabricantes)
                .Where(t => t.Fabricante.Equals(codigoFabricante))
                .Select(AsArticuloDto);
        }
    }
}
