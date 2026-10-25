namespace PLANAZO.Modelos
{
    public abstract class Evento
    {
        private int cuposDisponibles;

        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public DateTime Fecha { get; private set; }
        public decimal PrecioBase { get; private set; }
        public int CapacidadMaxima { get; private set; }

        public int CuposDisponibles
        {
            get { return cuposDisponibles; }
        }

        public Evento(int id, string nombre, DateTime fecha, decimal precioBase, int capacidadMaxima)
        {
            if (precioBase <= 0)
            {
                throw new Exception("El precio base debe ser mayor que cero.");
            }
            if (capacidadMaxima <= 0)
            {
                throw new Exception("La capacidad maxima debe ser mayor que cero.");
            }

            Id = id;
            Nombre = nombre;
            Fecha = fecha;
            PrecioBase = precioBase;
            CapacidadMaxima = capacidadMaxima;
            cuposDisponibles = capacidadMaxima;
        }

        public abstract decimal CalcularPrecioFinal();

        public abstract string ObtenerTipo();

        public void ReducirCupos(int cantidad)
        {
            if (cantidad <= 0)
            {
                throw new Exception("La cantidad de entradas debe ser mayor que cero.");
            }
            if (cantidad > cuposDisponibles)
            {
                throw new Exception("No hay cupos suficientes. Solo quedan " + cuposDisponibles + ".");
            }

            cuposDisponibles = cuposDisponibles - cantidad;
        }

        public void DevolverCupos(int cantidad)
        {
            cuposDisponibles = cuposDisponibles + cantidad;
        }

        public override string ToString()
        {
            return "[" + Id + "] " + ObtenerTipo() + " - " + Nombre
                + " | Fecha: " + Fecha.ToString("dd/MM/yyyy HH:mm")
                + " | Precio: $" + CalcularPrecioFinal().ToString("N0")
                + " | Cupos: " + cuposDisponibles + "/" + CapacidadMaxima;
        }
    }
}
