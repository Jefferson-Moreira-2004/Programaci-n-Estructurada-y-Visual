//1. Imprimir los números pares dentro de los primeros 100 números enteros (sin usar if)
using System;

class Program
{
    static void Main()
    {
        // Usamos un bucle for para recorrer del 0 al 100
        for (int i = 0; i <= 100; i += 2) // Incrementamos de 2 en 2 para obtener solo números pares
        {
            Console.WriteLine(i);
        }
    }
}
