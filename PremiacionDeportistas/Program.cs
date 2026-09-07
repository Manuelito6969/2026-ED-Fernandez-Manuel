using UEA.EstructuraDatos.Premiacion;

class Program
{
    static readonly SistemaPremiacion sistema = new();

    static void Main()
    {
        CargarDatosDemostracion();

        int opcion;

        do
        {
            MostrarMenu();

            Console.Write("Seleccione una opción: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("\nOpción no válida.");
                Pausar();
                continue;
            }

            Console.Clear();

            switch (opcion)
            {
                case 1:
                    MostrarDisciplinas();
                    break;

                case 2:
                    RegistrarAtleta();
                    break;

                case 3:
                    ConsultarAtleta();
                    break;

                case 4:
                    ActualizarAtleta();
                    break;

                case 5:
                    EliminarAtleta();
                    break;

                case 6:
                    MostrarTodosLosAtletas();
                    break;

                case 7:
                    MostrarAtletasPorDisciplina();
                    break;

                case 8:
                    AsignarMedalla();
                    break;

                case 9:
                    MostrarPodio();
                    break;

                case 10:
                    MostrarMedallero();
                    break;

                case 11:
                    DemostrarControlDuplicados();
                    break;

                case 12:
                    MedirTiempoEjecucion();
                    break;

                case 13:
                    Console.WriteLine(
                        "Programa finalizado correctamente.");
                    break;

                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }

            if (opcion != 13)
                Pausar();

        } while (opcion != 13);
    }

    static void MostrarMenu()
    {
        Console.Clear();

        Console.WriteLine("==============================================");
        Console.WriteLine("     SISTEMA DE PREMIACIÓN DE DEPORTISTAS");
        Console.WriteLine("==============================================");
        Console.WriteLine("1.  Mostrar disciplinas habilitadas");
        Console.WriteLine("2.  Registrar atleta");
        Console.WriteLine("3.  Consultar atleta por ID");
        Console.WriteLine("4.  Actualizar atleta");
        Console.WriteLine("5.  Eliminar atleta");
        Console.WriteLine("6.  Mostrar todos los atletas");
        Console.WriteLine("7.  Mostrar atletas por disciplina");
        Console.WriteLine("8.  Asignar medalla");
        Console.WriteLine("9.  Mostrar podio por disciplina");
        Console.WriteLine("10. Mostrar medallero general");
        Console.WriteLine("11. Demostrar control de duplicados");
        Console.WriteLine("12. Medir tiempo de ejecución");
        Console.WriteLine("13. Salir");
        Console.WriteLine("==============================================");
    }

    static void CargarDatosDemostracion()
    {
        sistema.RegistrarAtleta(
            new Atleta(
                "EC001",
                "María Chalán",
                "Ecuador",
                "Atletismo"));

        sistema.RegistrarAtleta(
            new Atleta(
                "EC002",
                "Luis Vargas",
                "Ecuador",
                "Atletismo"));

        sistema.RegistrarAtleta(
            new Atleta(
                "PE001",
                "Ana Quispe",
                "Perú",
                "Atletismo"));

        sistema.RegistrarAtleta(
            new Atleta(
                "CO001",
                "Julián Pérez",
                "Colombia",
                "Natación"));

        sistema.RegistrarAtleta(
            new Atleta(
                "EC003",
                "Pedro Suárez",
                "Ecuador",
                "Natación"));

        sistema.AsignarMedalla(
            "Atletismo",
            PosicionPodio.Oro,
            "EC001");

        sistema.AsignarMedalla(
            "Atletismo",
            PosicionPodio.Plata,
            "PE001");

        sistema.AsignarMedalla(
            "Atletismo",
            PosicionPodio.Bronce,
            "EC002");

        sistema.AsignarMedalla(
            "Natación",
            PosicionPodio.Oro,
            "EC003");

        sistema.AsignarMedalla(
            "Natación",
            PosicionPodio.Plata,
            "CO001");
    }

