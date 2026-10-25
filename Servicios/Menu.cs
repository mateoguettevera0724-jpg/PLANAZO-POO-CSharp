using PLANAZO.Modelos;
using PLANAZO.Servicios;

namespace PLANAZO.Servicios
{
    public class Menu
    {
        private GestorReservas gestor;

        public Menu(GestorReservas gestor)
        {
            this.gestor = gestor;
        }

        public void Iniciar()
        {
            int opcion = -1;

            while (opcion != 0)
            {
                MostrarMenu();

                try
                {
                    opcion = LeerEntero("");
                    Console.WriteLine();

                    if (opcion == 1) VerEventos();
                    else if (opcion == 2) ConsultarEvento();
                    else if (opcion == 3) ReservarEntradas();
                    else if (opcion == 4) VerReservas();
                    else if (opcion == 5) CancelarReserva();
                    else if (opcion == 6) VerResumen();
                    else if (opcion == 7) VerReservasDeUsuario();
                    else if (opcion == 0) Console.WriteLine("Hasta luego!");
                    else Console.WriteLine("Opcion no valida.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex.Message);
                    opcion = -1;
                }

                if (opcion != 0)
                {
                    Console.WriteLine();
                    Console.Write("Presione ENTER para continuar...");
                    Console.ReadLine();
                }
            }
        }

        private void MostrarMenu()
        {
            Console.WriteLine();
            Console.WriteLine("================================");
            Console.WriteLine("        PLANAZO - EVENTOS");
            Console.WriteLine("================================");
            Console.WriteLine();
            Console.WriteLine("1. Ver eventos disponibles");
            Console.WriteLine("2. Consultar evento");
            Console.WriteLine("3. Reservar entradas");
            Console.WriteLine("4. Ver reservas realizadas");
            Console.WriteLine("5. Cancelar una reserva");
            Console.WriteLine("6. Ver resumen de reservas");
            Console.WriteLine("7. Ver reservas de un usuario");
            Console.WriteLine("0. Salir");
            Console.WriteLine();
            Console.Write("Seleccione una opción: ");
        }

        private int LeerEntero(string mensaje)
        {
            Console.Write(mensaje);
            string texto = Console.ReadLine();
            int numero;

            if (!int.TryParse(texto, out numero))
            {
                throw new Exception("Debe escribir un numero entero.");
            }

            return numero;
        }

        private void VerEventos()
        {
            Console.WriteLine("--- EVENTOS DISPONIBLES ---");
            foreach (Evento evento in gestor.ObtenerEventos())
            {
                Console.WriteLine(evento);
            }
        }

        private void ConsultarEvento()
        {
            int id = LeerEntero("Id del evento: ");
            Evento evento = gestor.BuscarEvento(id);

            Console.WriteLine();
            Console.WriteLine("Nombre: " + evento.Nombre);
            Console.WriteLine("Tipo: " + evento.ObtenerTipo());
            Console.WriteLine("Fecha: " + evento.Fecha.ToString("dd/MM/yyyy HH:mm"));
            Console.WriteLine("Precio base: $" + evento.PrecioBase.ToString("N0"));
            Console.WriteLine("Precio final: $" + evento.CalcularPrecioFinal().ToString("N0"));
            Console.WriteLine("Capacidad maxima: " + evento.CapacidadMaxima);
            Console.WriteLine("Cupos disponibles: " + evento.CuposDisponibles);
        }

        private void ReservarEntradas()
        {
            VerEventos();
            Console.WriteLine();

            int idEvento = LeerEntero("Id del evento: ");
            Console.Write("Nombre de la persona: ");
            string nombre = Console.ReadLine();
            int cantidad = LeerEntero("Cantidad de entradas: ");

            Reserva reserva = gestor.Reservar(idEvento, nombre, cantidad);

            Console.WriteLine();
            Console.WriteLine("Reserva realizada con exito:");
            Console.WriteLine(reserva);
        }

        private void VerReservas()
        {
            Console.WriteLine("--- RESERVAS REALIZADAS ---");

            if (gestor.ObtenerReservas().Count == 0)
            {
                Console.WriteLine("Todavia no hay reservas.");
                return;
            }

            foreach (Reserva reserva in gestor.ObtenerReservas())
            {
                Console.WriteLine(reserva);
            }
        }

        private void CancelarReserva()
        {
            VerReservas();
            Console.WriteLine();

            int id = LeerEntero("Id de la reserva a cancelar: ");
            Reserva reserva = gestor.CancelarReserva(id);

            Console.WriteLine();
            Console.WriteLine("Reserva cancelada. Se devolvieron " + reserva.CantidadEntradas + " cupos.");
            Console.WriteLine("Cupos disponibles de " + reserva.Evento.Nombre + ": " + reserva.Evento.CuposDisponibles);
        }

        private void VerResumen()
        {
            Console.WriteLine("--- RESUMEN DE RESERVAS ---");
            Console.WriteLine("Reservas activas: " + gestor.ContarReservasActivas());
            Console.WriteLine("Entradas reservadas: " + gestor.ContarEntradasReservadas());
            Console.WriteLine("Valor total: $" + gestor.CalcularValorTotal().ToString("N0"));
        }

        private void VerReservasDeUsuario()
        {
            Console.Write("Nombre de la persona: ");
            string nombre = Console.ReadLine();

            List<Reserva> lista = gestor.ReservasDeUsuario(nombre);

            Console.WriteLine();
            if (lista.Count == 0)
            {
                Console.WriteLine("Esa persona no tiene reservas.");
                return;
            }

            Console.WriteLine("--- RESERVAS DE " + nombre.Trim().ToUpper() + " ---");
            foreach (Reserva reserva in lista)
            {
                Console.WriteLine(reserva);
            }
        }
    }
}
