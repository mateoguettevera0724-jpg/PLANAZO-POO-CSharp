namespace PLANAZO.Modelos
{
    public class Reserva
    {
        public int Id { get; private set; }
        public Usuario Usuario { get; private set; }
        public Evento Evento { get; private set; }
        public int CantidadEntradas { get; private set; }
        public decimal ValorTotal { get; private set; }
        public string Estado { get; private set; }

        public Reserva(int id, Usuario usuario, Evento evento, int cantidadEntradas)
        {
            evento.ReducirCupos(cantidadEntradas);

            Id = id;
            Usuario = usuario;
            Evento = evento;
            CantidadEntradas = cantidadEntradas;
            ValorTotal = evento.CalcularPrecioFinal() * cantidadEntradas;
            Estado = "Activa";
        }

        public void Cancelar()
        {
            if (Estado == "Cancelada")
            {
                throw new Exception("Esa reserva ya estaba cancelada.");
            }

            Evento.DevolverCupos(CantidadEntradas);
            Estado = "Cancelada";
        }

        public override string ToString()
        {
            return "[Reserva " + Id + "] " + Usuario.Nombre
                + " | " + Evento.Nombre
                + " | Entradas: " + CantidadEntradas
                + " | Total: $" + ValorTotal.ToString("N0")
                + " | Estado: " + Estado;
        }
    }
}
