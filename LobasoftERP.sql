IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'LobasoftERP')
BEGIN
    -- Crear la base de datos EjemploDB
    CREATE DATABASE LobasoftERP;
END
GO

-- Usar la base de datos
USE LobasoftERP;
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Proveedor')
BEGIN
    CREATE TABLE Proveedor (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Nombre VARCHAR(50) NOT NULL,
        Provincia VARCHAR(50) NOT NULL,
		Canton VARCHAR(50) NOT NULL,
		Distrito VARCHAR(50) NOT NULL,
		Direccion VARCHAR(200) NOT NULL,
		Telefono VARCHAR(50) NOT NULL,
        Email VARCHAR(100) NOT NULL
    );

    -- Insertar datos de ejemplo en la tabla Usuario
    INSERT INTO Proveedor (nombre, provincia, canton, distrito,direccion, telefono,email)
    VALUES ('Suministros Industriales S.A.', 'Puntarenas', 'Central', 'El Roble','200 norte', '88888888','suministros@gmail.com');

    INSERT INTO Proveedor (nombre, provincia, canton, distrito,direccion, telefono,email)
    VALUES ('Tecnología Avanzada Ltda.', 'Puntarenas', 'Central', 'El Roble','300 norte', '88888888','tecnologia@gmail.com');
END
GO

drop table Proveedor

---- LobasAmi1234 contraseña de perfil del string de conexion
select * from Proveedor;