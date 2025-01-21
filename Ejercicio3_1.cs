using System;

class Persona
{
    private string nombre;
    private int edad;
    private string dni;

    // Constructor
    public Persona(string nombre, int edad, string dni)
    {
        this.nombre = nombre;
        this.edad = edad;
        this.dni = dni;
    }

    // Métodos para obtener y establecer valores
    public string GetNombre() => nombre;
    public void SetNombre(string nombre) => this.nombre = nombre;

    public int GetEdad() => edad;
    public void SetEdad(int edad) => this.edad = edad;

    public string GetDni() => dni;
    public void SetDni(string dni) => this.dni = dni;

    // Método Main para probar la clase
    public static void Main()
    {
        Persona persona = new Persona("Jefferson", 25, "12345678");
        Console.WriteLine($"Nombre: {persona.GetNombre()}, Edad: {persona.GetEdad()}, DNI: {persona.GetDni()}");
    }
}
