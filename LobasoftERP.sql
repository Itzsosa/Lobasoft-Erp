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

    -- Insertar datos de ejemplo en la tabla Proveedores
    INSERT INTO LBS_Proveedores (nombre, provincia, canton, distrito,direccion, telefono,email)
    VALUES ('Suministros Industriales S.A.', 'Puntarenas', 'Central', 'El Roble','200 norte', '88888888','brandonchf13@gmail.com');

    INSERT INTO LBS_Proveedores (nombre, provincia, canton, distrito,direccion, telefono,email)
    VALUES ('Pescadores del Pac�fico', 'Puntarenas', 'Central', 'El Roble','300 norte', '88888888','tecnologia@gmail.com'),
	    			('Tecnolog�a Avanzada Ltda.', 'Puntarenas', 'Central', 'El Roble','300 norte', '88888888','tecnologia@gmail.com'),
	      		('Queser�a Queso Badilla', 'Alajuela', 'San Carlos', 'Pocosol','300 norte', '88888888','quesos@gmail.com'),
	      		('Carnes el Pedazo', 'Cartago', 'Central', 'El Roble','300 norte', '88888888','carnes@gmail.com');
END
GO

Delete LBS_Proveedores
drop table LBS_Proveedores

---- LobasAmi1234 contrase�a de perfil del string de conexion
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
	VALUES ('Reggy', 'admin', 'admin@example.com', 'Admin', 'activo');

	INSERT INTO LBS_Usuarios (U_nombreUsuario, U_contrasena, U_correo, U_rol, U_estado)
	VALUES ('Brandon', 'bran123', 'brandonchavarria13@gmail.com', 'Cliente', 'activo');

	INSERT INTO LBS_Usuarios (U_nombreUsuario, U_contrasena, U_correo, U_rol, U_estado)
	VALUES ('Itzsosa', 'Sosa1234', 'lmausosa23@gmail.com', 'Cliente', 'Activo');
END
GO

drop table LBS_Usuarios
select * from LBS_Usuarios;


--Tabla de LBS_AreaComercial
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LBS_AreaComercial')
BEGIN
    create table  LBS_AreaComercial(
	Id INT PRIMARY KEY IDENTITY(1,1),
    NombreAreaComercial VARCHAR(200) NOT NULL,
    Descripcion VARCHAR(200) NOT NULL);

	INSERT INTO LBS_AreaComercial (NombreAreaComercial,Descripcion)
	VALUES ('Pescaderia', 'Area comercial de pescaderos'),
	       ('Lacteos', 'Area comercial de Lacteos'),
		   ('Carnicer�a', 'Area comercial de carnes');
END
GO
Delete from LBS_AreaComercial
select * from LBS_AreaComercial

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LBS_AsignacionAreaProveedor')
BEGIN
    create table  LBS_AsignacionAreaProveedor(
	Id INT PRIMARY KEY IDENTITY(1,1),
    A_idAreaComercial INT NOT NULL,
    A_idProveedor INT NOT NULL,
	FOREIGN KEY (A_idAreaComercial) REFERENCES LBS_AreaComercial(Id) ON DELETE CASCADE,
    FOREIGN KEY (A_idProveedor) REFERENCES LBS_Proveedores(Id) ON DELETE CASCADE
	);

	INSERT INTO LBS_AsignacionAreaProveedor (A_idAreaComercial,A_idProveedor)
	VALUES ((Select id from LBS_AreaComercial where id = 1), (Select id from LBS_AreaComercial where id = 1));

	INSERT INTO LBS_AsignacionAreaProveedor (A_idAreaComercial,A_idProveedor)
	VALUES (2,3),(3,4),(1,5);
	--INSERT INTO LBS_AsignacionAreaProveedor (A_idAreaComercial,A_idProveedor)
	--VALUES ((Select id from LBS_AreaComercial where id = 1), (Select id from LBS_AreaComercial where id = 1));
END
--Tabla de Ordenes ------------------------------------------------------------
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LBS_Usuarios ')
BEGIN
    CREATE TABLE LBS_Ordenes  (
		O_IdOrden INT PRIMARY KEY IDENTITY(1,1),
		O_IdProveedor INT,
		O_IdUsuario INT,
		O_Fecha DATETIME NOT NULL DEFAULT getdate(),
		O_Asunto VARCHAR(MAX) NOT NULL,
		O_Descripcion VARCHAR(MAX) NOT NULL,
		FOREIGN KEY (O_IdProveedor) REFERENCES LBS_Proveedores(Id) ON DELETE CASCADE,
		FOREIGN KEY (O_IdUsuario) REFERENCES LBS_Usuarios(U_idUsuario) ON DELETE CASCADE
);
END

INSERT INTO LBS_Ordenes(O_Asunto,O_Descripcion,O_idProveedor,O_idUsuario)
	VALUES ('Pedido Mariscos', '30kg PG 15kg Corvina',1,2),
	       ('Pedido Muebles Salon', 'Un saludo les hablo de la empresa X, me gustaria contactarme con el encargado de ventas para ver el catalogo de sus productos. ya que se necesitan encargar mobiliarios para una sucursal nueva',2,2),
		   ('Pedido Mariscos', 'quieria saber si tienen pulpo de parte de la empresa X',1,3);
-----------------------------------------------------------------------------------------------------------------------
select * from LBS_AreaComercial
select * from LBS_Proveedores
select * from LBS_Usuarios
select * from LBS_AsignacionAreaProveedor
select * from LBS_Ordenes




Drop table LBS_AsignacionAreaProveedor
-----------------------------------------------------------------------------------------------------------------------

-----------------------------------------------------------------------------------------------------------------------
-----------------------------Procedimientos almacenados----------------------------------------------------------------

--Procedimiento que filtra los proveedores por Area Comercial, Provincia, Canton, Distrito 
CREATE PROCEDURE Sp_FiltroProveedores
    @areaComercialId INT = NULL,
    @provincia VARCHAR(50) = NULL,
    @canton VARCHAR(50) = NULL,
    @distrito VARCHAR(50) = NULL
AS
BEGIN
    SELECT DISTINCT p.Id, p.Nombre, p.Provincia,  p.Email
    FROM LBS_Proveedores p
    INNER JOIN LBS_AsignacionAreaProveedor a ON p.Id = a.A_idProveedor
    WHERE (@areaComercialId IS NULL OR a.A_idAreaComercial = @areaComercialId)
        AND (@provincia IS NULL OR p.Provincia = @provincia)
        AND (@canton IS NULL OR p.Canton = @canton)
        AND (@distrito IS NULL OR p.Distrito = @distrito)
END

EXEC Sp_FiltroProveedores @areaComercialId = 1;
EXEC Sp_FiltroProveedores @areaComercialId =2, @provincia='Puntarenas', @canton=null, @distrito=null

--Procedimiento que retorna las ordenes por usuario
CREATE PROCEDURE SP_OrdenesPorUsuario
    @UsuarioCorreo VARCHAR(60)
AS
BEGIN
    SELECT o.O_idOrden AS Id,
		   p.Nombre AS Nombre,
		   p.Email AS Email,
           o.O_Asunto AS Asunto,
           o.O_Descripcion AS Descripcion,
           o.O_Fecha AS Fecha
    FROM LBS_Ordenes o
	INNER JOIN LBS_Usuarios u ON o.O_IdUsuario = u.U_idUsuario
    INNER JOIN LBS_Proveedores p ON o.O_IdProveedor = p.Id
    WHERE u.U_correo = @UsuarioCorreo
END


EXEC SP_OrdenesPorUsuario @UsuarioCorreo= 'brandonchavarria13@gmail.com';