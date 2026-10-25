namespace PLANAZO.Modelos
{
    public class Festival : Evento
    {
        public Festival(int id, string nombre, DateTime fecha, decimal precioBase, int capacidadMaxima)
            : base(id, nombre, fecha, precioBase, capacidadMaxima)
        {
        }

        public override decimal CalcularPrecioFinal()
        {
            return PrecioBase * 1.25m + 8000m;
        }

        public override string ObtenerTipo()
        {
            return "Festival";
        }
    }
}
