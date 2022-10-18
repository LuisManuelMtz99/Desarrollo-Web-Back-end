using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationBackEnd.Entidades;
using WebApplicationBackEnd.Filtros;
using WebApplicationBackEnd.Services;

namespace WebApplicationBackEnd.Controllers
{
    [ApiController]

    [Route("videojuegos")]
    public class VideojuegosControllers : ControllerBase

    {
        private readonly ApplicationDbContext dbContext;
        private readonly IService service;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<VideojuegosControllers> logger;
        private readonly IWebHostEnvironment env;

        public VideojuegosControllers(ApplicationDbContext context, IService service,
            ServiceTransient serviceTransient, ServiceScoped serviceScoped,
            ServiceSingleton serviceSingleton, ILogger<VideojuegosControllers> logger,
            IWebHostEnvironment env)
        {
            this.dbContext = context;
            this.service = service;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
            this.logger = logger;
            this.env = env;
        }

        [HttpGet("GUID")]
        [ResponseCache(Duration = 10)]
        [ServiceFilter(typeof(FiltroDeAccion))]
        public ActionResult ObtenerGuid()
        {
           
            return Ok(new
            {
                AlumnosControllerTransient = serviceTransient.guid,
                ServiceA_Transient = service.GetTransient(),
                AlumnosControllerScoped = serviceScoped.guid,
                ServiceA_Scoped = service.GetScoped(),
                AlumnosControllerSingleton = serviceSingleton.guid,
                ServiceA_Singleton = service.GetSingleton()
            });
        }

        [HttpGet]
        [HttpGet("listado")]
        [HttpGet("/lisatado")]

        //[ResponseCache(Duration = 15)]
        //[Authorize]
        //[ServiceFilter(typeof(FiltroDeAccion))]

        public async Task<ActionResult<List<Videojuego>>> Get()
        {
            //* Niveles de logs
            // Critical
            // Error
            // Warning
            // Information
            // Debug
            // Trace
            // *//+
        
            logger.LogInformation("Se obtiene el listado de alumnos");
            logger.LogWarning("Mensaje de prueba warning");
            service.EjecutarJob();

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
            //Ejemplo para validar desde el controlador con la BD con ayuda del dbContext

            var existeAlumnoMismoNombre = await dbContext.Videojuegos.AnyAsync(x => x.nombre == videojuego.nombre);

            if (existeAlumnoMismoNombre)
            {
                return BadRequest("Ya existe un videojuego con este nombre");
            }


            dbContext.Add(videojuego);
            await dbContext.SaveChangesAsync();

            //   var ruta = $@"{env.ContentRootPath}\wwwroot\{nuevosRegistros}";
            //  using (StreamWriter writer = new StreamWriter(ruta, append: true)) { writer.WriteLine(alumno.Id + " " + alumno.Nombre); }

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
