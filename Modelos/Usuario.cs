namespace PLANAZO.Modelos
{
    public class Usuario
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }

        public Usuario(int id, string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new Exception("El nombre de la persona no puede estar vacio.");
            }

            Id = id;
            Nombre = nombre.Trim();
        }

        public override string ToString()
        {
            return "[Usuario " + Id + "] " + Nombre;
        }
    }
}
