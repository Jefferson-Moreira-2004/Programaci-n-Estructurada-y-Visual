//2. Imprimir números del 1 al 50 con "Fizz", "Buzz" y "FizzBuzz"
using System;

class Program
{
    static void Main()
    {
        // Recorremos los números del 1 al 50
        for (int i = 1; i <= 50; i++)
        {
            // Verificamos si el número es divisible por 3 y 5
            if (i % 3 == 0 && i % 5 == 0)
            {
                Console.WriteLine("FizzBuzz");
            }
            else if (i % 3 == 0) // Si es divisible solo por 3
            {
                Console.WriteLine("Fizz");
            }
            else if (i % 5 == 0) // Si es divisible solo por 5
            {
                Console.WriteLine("Buzz");
            }
            else
            {
                Console.WriteLine(i); // Si no es divisible ni por 3 ni por 5, imprimimos el número
            }
        }
    }
}