    static void MostrarDisciplinas()
    {
        Console.WriteLine("=== DISCIPLINAS HABILITADAS ===\n");

        foreach (string disciplina
                 in sistema.ObtenerDisciplinasHabilitadas())
        {
            Console.WriteLine($"- {disciplina}");
        }
    }

    static void RegistrarAtleta()
    {
        Console.WriteLine("=== REGISTRO DE ATLETA ===\n");

        Console.Write("ID: ");
        string id = Console.ReadLine() ?? "";

        Console.Write("Nombre: ");
        string nombre = Console.ReadLine() ?? "";

        Console.Write("País: ");
        string pais = Console.ReadLine() ?? "";

        Console.Write("Disciplina: ");
        string disciplina = Console.ReadLine() ?? "";

        Atleta atleta =
            new(id, nombre, pais, disciplina);

        bool registrado =
            sistema.RegistrarAtleta(atleta);

        if (registrado)
            Console.WriteLine(
                "\nAtleta registrado correctamente.");
        else
            Console.WriteLine(
                "\nNo se pudo registrar. Verifique el ID o la disciplina.");
    }

    static void ConsultarAtleta()
    {
        Console.WriteLine("=== CONSULTA DE ATLETA ===\n");

        Console.Write("Ingrese el ID: ");
        string id = Console.ReadLine() ?? "";

        Atleta? atleta =
            sistema.ConsultarAtleta(id);

        if (atleta != null)
            Console.WriteLine($"\n{atleta}");
        else
            Console.WriteLine("\nAtleta no encontrado.");
    }

    static void ActualizarAtleta()
    {
        Console.WriteLine("=== ACTUALIZACIÓN DE ATLETA ===\n");

        Console.Write("ID del atleta: ");
        string id = Console.ReadLine() ?? "";

        Console.Write("Nuevo nombre: ");
        string nombre = Console.ReadLine() ?? "";

        Console.Write("Nuevo país: ");
        string pais = Console.ReadLine() ?? "";

        Console.Write("Nueva disciplina: ");
        string disciplina = Console.ReadLine() ?? "";

        bool actualizado =
            sistema.ActualizarAtleta(
                id,
                nombre,
                pais,
                disciplina);

        Console.WriteLine(
            actualizado
                ? "\nAtleta actualizado correctamente."
                : "\nNo se pudo actualizar el atleta.");
    }

    static void EliminarAtleta()
    {
        Console.WriteLine("=== ELIMINACIÓN DE ATLETA ===\n");

        Console.Write("Ingrese el ID: ");
        string id = Console.ReadLine() ?? "";

        bool eliminado =
            sistema.EliminarAtleta(id);

        Console.WriteLine(
            eliminado
                ? "\nAtleta eliminado correctamente."
                : "\nAtleta no encontrado.");
    }

    static void MostrarTodosLosAtletas()
    {
        Console.WriteLine("=== ATLETAS REGISTRADOS ===\n");

        var atletas =
            sistema.ObtenerTodosLosAtletas().ToList();

        if (atletas.Count == 0)
        {
            Console.WriteLine("No existen atletas registrados.");
            return;
        }

        foreach (Atleta atleta in atletas)
            Console.WriteLine(atleta);
    }

    static void MostrarAtletasPorDisciplina()
    {
        Console.WriteLine("=== ATLETAS POR DISCIPLINA ===\n");

        Console.Write("Ingrese la disciplina: ");
        string disciplina = Console.ReadLine() ?? "";

        var atletas =
            sistema.ListarPorDisciplina(disciplina).ToList();

        if (atletas.Count == 0)
        {
            Console.WriteLine(
                "\nNo existen atletas en esa disciplina.");
            return;
        }

        Console.WriteLine();

        foreach (Atleta atleta in atletas)
            Console.WriteLine(atleta);
    }

