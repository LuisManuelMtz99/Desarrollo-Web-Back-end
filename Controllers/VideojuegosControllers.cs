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
        [HttpGet("listado")]
        [HttpGet("/lisatado")]


        public async Task<ActionResult<List<Videojuego>>> Get()
        {
            return await dbContext.Videojuegos.Include(x => x.datos).ToListAsync();
        }


        [HttpGet("primero")] //videojuegos/primero

        public async Task<ActionResult<Videojuego>> PrimerAutor([FromHeader] int valor, [FromQuery] string videojuego, [FromQuery] int Videojuegoid)
        {
            return await dbContext.Videojuegos.FirstOrDefaultAsync();
        }

        [HttpGet("primero2")] //videojuegos/primero

        public  ActionResult<Videojuego> PrimerAutorD()
        {
            return new Videojuego() { nombre = "DOS" };
        }

        [HttpGet("{id:int}/{param=DbzBt3}")] 

        public async Task<ActionResult< Videojuego>> Get(int Id, string param)
        {
           var videojuego =  await dbContext.Videojuegos.FirstOrDefaultAsync(x => x.id == Id);

               if(videojuego == null)
            {
                return NotFound();
           }

            return videojuego;
        }

        [HttpGet("{Nombre}")]

        public async Task<ActionResult<Videojuego>> Get([FromRoute] string Nombre)
        {
            var videojuego = await dbContext.Videojuegos.FirstOrDefaultAsync(x => x.nombre.Contains(Nombre));

            if (videojuego == null)
            {
                return NotFound();
            }

            return videojuego;
        }


        [HttpPost]

        public async Task<ActionResult> Post([FromBody]Videojuego videojuego)
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
