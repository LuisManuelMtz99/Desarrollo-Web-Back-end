namespace WebApplicationBackEnd.Entidades
{
    public class Datos
    {
        public int id { get; set; }
        public string genero { get; set; }

        public string desarrolladores{ get; set; }

        public string plataformas { get; set; }

        public string primerlanzamiento { get; set; }

        public int Videojuegoid  {        get; set; }


        public Videojuego Videojuego { get; set; }



    }
}