    static void AsignarMedalla()
    {
        Console.WriteLine("=== ASIGNACIÓN DE MEDALLA ===\n");

        Console.Write("Disciplina: ");
        string disciplina = Console.ReadLine() ?? "";

        Console.Write("ID del atleta: ");
        string id = Console.ReadLine() ?? "";

        Console.WriteLine("\n1. Oro");
        Console.WriteLine("2. Plata");
        Console.WriteLine("3. Bronce");

        Console.Write("\nSeleccione la posición: ");

        if (!int.TryParse(
                Console.ReadLine(),
                out int numeroPosicion)
            || numeroPosicion < 1
            || numeroPosicion > 3)
        {
            Console.WriteLine("\nPosición no válida.");
            return;
        }

        PosicionPodio posicion =
            (PosicionPodio)numeroPosicion;

        bool asignada =
            sistema.AsignarMedalla(
                disciplina,
                posicion,
                id);

        Console.WriteLine(
            asignada
                ? "\nMedalla asignada correctamente."
                : "\nNo se pudo asignar la medalla.");
    }

    static void MostrarPodio()
    {
        Console.WriteLine("=== PODIO POR DISCIPLINA ===\n");

        Console.Write("Ingrese la disciplina: ");
        string disciplina = Console.ReadLine() ?? "";

        var podio =
            sistema.ObtenerPodio(disciplina);

        if (podio.Count == 0)
        {
            Console.WriteLine(
                "\nNo existen resultados para esta disciplina.");
            return;
        }

        Console.WriteLine(
            $"\nPODIO DE {disciplina.ToUpper()}");
        Console.WriteLine("--------------------------------");

        foreach (PosicionPodio posicion
                 in Enum.GetValues<PosicionPodio>())
        {
            if (podio.TryGetValue(
                    posicion,
                    out Atleta? atleta))
            {
                Console.WriteLine(
                    $"{posicion}: {atleta.Nombre} - {atleta.Pais}");
            }
        }
    }

    static void MostrarMedallero()
    {
        Console.WriteLine("=== MEDALLERO GENERAL ===\n");

        var medallero =
            sistema.ObtenerMedalleroGeneral();

        Console.WriteLine(
            $"{"PAÍS",-15} {"ORO",5} {"PLATA",7} {"BRONCE",8} {"TOTAL",7}");

        Console.WriteLine(
            new string('-', 46));

        foreach (ResultadoMedallero resultado
                 in medallero)
        {
            Console.WriteLine(
                $"{resultado.Pais,-15} " +
                $"{resultado.Oro,5} " +
                $"{resultado.Plata,7} " +
                $"{resultado.Bronce,8} " +
                $"{resultado.Total,7}");
        }
    }

    static void DemostrarControlDuplicados()
    {
        Console.WriteLine(
            "=== CONTROL DE REGISTROS DUPLICADOS ===\n");

        Atleta duplicado =
            new(
                "EC001",
                "Atleta Duplicado",
                "Ecuador",
                "Atletismo");

        bool registrado =
            sistema.RegistrarAtleta(duplicado);

        Console.WriteLine(
            "Se intentó registrar nuevamente el ID EC001.");

        Console.WriteLine(
            registrado
                ? "Resultado: registro aceptado."
                : "Resultado: registro rechazado correctamente.");

        Console.WriteLine(
            "\nDictionary evita IDs duplicados y HashSet controla elementos únicos.");
    }

    static void MedirTiempoEjecucion()
    {
        Console.WriteLine(
            "=== MEDICIÓN DEL TIEMPO DE EJECUCIÓN ===\n");

        var tiempoBusqueda =
            sistema.MedirTiempoBusqueda("EC001");

        var tiempoMedallero =
            sistema.MedirTiempoMedallero();

        Console.WriteLine("Búsqueda del atleta EC001:");
        Console.WriteLine(
            $"Ticks: {tiempoBusqueda.ticks}");
        Console.WriteLine(
            $"Milisegundos: {tiempoBusqueda.milisegundos:F6}");

        Console.WriteLine();

        Console.WriteLine("Generación del medallero general:");
        Console.WriteLine(
            $"Ticks: {tiempoMedallero.ticks}");
        Console.WriteLine(
            $"Milisegundos: {tiempoMedallero.milisegundos:F6}");

        Console.WriteLine(
            "\nLos valores pueden variar entre cada ejecución.");
    }

    static void Pausar()
    {
        Console.WriteLine(
            "\nPresione ENTER para regresar al menú...");
        Console.ReadLine();
    }
}