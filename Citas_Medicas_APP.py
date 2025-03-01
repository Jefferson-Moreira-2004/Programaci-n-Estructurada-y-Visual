from google_auth_oauthlib.flow import InstalledAppFlow
from googleapiclient.discovery import build
import tkinter as tk
from tkinter import messagebox, ttk
from tkcalendar import Calendar
import os
import pickle

# Datos temporales
usuarios = [
    {"id": 1, "nombre": "Paciente 1", "tipo": "Paciente"},
    {"id": 2, "nombre": "Admin", "tipo": "Admin"}
]

medicos = [
    {"id": 1, "nombre": "Dra. Pérez", "especialidad": "Odontología"},
    {"id": 2, "nombre": "Dr. Gómez", "especialidad": "Pediatría"},
    {"id": 3, "nombre": "Dra. Martínez", "especialidad": "Cardiología"},
    {"id": 4, "nombre": "Dra. Zambrano", "especialidad": "Neurólogo"},
    {"id": 5, "nombre": "Dr. Gómez", "especialidad": "Médico General"},
    {"id": 6, "nombre": "Dra. Moreira", "especialidad": "Otorrinolaringólogo"}
]

citas = []

def iniciar_sesion():
    usuario_id = entry_usuario.get()
    usuario = next((u for u in usuarios if str(u["id"]) == usuario_id), None)
    if usuario:
        if usuario["tipo"] == "Admin":
            abrir_pantalla_admin()
        else:
            abrir_pantalla_paciente(usuario)
    else:
        messagebox.showerror("Error", "Usuario no encontrado")

def abrir_pantalla_admin():
    limpiar_ventana()
    tk.Label(ventana, text="Bienvenido, Administrador", font=("Times New Roman", 14)).pack(pady=10)
    tk.Button(ventana, text="Ver todas las citas", command=ver_citas_admin).pack(pady=10)
    tk.Button(ventana, text="Agendar cita", command=agendar_cita).pack(pady=10)

def abrir_pantalla_paciente(usuario):
    limpiar_ventana()
    tk.Label(ventana, text=f"Bienvenido, {usuario['nombre']}", font=("Times New Roman", 14)).pack(pady=10)
    tk.Button(ventana, text="Ver mis citas", command=lambda: ver_citas_paciente(usuario["id"])).pack(pady=10)
    tk.Button(ventana, text="Agendar cita", command=agendar_cita).pack(pady=10)

def ver_citas_admin():
    limpiar_ventana()
    tk.Label(ventana, text="Citas Programadas", font=("Times New Roman", 14)).pack(pady=10)
    for cita in citas:
        tk.Label(ventana, text=f"Paciente: {cita['paciente']} - Médico: {cita['medico']} ({cita['especialidad']}) - Fecha: {cita['fecha']} - Hora: {cita['hora']}", font=("Times New Roman", 12)).pack()
    
    # Botón para editar citas
    tk.Button(ventana, text="Editar Cita", command=editar_cita_admin).pack(pady=10)

def ver_citas_paciente(paciente_id):
    limpiar_ventana()
    tk.Label(ventana, text="Mis Citas", font=("Times New Roman", 14)).pack(pady=10)
    citas_paciente = [cita for cita in citas if cita["paciente_id"] == paciente_id]
    seleccion = tk.Listbox(ventana, selectmode=tk.MULTIPLE, font=("Times New Roman", 12))
    for i, cita in enumerate(citas_paciente):
        seleccion.insert(i, f"Médico: {cita['medico']} ({cita['especialidad']}) - Fecha: {cita['fecha']} - Hora: {cita['hora']}")
    seleccion.pack()

    # Botón para editar citas
    tk.Button(ventana, text="Editar Cita", command=lambda: editar_cita_paciente(seleccion, paciente_id)).pack(pady=10)
    
    def cancelar_cita():
        global citas
        indices = seleccion.curselection()
        if not indices:
            messagebox.showwarning("Advertencia", "Seleccione al menos una cita para cancelar.")
            return
        
        citas_a_cancelar = [citas_paciente[i] for i in indices]
        citas = [cita for cita in citas if cita not in citas_a_cancelar]
        messagebox.showinfo("Cancelación", "Cita(s) cancelada(s) exitosamente.")
        abrir_pantalla_paciente(next(u for u in usuarios if u["id"] == paciente_id))
    
    tk.Button(ventana, text="Cancelar Cita", command=cancelar_cita).pack(pady=10)
    tk.Button(ventana, text="Volver", command=lambda: abrir_pantalla_paciente(next(u for u in usuarios if u["id"] == paciente_id))).pack(pady=5)

