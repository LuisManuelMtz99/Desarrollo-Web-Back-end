namespace WebApplicationBackEnd.Entidades
{
    public class Videojuego
    {
        public int id { get; set; }

        public string nombre { get; set; }

        public List<Datos> datos { get; set; }

    }
}
