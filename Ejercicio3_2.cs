using System;

class Punto
{
    private double x;
    private double y;

    // Constructor
    public Punto(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    // Métodos para obtener y establecer valores
    public double GetX() => x;
    public void SetX(double x) => this.x = x;

    public double GetY() => y;
    public void SetY(double y) => this.y = y;

    // Método Main para probar la clase
    public static void Main()
    {
        Punto punto = new Punto(3.5, 7.8);
        Console.WriteLine($"Punto en coordenadas: ({punto.GetX()}, {punto.GetY()})");
    }
}