def editar_cita_admin():
    # Obtener una lista de todas las citas para que el administrador pueda editarlas
    limpiar_ventana()
    tk.Label(ventana, text="Editar Cita", font=("Times New Roman", 14)).pack(pady=10)
    
    # Crear una lista desplegable para que el admin pueda seleccionar la cita a editar
    cita_var = tk.StringVar()
    citas_dropdown = ttk.Combobox(ventana, textvariable=cita_var)
    citas_dropdown['values'] = [f"Paciente: {cita['paciente']} - Médico: {cita['medico']} ({cita['especialidad']}) - Fecha: {cita['fecha']} - Hora: {cita['hora']}" for cita in citas]
    citas_dropdown.pack(pady=5)

    def confirmar_editar_cita_admin():
        cita_seleccionada = citas_dropdown.get()
        if not cita_seleccionada:
            messagebox.showwarning("Advertencia", "Debe seleccionar una cita para editar.")
            return
        
        # Encontrar la cita seleccionada en la lista de citas
        cita_id = citas_dropdown.current()
        cita_a_editar = citas[cita_id]
        
        # Similar a la edición del paciente, cargar los datos de la cita seleccionada
        tk.Label(ventana, text="Seleccionar Médico:").pack()
        medico_var = tk.StringVar()
        medico_dropdown = ttk.Combobox(ventana, textvariable=medico_var)
        medico_dropdown['values'] = [f"{m['nombre']} - {m['especialidad']}" for m in medicos]
        medico_dropdown.set(f"{cita_a_editar['medico']} - {cita_a_editar['especialidad']}")
        medico_dropdown.pack(pady=5)
        
        tk.Label(ventana, text="Seleccionar Fecha:").pack()
        calendario = Calendar(ventana)
        calendario.set_date(cita_a_editar['fecha'])
        calendario.pack(pady=5)
        
        tk.Label(ventana, text="Seleccionar Hora:").pack()
        hora_var = tk.StringVar()
        hora_dropdown = ttk.Combobox(ventana, textvariable=hora_var)
        hora_dropdown['values'] = [f"{h}:00" for h in range(8, 18)]
        hora_dropdown.set(cita_a_editar['hora'])
        hora_dropdown.pack(pady=5)

        def confirmar_editar_cita():
            medico = medico_var.get()
            fecha = calendario.get_date()
            hora = hora_var.get()
            if medico and fecha and hora:
                cita_a_editar['medico'] = medico.split(" - ")[0]
                cita_a_editar['especialidad'] = medico.split(" - ")[1]
                cita_a_editar['fecha'] = fecha
                cita_a_editar['hora'] = hora
                messagebox.showinfo("Cita Editada", "Cita editada correctamente.")
                ver_citas_admin()
            else:
                messagebox.showerror("Error", "Debe seleccionar un médico, una fecha y una hora.")
        
        tk.Button(ventana, text="Confirmar Edición", command=confirmar_editar_cita).pack(pady=10)
    
    tk.Button(ventana, text="Volver", command=ver_citas_admin).pack(pady=5)

