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
// Esta es la parte del formulario. Todavía no he agregado todas las validaciones, pero lo tengo casi listo
public class MainForm : Form
{
    private GestionAlumnos gestion = new GestionAlumnos();
    private TextBox txtDNI, txtApellidos, txtNombre, txtNota;
    private Button btnAgregar, btnMostrar;
    private ListBox lstAlumnos;

    public MainForm()
    {
        Text = "Gestión de Alumnos";
        Size = new System.Drawing.Size(400, 400);

        // Los controles están bien, aunque todavía no he agregado validación de datos
        Label lblDNI = new Label { Text = "DNI:", Top = 20, Left = 10 };
        txtDNI = new TextBox { Top = 20, Left = 100 };
        Label lblApellidos = new Label { Text = "Apellidos:", Top = 50, Left = 10 };
        txtApellidos = new TextBox { Top = 50, Left = 100 };
        Label lblNombre = new Label { Text = "Nombre:", Top = 80, Left = 10 };
        txtNombre = new TextBox { Top = 80, Left = 100 };
        Label lblNota = new Label { Text = "Nota:", Top = 110, Left = 10 };
        txtNota = new TextBox { Top = 110, Left = 100 };

        btnAgregar = new Button { Text = "Agregar Alumno", Top = 140, Left = 100 };
        btnAgregar.Click += BtnAgregar_Click;

        btnMostrar = new Button { Text = "Mostrar Alumnos", Top = 170, Left = 100 };
        btnMostrar.Click += BtnMostrar_Click;

        lstAlumnos = new ListBox { Top = 200, Left = 10, Width = 350, Height = 150 };

        Controls.Add(lblDNI);
        Controls.Add(txtDNI);
        Controls.Add(lblApellidos);
        Controls.Add(txtApellidos);
        Controls.Add(lblNombre);
        Controls.Add(txtNombre);
        Controls.Add(lblNota);
        Controls.Add(txtNota);
        Controls.Add(btnAgregar);
        Controls.Add(btnMostrar);
        Controls.Add(lstAlumnos);
    }
}
// Estos son los eventos que se activan cuando presiono los botones. Todavía falta agregar validación de entrada en los campos
private void BtnAgregar_Click(object sender, EventArgs e)
{
    try
    {
        double nota = double.Parse(txtNota.Text); // Esto puede lanzar una excepción si el valor no es un número
        var alumno = new Alumno(txtDNI.Text, txtApellidos.Text, txtNombre.Text, nota);
        gestion.AgregarAlumno(alumno);
        MessageBox.Show("Alumno agregado correctamente.");
    }
    catch (Exception ex)
    {
        // Falta manejar mejor los errores
        MessageBox.Show(ex.Message);
    }
}

private void BtnMostrar_Click(object sender, EventArgs e)
{
    lstAlumnos.Items.Clear();
    // Falta validar que haya alumnos antes de intentar mostrar algo
    foreach (var alumno in gestion.ObtenerAlumnos())
        lstAlumnos.Items.Add($"{alumno.DNI} {alumno.Apellidos}, {alumno.Nombre} {alumno.Nota} {alumno.Calificacion}");
}

