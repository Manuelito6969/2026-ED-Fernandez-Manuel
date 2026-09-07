using System.Diagnostics;

namespace UEA.EstructuraDatos.Premiacion
{
    public enum PosicionPodio
    {
        Oro = 1,
        Plata = 2,
        Bronce = 3
    }

    public class ResultadoMedallero
    {
        public string Pais { get; set; } = "";
        public int Oro { get; set; }
        public int Plata { get; set; }
        public int Bronce { get; set; }
        public int Total => Oro + Plata + Bronce;
    }

    public class SistemaPremiacion
    {
        // Diccionario: relaciona el ID único con cada atleta.
        private readonly Dictionary<string, Atleta> _atletasPorId = new();

        // Conjunto: almacena disciplinas únicas y evita duplicados.
        private readonly HashSet<string> _disciplinasHabilitadas =
            new(StringComparer.OrdinalIgnoreCase);

        // Diccionario anidado: relaciona cada disciplina con su podio.
        private readonly Dictionary<string, Dictionary<PosicionPodio, string>> _podios =
            new(StringComparer.OrdinalIgnoreCase);

        // Conjunto: impide premiar dos veces al mismo atleta en una disciplina.
        private readonly HashSet<string> _atletasPremiadosPorDisciplina =
            new(StringComparer.OrdinalIgnoreCase);

        public SistemaPremiacion()
        {
            AgregarDisciplina("Atletismo");
            AgregarDisciplina("Natación");
            AgregarDisciplina("Ciclismo");
            AgregarDisciplina("Gimnasia");
        }

        public bool AgregarDisciplina(string disciplina)
        {
            return _disciplinasHabilitadas.Add(disciplina);
        }

        public bool RegistrarAtleta(Atleta atleta)
        {
            if (!_disciplinasHabilitadas.Contains(atleta.Disciplina))
                return false;

            return _atletasPorId.TryAdd(atleta.Id, atleta);
        }

        public Atleta? ConsultarAtleta(string id)
        {
            _atletasPorId.TryGetValue(id, out Atleta? atleta);
            return atleta;
        }

        public bool ActualizarAtleta(
            string id,
            string nombre,
            string pais,
            string disciplina)
        {
            if (!_atletasPorId.TryGetValue(id, out Atleta? atleta))
                return false;

            if (!_disciplinasHabilitadas.Contains(disciplina))
                return false;

            atleta.Nombre = nombre;
            atleta.Pais = pais;
            atleta.Disciplina = disciplina;
            return true;
        }

        public bool EliminarAtleta(string id)
        {
            if (!_atletasPorId.ContainsKey(id))
                return false;

            // Elimina referencias del atleta en los podios.
            foreach (var podio in _podios)
            {
                var posiciones = podio.Value
                    .Where(x => x.Value.Equals(
                        id,
                        StringComparison.OrdinalIgnoreCase))
                    .Select(x => x.Key)
                    .ToList();

                foreach (var posicion in posiciones)
                {
                    podio.Value.Remove(posicion);
                }

                _atletasPremiadosPorDisciplina.Remove(
                    $"{podio.Key}|{id}");
            }

            return _atletasPorId.Remove(id);
        }

        public IEnumerable<Atleta> ObtenerTodosLosAtletas()
        {
            return _atletasPorId.Values
                .OrderBy(a => a.Nombre);
        }

        public IEnumerable<Atleta> ListarPorDisciplina(string disciplina)
        {
            return _atletasPorId.Values
                .Where(a => a.Disciplina.Equals(
                    disciplina,
                    StringComparison.OrdinalIgnoreCase))
                .OrderBy(a => a.Nombre);
        }

        public bool AsignarMedalla(
            string disciplina,
            PosicionPodio posicion,
            string atletaId)
        {
            if (!_disciplinasHabilitadas.Contains(disciplina))
                return false;

            if (!_atletasPorId.TryGetValue(atletaId, out Atleta? atleta))
                return false;

            if (!atleta.Disciplina.Equals(
                disciplina,
                StringComparison.OrdinalIgnoreCase))
                return false;

            string clavePremio = $"{disciplina}|{atletaId}";

            if (_atletasPremiadosPorDisciplina.Contains(clavePremio))
                return false;

            if (!_podios.ContainsKey(disciplina))
            {
                _podios[disciplina] =
                    new Dictionary<PosicionPodio, string>();
            }

            if (_podios[disciplina].ContainsKey(posicion))
                return false;

            _podios[disciplina][posicion] = atletaId;
            _atletasPremiadosPorDisciplina.Add(clavePremio);

            return true;
        }

        public Dictionary<PosicionPodio, Atleta> ObtenerPodio(
            string disciplina)
        {
            var resultado =
                new Dictionary<PosicionPodio, Atleta>();

            if (!_podios.TryGetValue(
                disciplina,
                out Dictionary<PosicionPodio, string>? podio))
                return resultado;

            foreach (var item in podio)
            {
                if (_atletasPorId.TryGetValue(
                    item.Value,
                    out Atleta? atleta))
                {
                    resultado[item.Key] = atleta;
                }
            }

            return resultado;
        }

        public List<ResultadoMedallero> ObtenerMedalleroGeneral()
        {
            var medallero =
                new Dictionary<string, ResultadoMedallero>(
                    StringComparer.OrdinalIgnoreCase);

            foreach (var podio in _podios.Values)
            {
                foreach (var premio in podio)
                {
                    if (!_atletasPorId.TryGetValue(
                        premio.Value,
                        out Atleta? atleta))
                        continue;

                    if (!medallero.ContainsKey(atleta.Pais))
                    {
                        medallero[atleta.Pais] =
                            new ResultadoMedallero
                            {
                                Pais = atleta.Pais
                            };
                    }

                    switch (premio.Key)
                    {
                        case PosicionPodio.Oro:
                            medallero[atleta.Pais].Oro++;
                            break;

                        case PosicionPodio.Plata:
                            medallero[atleta.Pais].Plata++;
                            break;

                        case PosicionPodio.Bronce:
                            medallero[atleta.Pais].Bronce++;
                            break;
                    }
                }
            }

            return medallero.Values
                .OrderByDescending(m => m.Oro)
                .ThenByDescending(m => m.Plata)
                .ThenByDescending(m => m.Bronce)
                .ThenBy(m => m.Pais)
                .ToList();
        }

        public IEnumerable<string> ObtenerDisciplinasHabilitadas()
        {
            return _disciplinasHabilitadas.OrderBy(d => d);
        }

        public (long ticks, double milisegundos) MedirTiempoBusqueda(
            string id)
        {
            Stopwatch reloj = Stopwatch.StartNew();

            _atletasPorId.TryGetValue(id, out _);

            reloj.Stop();

            return (reloj.ElapsedTicks, reloj.Elapsed.TotalMilliseconds);
        }

        public (long ticks, double milisegundos) MedirTiempoMedallero()
        {
            Stopwatch reloj = Stopwatch.StartNew();

            ObtenerMedalleroGeneral();

            reloj.Stop();

            return (reloj.ElapsedTicks, reloj.Elapsed.TotalMilliseconds);
        }
    }
}