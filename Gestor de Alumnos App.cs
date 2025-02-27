// Empecé creando la interfaz para las operaciones básicas de alumnos.
public interface IAlumnoOperations
{
    void AgregarAlumno(Alumno alumno);
    void EliminarAlumno(string dni);
    Alumno BuscarAlumno(string dni);
    List<Alumno> ObtenerAlumnos();
}

// Clase base Persona - Por ahora, lo tengo sencillo.
public class Persona
{
    public string DNI { get; set; }
    public string Apellidos { get; set; }
    public string Nombre { get; set; }
}

// Clase Alumno que hereda de Persona
public class Alumno : Persona
{
    public double Nota { get; set; }
    public string Calificacion { get; private set; }

    public Alumno(string dni, string apellidos, string nombre, double nota)
    {
        DNI = dni;
        Apellidos = apellidos;
        Nombre = nombre;
        Nota = nota;
        CalcularCalificacion();
    }

    public void CalcularCalificacion()
    {
        if (Nota < 5) Calificacion = "SS";
        else if (Nota < 7) Calificacion = "AP";
        else if (Nota < 9) Calificacion = "NT";
        else Calificacion = "SB";
    }
}
// Implementación de la gestión de alumnos, aunque todavía me falta validar algunos casos
public class GestionAlumnos : IAlumnoOperations
{
    private List<Alumno> alumnos = new List<Alumno>();

    public void AgregarAlumno(Alumno alumno)
    {
        // Falta la validación para duplicados
        if (!alumnos.Any(a => a.DNI == alumno.DNI))
            alumnos.Add(alumno);
        else
            throw new Exception("El alumno con este DNI ya existe.");
    }

    public void EliminarAlumno(string dni)
    {
        // Aquí todavía no he validado si el alumno existe
        var alumno = BuscarAlumno(dni);
        if (alumno != null) alumnos.Remove(alumno);
    }

    public Alumno BuscarAlumno(string dni)
    {
        // Falta que maneje si el alumno no se encuentra
        return alumnos.FirstOrDefault(a => a.DNI == dni);
    }

    public List<Alumno> ObtenerAlumnos()
    {
        return alumnos; // Esto ya está funcionando bien
    }
}
