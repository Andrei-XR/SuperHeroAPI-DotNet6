using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;
using SuperHeroAPI.Models;

namespace SuperHeroAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SuperHeroiController : Controller
    {
        private readonly DataContext _context;

        public SuperHeroiController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHeroi>>> List()
        {
            return Ok(await _context.SuperHerois.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHeroi>> Get(int id)
        {
            var heroi = await _context.SuperHerois.FindAsync(id);

            if(heroi == null)
            {
                return BadRequest("Heroi não encontrado!");
            }

            return Ok(heroi);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHeroi>>> AddHeroi(SuperHeroi heroi)
        {
            _context.SuperHerois.Add(heroi);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHerois.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SuperHeroi>> UpdateHeroi(SuperHeroi request)
        {
            var heroi = await _context.SuperHerois.FindAsync(request.Id);

            if(heroi == null)
            {
                return BadRequest("Heroi não encontrado!");
            }

            heroi.NomeHeroi = request.NomeHeroi;
            heroi.Nome = request.Nome;
            heroi.Sobrenome = request.Sobrenome;
            heroi.Cidade = request.Cidade;

            await _context.SaveChangesAsync();

            return Ok(heroi);
        }

        [HttpDelete]
        public async Task<ActionResult<List<SuperHeroi>>> Delete(int id)
        {
            var heroi = await _context.SuperHerois.FindAsync(id);

            if(heroi == null)
            {
                return BadRequest("Heroi não encontrado!");
            }

            _context.SuperHerois.Remove(heroi);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHerois.ToListAsync());
        }
    }
}
