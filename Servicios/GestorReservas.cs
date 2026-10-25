using PLANAZO.Modelos;

namespace PLANAZO.Servicios
{
    public class GestorReservas
    {
        private List<Evento> eventos = new List<Evento>();
        private List<Reserva> reservas = new List<Reserva>();
        private List<Usuario> usuarios = new List<Usuario>();
        private int siguienteIdReserva = 1;
        private int siguienteIdUsuario = 1;

        public GestorReservas()
        {
            CargarEventosIniciales();
        }

        private void CargarEventosIniciales()
        {
            eventos.Add(new Cine(1, "Interstellar", new DateTime(2026, 11, 7, 19, 30, 0), 15000m, 50));
            eventos.Add(new Cine(2, "Coco", new DateTime(2026, 11, 8, 16, 0, 0), 12000m, 40));
            eventos.Add(new Concierto(3, "Concierto de Rock", new DateTime(2026, 11, 14, 20, 0, 0), 50000m, 100));
            eventos.Add(new Concierto(4, "Concierto de Salsa", new DateTime(2026, 11, 21, 21, 0, 0), 60000m, 80));
            eventos.Add(new Taller(5, "Taller de Fotografia", new DateTime(2026, 11, 10, 9, 0, 0), 80000m, 20));
            eventos.Add(new Taller(6, "Taller de Cocina", new DateTime(2026, 11, 12, 15, 0, 0), 70000m, 15));
            eventos.Add(new Festival(7, "Festival de Musica", new DateTime(2026, 12, 5, 18, 0, 0), 100000m, 200));
        }

        public List<Evento> ObtenerEventos()
        {
            return eventos;
        }

        public List<Reserva> ObtenerReservas()
        {
            return reservas;
        }

        public Evento BuscarEvento(int id)
        {
            foreach (Evento evento in eventos)
            {
                if (evento.Id == id)
                {
                    return evento;
                }
            }

            throw new Exception("No existe un evento con el id " + id + ".");
        }

        public Reserva BuscarReserva(int id)
        {
            foreach (Reserva reserva in reservas)
            {
                if (reserva.Id == id)
                {
                    return reserva;
                }
            }

            throw new Exception("No existe una reserva con el id " + id + ".");
        }

        private Usuario BuscarUsuario(string nombre)
        {
            foreach (Usuario usuario in usuarios)
            {
                if (usuario.Nombre.ToLower() == nombre.Trim().ToLower())
                {
                    return usuario;
                }
            }

            return null;
        }

        public Reserva Reservar(int idEvento, string nombrePersona, int cantidad)
        {
            if (string.IsNullOrWhiteSpace(nombrePersona))
            {
                throw new Exception("El nombre de la persona no puede estar vacio.");
            }

            Evento evento = BuscarEvento(idEvento);

            Usuario usuario = BuscarUsuario(nombrePersona);
            bool esNuevo = false;
            if (usuario == null)
            {
                usuario = new Usuario(siguienteIdUsuario, nombrePersona);
                esNuevo = true;
            }

            Reserva reserva = new Reserva(siguienteIdReserva, usuario, evento, cantidad);

            if (esNuevo)
            {
                usuarios.Add(usuario);
                siguienteIdUsuario++;
            }

            reservas.Add(reserva);
            siguienteIdReserva++;
            return reserva;
        }

        public Reserva CancelarReserva(int idReserva)
        {
            Reserva reserva = BuscarReserva(idReserva);
            reserva.Cancelar();
            return reserva;
        }

        public List<Reserva> ReservasDeUsuario(string nombrePersona)
        {
            List<Reserva> resultado = new List<Reserva>();

            if (string.IsNullOrWhiteSpace(nombrePersona))
            {
                throw new Exception("El nombre de la persona no puede estar vacio.");
            }

            Usuario usuario = BuscarUsuario(nombrePersona);
            if (usuario == null)
            {
                return resultado;
            }

            foreach (Reserva reserva in reservas)
            {
                if (reserva.Usuario.Id == usuario.Id)
                {
                    resultado.Add(reserva);
                }
            }

            return resultado;
        }

        public int ContarReservasActivas()
        {
            int total = 0;
            foreach (Reserva reserva in reservas)
            {
                if (reserva.Estado == "Activa")
                {
                    total++;
                }
            }
            return total;
        }

        public int ContarEntradasReservadas()
        {
            int total = 0;
            foreach (Reserva reserva in reservas)
            {
                if (reserva.Estado == "Activa")
                {
                    total = total + reserva.CantidadEntradas;
                }
            }
            return total;
        }

        public decimal CalcularValorTotal()
        {
            decimal total = 0;
            foreach (Reserva reserva in reservas)
            {
                if (reserva.Estado == "Activa")
                {
                    total = total + reserva.ValorTotal;
                }
            }
            return total;
        }
    }
}