def editar_cita_paciente(seleccion, paciente_id):
    indices = seleccion.curselection()
    if not indices:
        messagebox.showwarning("Advertencia", "Seleccione al menos una cita para editar.")
        return
    
    cita_seleccionada = citas[indices[0]]  # Se obtiene la primera cita seleccionada
    
    # Cargar los datos de la cita seleccionada en los campos correspondientes
    limpiar_ventana()
    tk.Label(ventana, text="Editar Cita", font=("Times New Roman", 14)).pack(pady=10)
    
    tk.Label(ventana, text="Seleccionar Médico:").pack()
    medico_var = tk.StringVar()
    medico_dropdown = ttk.Combobox(ventana, textvariable=medico_var)
    medico_dropdown['values'] = [f"{m['nombre']} - {m['especialidad']}" for m in medicos]
    medico_dropdown.set(f"{cita_seleccionada['medico']} - {cita_seleccionada['especialidad']}")
    medico_dropdown.pack(pady=5)
    
    tk.Label(ventana, text="Seleccionar Fecha:").pack()
    calendario = Calendar(ventana)
    calendario.set_date(cita_seleccionada['fecha'])
    calendario.pack(pady=5)
    
    tk.Label(ventana, text="Seleccionar Hora:").pack()
    hora_var = tk.StringVar()
    hora_dropdown = ttk.Combobox(ventana, textvariable=hora_var)
    hora_dropdown['values'] = [f"{h}:00" for h in range(8, 18)]
    hora_dropdown.set(cita_seleccionada['hora'])
    hora_dropdown.pack(pady=5)
    
    def confirmar_editar_cita():
        medico = medico_var.get()
        fecha = calendario.get_date()
        hora = hora_var.get()
        if medico and fecha and hora:
            cita_seleccionada['medico'] = medico.split(" - ")[0]
            cita_seleccionada['especialidad'] = medico.split(" - ")[1]
            cita_seleccionada['fecha'] = fecha
            cita_seleccionada['hora'] = hora
            messagebox.showinfo("Cita Editada", "Cita editada correctamente.")
            abrir_pantalla_paciente(next(u for u in usuarios if u["id"] == paciente_id))
        else:
            messagebox.showerror("Error", "Debe seleccionar un médico, una fecha y una hora.")
    
    tk.Button(ventana, text="Confirmar Edición", command=confirmar_editar_cita).pack(pady=10)
    tk.Button(ventana, text="Volver", command=lambda: abrir_pantalla_paciente(next(u for u in usuarios if u["id"] == paciente_id))).pack(pady=5)

def agendar_cita():
    limpiar_ventana()
    tk.Label(ventana, text="Agendar Cita", font=("Times New Roman", 14)).pack(pady=10)
    
    tk.Label(ventana, text="Seleccionar Médico:").pack()
    medico_var = tk.StringVar()
    medico_dropdown = ttk.Combobox(ventana, textvariable=medico_var)
    medico_dropdown['values'] = [f"{m['nombre']} - {m['especialidad']}" for m in medicos]
    medico_dropdown.pack(pady=5)
    
    tk.Label(ventana, text="Seleccionar Fecha:").pack()
    calendario = Calendar(ventana)
    calendario.pack(pady=5)
    
    tk.Label(ventana, text="Seleccionar Hora:").pack()
    hora_var = tk.StringVar()
    hora_dropdown = ttk.Combobox(ventana, textvariable=hora_var)
    hora_dropdown['values'] = [f"{h}:00" for h in range(8, 18)]
    hora_dropdown.pack(pady=5)
    
    def confirmar_cita():
        medico = medico_var.get()
        fecha = calendario.get_date()
        hora = hora_var.get()
        if medico and fecha and hora:
            citas.append({"paciente": "Paciente 1", "medico": medico.split(" - ")[0], "especialidad": medico.split(" - ")[1], "fecha": fecha, "hora": hora, "paciente_id": 1})
            messagebox.showinfo("Cita Agendada", "Cita confirmada correctamente.")
            abrir_pantalla_paciente(usuarios[0])
        else:
            messagebox.showerror("Error", "Debe seleccionar un médico, una fecha y una hora.")
    
    tk.Button(ventana, text="Confirmar Cita", command=confirmar_cita).pack(pady=10)
    tk.Button(ventana, text="Volver", command=lambda: abrir_pantalla_paciente(usuarios[0])).pack(pady=5)

def limpiar_ventana():
    for widget in ventana.winfo_children():
        widget.destroy()

# Configuración de la ventana principal
ventana = tk.Tk()
ventana.title("Agenda de Citas Médicas")
ventana.geometry("650x500")

# Pantalla de inicio de sesión
tk.Label(ventana, text="Iniciar Sesión", font=("Times New Roman", 14)).pack(pady=10)
tk.Label(ventana, text="Usuario ID").pack()
entry_usuario = tk.Entry(ventana)
entry_usuario.pack(pady=5)
tk.Button(ventana, text="Iniciar Sesión", command=iniciar_sesion).pack(pady=10)

ventana.mainloop()