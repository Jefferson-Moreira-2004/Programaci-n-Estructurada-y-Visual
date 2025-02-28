using System;
using System.Collections.Generic;
using System.Windows.Forms;

public class Alumno
{
    public string Dni { get; set; }
    public string Apellidos { get; set; }
    public string Nombre { get; set; }
    public double Nota { get; set; }

    public string Calificacion
    {
        get
        {
            if (Nota < 5)
                return "SS";
            else if (Nota < 7)
                return "AP";
            else if (Nota < 9)
                return "NT";
            else
                return "SB";
        }
    }

    public string MostrarInfo()
    {
        return $"{Dni} {Apellidos}, {Nombre} {Nota} {Calificacion}";
    }
}

public partial class MainForm : Form
{
    private Dictionary<string, Alumno> alumnos;
    private TextBox dniTextBox, apellidosTextBox, nombreTextBox, notaTextBox;
    private Label resultLabel;

    public MainForm()
    {
        InitializeComponent();
        alumnos = new Dictionary<string, Alumno>();
    }

    private void InitializeComponent()
    {
        this.Text = "Gestión de Alumnos";
        this.ClientSize = new System.Drawing.Size(600, 400);

        // DNI
        Label dniLabel = new Label { Text = "DNI:", Location = new System.Drawing.Point(10, 10), Width = 100 };
        dniTextBox = new TextBox { Location = new System.Drawing.Point(100, 10), Width = 200 };
        this.Controls.Add(dniLabel);
        this.Controls.Add(dniTextBox);

        // Apellidos
        Label apellidosLabel = new Label { Text = "Apellidos:", Location = new System.Drawing.Point(10, 40), Width = 100 };
        apellidosTextBox = new TextBox { Location = new System.Drawing.Point(100, 40), Width = 200 };
        this.Controls.Add(apellidosLabel);
        this.Controls.Add(apellidosTextBox);

        // Nombre
        Label nombreLabel = new Label { Text = "Nombre:", Location = new System.Drawing.Point(10, 70), Width = 100 };
        nombreTextBox = new TextBox { Location = new System.Drawing.Point(100, 70), Width = 200 };
        this.Controls.Add(nombreLabel);
        this.Controls.Add(nombreTextBox);

        // Nota
        Label notaLabel = new Label { Text = "Nota:", Location = new System.Drawing.Point(10, 100), Width = 100 };
        notaTextBox = new TextBox { Location = new System.Drawing.Point(100, 100), Width = 200 };
        this.Controls.Add(notaLabel);
        this.Controls.Add(notaTextBox);

        // Botones
        Button agregarButton = new Button { Text = "Agregar Alumno", Location = new System.Drawing.Point(10, 130), Width = 120 };
        agregarButton.Click += AgregarAlumno;
        this.Controls.Add(agregarButton);

        Button eliminarButton = new Button { Text = "Eliminar Alumno", Location = new System.Drawing.Point(140, 130), Width = 120 };
        eliminarButton.Click += EliminarAlumno;
        this.Controls.Add(eliminarButton);

        Button consultarButton = new Button { Text = "Consultar Alumno", Location = new System.Drawing.Point(10, 160), Width = 120 };
        consultarButton.Click += ConsultarAlumno;
        this.Controls.Add(consultarButton);

        Button modificarButton = new Button { Text = "Modificar Nota", Location = new System.Drawing.Point(140, 160), Width = 120 };
        modificarButton.Click += ModificarNota;
        this.Controls.Add(modificarButton);

        Button mostrarButton = new Button { Text = "Mostrar Todos", Location = new System.Drawing.Point(10, 190), Width = 120 };
        mostrarButton.Click += MostrarTodos;
        this.Controls.Add(mostrarButton);

        Button suspensosButton = new Button { Text = "Suspensos", Location = new System.Drawing.Point(140, 190), Width = 120 };
        suspensosButton.Click += MostrarSuspensos;
        this.Controls.Add(suspensosButton);

        Button aprobadosButton = new Button { Text = "Aprobados", Location = new System.Drawing.Point(10, 220), Width = 120 };
        aprobadosButton.Click += MostrarAprobados;
        this.Controls.Add(aprobadosButton);

        Button mhButton = new Button { Text = "Candidatos a MH", Location = new System.Drawing.Point(140, 220), Width = 120 };
        mhButton.Click += MostrarMH;
        this.Controls.Add(mhButton);

        // Resultado
        resultLabel = new Label { Location = new System.Drawing.Point(10, 250), Width = 500, Height = 100 };
        this.Controls.Add(resultLabel);
    }

