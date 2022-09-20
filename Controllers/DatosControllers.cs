using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationBackEnd.Entidades;

namespace WebApplicationBackEnd.Controllers
{
    [ApiController]

    [Route("api/datos")]
    public class DatosControllers : ControllerBase  
    {

        private readonly ApplicationDbContext dbContext;

        public DatosControllers (ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]

        public async Task<ActionResult<List<Datos>>> GetAll()
        {
            return await dbContext.Datos.ToListAsync();
        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<Datos>> GetByid(int Id)
        {
            return await dbContext.Datos.FirstOrDefaultAsync(x => x.id == Id);
        }

        [HttpPost]

        public async Task<ActionResult> Post(Datos datos)
        {
            var existeVideojuego = await dbContext.Videojuegos.AnyAsync(x => x.id == datos.Videojuegoid);

            if (!existeVideojuego)
            {
                return BadRequest($"No existe el videojuego con el id: {datos.id} ");

            }
            
            dbContext.Add(datos);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]

        public async Task<ActionResult> Put(Datos datos, int Id)
        {

            var exist = await dbContext.Datos.AnyAsync(x => x.id == Id);
           
            if (!exist)
            {

                return NotFound("El videojuego especificado no existe. ");

            }

            if(datos.id != Id)
            {

                return BadRequest("El id de el videojuego no coincide con el establecido en la url. ");
            }

            dbContext.Update(datos);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{Id:int}")]

        public async Task<ActionResult> Delete(int Id)
        {
            var exist = await dbContext.Datos.AnyAsync(x => x.id == Id);
            if (!exist)
            {
                return NotFound("El recurso no fue encontrado. ");
            }

            dbContext.Remove(new Datos()
            {
                id = Id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
