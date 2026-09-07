namespace UEA.EstructuraDatos.Premiacion
{
    public class Atleta
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Pais { get; set; }
        public string Disciplina { get; set; }

        public Atleta(string id, string nombre, string pais, string disciplina)
        {
            Id = id;
            Nombre = nombre;
            Pais = pais;
            Disciplina = disciplina;
        }

        public override string ToString()
        {
            return $"ID: {Id} | Nombre: {Nombre} | País: {Pais} | Disciplina: {Disciplina}";
        }
    }
}