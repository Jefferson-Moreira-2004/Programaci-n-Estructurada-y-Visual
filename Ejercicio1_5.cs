//5. Imprimir los números pares dentro de los primeros 100 números enteros usando un buclewhile
using System;

class Program
{
    static void Main()
    {
        int i = 0;
        while (i <= 100) // Mientras i sea menor o igual a 100
        {
            Console.WriteLine(i); // Imprimimos el número
            i += 2; // Incrementamos en 2 para obtener solo números pares
        }
    }
}
