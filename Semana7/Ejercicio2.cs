using System;
using System.Collections.Generic;

class TorresHanoi
{
    static Stack<int> torreOrigen = new Stack<int>();
    static Stack<int> torreAuxiliar = new Stack<int>();
    static Stack<int> torreDestino = new Stack<int>();

    public static void Ejecutar2()
    {
        int numeroDiscos = 3;

        // Limpiar pilas por seguridad
        torreOrigen.Clear();
        torreAuxiliar.Clear();
        torreDestino.Clear();

        for (int i = numeroDiscos; i >= 1; i--)
        {
            torreOrigen.Push(i);
        }

        Console.WriteLine($"\n--- Inicio de Torres de Hanoi con {numeroDiscos} discos ---");
        ImprimirTorres();

        ResolverHanoi(numeroDiscos, torreOrigen, torreDestino, torreAuxiliar, "Origen", "Destino", "Auxiliar");
        Console.WriteLine("--- ¡Hanoi Completado con Éxito! ---");
    }

    static void ResolverHanoi(int n, Stack<int> origen, Stack<int> destino, Stack<int> auxiliar, string nomOrigen, string nomDestino, string nomAuxiliar)
    {
        if (n == 1)
        {
            int disco = origen.Pop();
            destino.Push(disco);
            Console.WriteLine($"Mover disco {disco} de Torre {nomOrigen} a Torre {nomDestino}");
            ImprimirTorres();
            return;
        }

        ResolverHanoi(n - 1, origen, auxiliar, destino, nomOrigen, nomAuxiliar, nomDestino);

        int discoGrande = origen.Pop();
        destino.Push(discoGrande);
        Console.WriteLine($"Mover disco {discoGrande} de Torre {nomOrigen} a Torre {nomDestino}");
        ImprimirTorres();

        ResolverHanoi(n - 1, auxiliar, destino, origen, nomAuxiliar, nomDestino, nomOrigen);
    }

    static void ImprimirTorres()
    {
        Console.WriteLine($"Torre Origen:   [{string.Join(", ", torreOrigen)}]");
        Console.WriteLine($"Torre Auxiliar: [{string.Join(", ", torreAuxiliar)}]");
        Console.WriteLine($"Torre Destino:  [{string.Join(", ", torreDestino)}]\n");
    }
}