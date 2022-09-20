using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationBackEnd.Entidades;

namespace WebApplicationBackEnd.Controllers
{
    [ApiController]

    [Route("videojuegos")]
    public class VideojuegosControllers : ControllerBase

    {
        private readonly ApplicationDbContext dbContext;

        public VideojuegosControllers(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]

        public async Task<ActionResult<List<Videojuego>>> Get()
        {
            return await dbContext.Videojuegos.Include(x => x.datos).ToListAsync();
        }
        [HttpPost]

        public async Task<ActionResult> Post(Videojuego videojuego)
        {
            dbContext.Add(videojuego);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]

        public async Task<ActionResult> Put(Videojuego videojuego, int id)
        {
            if(videojuego.id != id)
            {
                return BadRequest ("El id del videojuego solicitado no coincide con el establecido en la url");
            }
            dbContext.Update(videojuego);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{Id:int}")]

        public async Task<ActionResult> Delete(int Id)
        {
           var exist = await dbContext.Videojuegos.AnyAsync(x => x.id == Id);
            if(!exist)
            {
                return NotFound();
            }

            dbContext.Remove(new Videojuego()
            {
                id = Id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
       
    }
}
