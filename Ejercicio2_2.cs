//2. Mostrar mensaje según la calificación ingresada
using System;

class Program
{
    static void Main()
    {
        Console.Write("Ingresa una calificación: ");
        int calificacion = int.Parse(Console.ReadLine());

        if (calificacion >= 60)
        {
            Console.WriteLine("Aprobado");
        }
        else
        {
            Console.WriteLine("Reprobado");
        }
    }
}
