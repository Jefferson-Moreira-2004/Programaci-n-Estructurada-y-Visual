//4. Calcular la suma de los primeros 100 números enteros usando un buclewhile
using System;

class Program
{
    static void Main()
    {
        int suma = 0;
        int i = 1;
        while (i <= 100) // Mientras i sea menor o igual a 100
        {
            suma += i; // Agregamos i a la suma
            i++; // Incrementamos i
        }
        Console.WriteLine($"La suma de los primeros 100 numeros es: {suma}");
    }
}
