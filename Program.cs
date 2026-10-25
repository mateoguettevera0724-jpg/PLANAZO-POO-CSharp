using System.Text;
using PLANAZO.Servicios;

Console.OutputEncoding = Encoding.UTF8;

GestorReservas gestor = new GestorReservas();
Menu menu = new Menu(gestor);
menu.Iniciar();
