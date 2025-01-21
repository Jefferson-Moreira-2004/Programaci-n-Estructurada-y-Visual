//5. Realizar una operación matemática según un carácter ingresado
using System;

class Program
{
    static void Main()
    {
        Console.Write("Ingresa el primer número: ");
        double num1 = double.Parse(Console.ReadLine());

        Console.Write("Ingresa el segundo número: ");
        double num2 = double.Parse(Console.ReadLine());

        Console.Write("Ingresa la operación (+, -, *, /): ");
        char operacion = Console.ReadKey().KeyChar;
        Console.WriteLine();

        switch (operacion)
        {
            case '+':
                Console.WriteLine($"Resultado: {num1 + num2}");
                break;
            case '-':
                Console.WriteLine($"Resultado: {num1 - num2}");
                break;
            case '*':
                Console.WriteLine($"Resultado: {num1 * num2}");
                break;
            case '/':
                if (num2 != 0)
                {
                    Console.WriteLine($"Resultado: {num1 / num2}");
                }
                else
                {
                    Console.WriteLine("No se puede dividir entre cero.");
                }
                break;
            default:
                Console.WriteLine("Operación inválida.");
                break;
        }
    }
}
