CREATE TABLE Usuarios (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(255) NOT NULL,  
    Tipo VARCHAR(50) CHECK (Tipo IN ('Paciente', 'Admin'))  
);

CREATE TABLE Medicos (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(255) NOT NULL, 
    Especialidad VARCHAR(100) NOT NULL  
);

CREATE TABLE Citas (
    ID INT PRIMARY KEY IDENTITY(1,1),
    UsuarioID INT,
    MedicoID INT,
    Fecha DATE NOT NULL,
    Hora TIME NOT NULL,
    Estado VARCHAR(50) CHECK (Estado IN ('Confirmada', 'Cancelada')),  -- Usamos VARCHAR en lugar de TEXT
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(ID),
    FOREIGN KEY (MedicoID) REFERENCES Medicos(ID)
);

SELECT * FROM Usuarios;
SELECT * FROM Medicos;
SELECT * FROM Citas;

INSERT INTO Medicos (Nombre, Especialidad)
VALUES 
('Dra. Pérez', 'Odontología'),
('Dr. Gómez', 'Pediatría'),
('Dra. Martínez', 'Cardiología'),
('Dra. Zambrano', 'Neurólogo'),
('Dr. Gómez', 'Médico General'),
('Dra. Moreira', 'Otorrinolaringólogo');

