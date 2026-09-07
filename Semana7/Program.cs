using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("========================================");
        Console.WriteLine("   PRÁCTICA DE PILAS (STACK) - SEMANA 7  ");
        Console.WriteLine("========================================\n");

        // Ejecuta la verificación de paréntesis
        VerificarExpresion.Ejecutar1();

        // Ejecuta el algoritmo de Hanoi
        TorresHanoi.Ejecutar2();
    }
}