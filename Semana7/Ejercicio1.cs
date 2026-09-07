using System;
using System.Collections.Generic;

class VerificarExpresion
{
    // Cambiamos el nombre a Ejecutar1 para que no choque con el Main principal
    public static void Ejecutar1()
    {
        string expresion = "{7 + (8 * 5) - [(9 - 7) + (4 + 1)]}";
        
        if (EstaBalanceada(expresion))
        {
            Console.WriteLine("Resultado Ejercicio 1: Fórmula balanceada.");
        }
        else
        {
            Console.WriteLine("Resultado Ejercicio 1: Fórmula NO balanceada.");
        }
    }

    static bool EstaBalanceada(string cadena)
    {
        Stack<char> pila = new Stack<char>();

        foreach (char caracter in cadena)
        {
            if (caracter == '(' || caracter == '{' || caracter == '[')
            {
                pila.Push(caracter);
            }
            else if (caracter == ')' || caracter == '}' || caracter == ']')
            {
                if (pila.Count == 0) return false;

                char apertura = pila.Pop();

                if ((caracter == ')' && apertura != '(') ||
                    (caracter == '}' && apertura != '{') ||
                    (caracter == ']' && apertura != '['))
                {
                    return false;
                }
            }
        }
        return pila.Count == 0;
    }
}