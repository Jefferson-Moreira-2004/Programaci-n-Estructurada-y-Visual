//3. Determinar el rango de un número ingresado
using System;

class Program
{
    static void Main()
    {
        Console.Write("Ingresa un número: ");
        int numero = int.Parse(Console.ReadLine());

        if (numero < 10)
        {
            Console.WriteLine("Menor que 10");
        }
        else if (numero >= 10 && numero <= 20)
        {
            Console.WriteLine("Entre 10 y 20");
        }
        else
        {
            Console.WriteLine("Mayor que 20");
        }
    }
}
