IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'LobasoftERP')
BEGIN
    -- Crear la base de datos EjemploDB
    CREATE DATABASE LobasoftERP;
END
GO

-- Usar la base de datos
USE LobasoftERP;
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LBS_Proveedores')
BEGIN
    CREATE TABLE LBS_Proveedores (
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
    INSERT INTO LBS_Proveedores (nombre, provincia, canton, distrito,direccion, telefono,email)
    VALUES ('Suministros Industriales S.A.', 'Puntarenas', 'Central', 'El Roble','200 norte', '88888888','suministros@gmail.com');

    INSERT INTO LBS_Proveedores (nombre, provincia, canton, distrito,direccion, telefono,email)
    VALUES ('Tecnología Avanzada Ltda.', 'Puntarenas', 'Central', 'El Roble','300 norte', '88888888','tecnologia@gmail.com');
END
GO

drop table LBS_Proveedores

---- LobasAmi1234 contraseña de perfil del string de conexion
select * from LBS_Proveedores;

--Tabla de usuarios ------------------------------------------------------------
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LBS_Usuarios ')
BEGIN
    CREATE TABLE LBS_Usuarios  (
		U_idUsuario INT PRIMARY KEY IDENTITY(1,1),
		U_nombreUsuario VARCHAR(20) NOT NULL,
		U_contrasena VARCHAR(50) NOT NULL,
		U_correo VARCHAR(60) NOT NULL,
		U_rol VARCHAR(20) NOT NULL,
		U_estado VARCHAR(10) NOT NULL
);


    -- Insertar datos de ejemplo en la tabla Usuario
	INSERT INTO LBS_Usuarios (U_nombreUsuario, U_contrasena, U_correo, U_rol, U_estado)
	VALUES ('Reggy', 'SelacomeToda', 'admin@example.com', 'Admin', 'activo');


END
GO

drop table LBS_Usuarios


select * from LBS_Usuarios;



create table  LBS_AreaComercial(
	Id INT PRIMARY KEY IDENTITY(1,1),
    NombreAreaComercial VARCHAR(200) NOT NULL,
    Descripcion VARCHAR(200) NOT NULL
)
go

drop table LBS_AreaComercial;

