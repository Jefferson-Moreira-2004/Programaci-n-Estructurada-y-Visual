using System;

class Circulo
{
    private double x;
    private double y;
    private double radio;

    // Constructor
    public Circulo(double x, double y, double radio)
    {
        this.x = x;
        this.y = y;
        this.radio = radio;
    }

    // Métodos para obtener y establecer valores
    public double GetX() => x;
    public void SetX(double x) => this.x = x;

    public double GetY() => y;
    public void SetY(double y) => this.y = y;

    public double GetRadio() => radio;
    public void SetRadio(double radio) => this.radio = radio;

    // Método para calcular el área del círculo
    public double CalcularArea() => Math.PI * radio * radio;

    // Método Main para probar la clase
    public static void Main()
    {
        Circulo circulo = new Circulo(0, 0, 5);
        Console.WriteLine($"Círculo en ({circulo.GetX()}, {circulo.GetY()}), Radio: {circulo.GetRadio()}, Área: {circulo.CalcularArea()}");
    }
}