    private void AgregarAlumno(object sender, EventArgs e)
    {
        string dni = dniTextBox.Text;
        string apellidos = apellidosTextBox.Text;
        string nombre = nombreTextBox.Text;
        try
        {
            double nota = Convert.ToDouble(notaTextBox.Text);
            if (alumnos.ContainsKey(dni))
                MessageBox.Show("Alumno ya registrado.");
            else
            {
                Alumno alumno = new Alumno { Dni = dni, Apellidos = apellidos, Nombre = nombre, Nota = nota };
                alumnos[dni] = alumno;
                MessageBox.Show("Alumno agregado.");
                LimpiarCampos();
            }
        }
        catch (FormatException)
        {
            MessageBox.Show("La nota debe ser un número.");
        }
    }

    private void EliminarAlumno(object sender, EventArgs e)
    {
        string dni = dniTextBox.Text;
        if (alumnos.ContainsKey(dni))
        {
            alumnos.Remove(dni);
            MessageBox.Show("Alumno eliminado.");
            LimpiarCampos();
        }
        else
        {
            MessageBox.Show("Alumno no encontrado.");
        }
    }

    private void ConsultarAlumno(object sender, EventArgs e)
    {
        string dni = dniTextBox.Text;
        if (alumnos.ContainsKey(dni))
        {
            Alumno alumno = alumnos[dni];
            resultLabel.Text = alumno.MostrarInfo();
        }
        else
        {
            MessageBox.Show("Alumno no encontrado.");
        }
    }

    private void ModificarNota(object sender, EventArgs e)
    {
        string dni = dniTextBox.Text;
        try
        {
            double nuevaNota = Convert.ToDouble(notaTextBox.Text);
            if (alumnos.ContainsKey(dni))
            {
                alumnos[dni].Nota = nuevaNota;
                MessageBox.Show("Nota modificada.");
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Alumno no encontrado.");
            }
        }
        catch (FormatException)
        {
            MessageBox.Show("La nota debe ser un número.");
        }
    }

    private void MostrarTodos(object sender, EventArgs e)
    {
        string todos = string.Join("\n", alumnos.Values);
        resultLabel.Text = todos;
    }

    private void MostrarSuspensos(object sender, EventArgs e)
    {
        var suspensos = new List<string>();
        foreach (var alumno in alumnos.Values)
        {
            if (alumno.Nota < 5)
                suspensos.Add(alumno.MostrarInfo());
        }
        resultLabel.Text = string.Join("\n", suspensos);
    }

    private void MostrarAprobados(object sender, EventArgs e)
    {
        var aprobados = new List<string>();
        foreach (var alumno in alumnos.Values)
        {
            if (alumno.Nota >= 5)
                aprobados.Add(alumno.MostrarInfo());
        }
        resultLabel.Text = string.Join("\n", aprobados);
    }

    private void MostrarMH(object sender, EventArgs e)
    {
        var mh = new List<string>();
        foreach (var alumno in alumnos.Values)
        {
            if (alumno.Nota == 10)
                mh.Add(alumno.MostrarInfo());
        }
        resultLabel.Text = string.Join("\n", mh);
    }

    private void LimpiarCampos()
    {
        dniTextBox.Clear();
        apellidosTextBox.Clear();
        nombreTextBox.Clear();
        notaTextBox.Clear();
    }

    [STAThread]
    static void Main()
    {
        Application.Run(new MainForm());
    }
}
