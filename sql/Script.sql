DROP DATABASE IF EXISTS BdiExamen
CREATE DATABASE BdiExamen
USE BdiExamen

CREATE TABLE tblExamen (
    idExamen INT PRIMARY KEY IDENTITY (1, 1),
    nombre VARCHAR (255),
    descripcion VARCHAR (255)
);


--DROP PROCEDURE IF EXISTS sp_InsertExamen
CREATE PROCEDURE sp_InsertExamen
    @nombre VARCHAR(255),
    @descripcion VARCHAR(255),
    @codigoRetorno INT OUTPUT,
    @descripcionRetorno VARCHAR(255) OUTPUT,
	@idExamen INT OUTPUT
AS
BEGIN
    BEGIN TRY
        INSERT INTO tblExamen (nombre, descripcion)
        VALUES (@nombre, @descripcion);

        SET @codigoRetorno = 0;
        SET @descripcionRetorno = 'Registro insertado satisfactoriamente.';
		SET @idExamen = SCOPE_IDENTITY();
    END TRY
    BEGIN CATCH
        SET @codigoRetorno = ERROR_NUMBER();
        SET @descripcionRetorno = ERROR_MESSAGE();
		SET @idExamen = -1;
    END CATCH;
END;


--DROP PROCEDURE IF EXISTS sp_UpdateExamen
CREATE PROCEDURE sp_UpdateExamen
    @idExamen INT,
    @nombre VARCHAR(255),
    @descripcion VARCHAR(255),
	@codigoRetorno INT OUTPUT,
    @descripcionRetorno VARCHAR(255) OUTPUT
AS
BEGIN
BEGIN TRY
    UPDATE tblExamen
    SET nombre = @nombre,
        descripcion = @descripcion
    WHERE idExamen = @idExamen;

	SET @codigoRetorno = 0;
    SET @descripcionRetorno = 'Registro actualizado satisfactoriamente.';
	END TRY
    BEGIN CATCH
		SET @codigoRetorno = ERROR_NUMBER();
		SET @descripcionRetorno = ERROR_MESSAGE();
    END CATCH;
END;


--DROP PROCEDURE IF EXISTS sp_GetExamen
CREATE PROCEDURE sp_GetExamen
    @idExamen INT = NULL
AS
BEGIN
    IF @idExamen IS NULL
    BEGIN
        -- Consultar todos los registros
        SELECT * FROM tblExamen;
    END
    ELSE
    BEGIN
        -- Consultar un registro específico
        SELECT * FROM tblExamen WHERE idExamen = @idExamen;
    END
END;

--DROP PROCEDURE IF EXISTS sp_DeleteExamen
CREATE PROCEDURE sp_DeleteExamen
    @idExamen INT,
	@codigoRetorno INT OUTPUT,
    @descripcionRetorno VARCHAR(255) OUTPUT
AS
BEGIN
BEGIN TRY
    DELETE FROM tblExamen
    WHERE idExamen = @idExamen;

	SET @codigoRetorno = 0;
    SET @descripcionRetorno = 'Registro eliminado.';
	END TRY
    BEGIN CATCH
		SET @codigoRetorno = ERROR_NUMBER();
		SET @descripcionRetorno = ERROR_MESSAGE();
    END CATCH;
END;



INSERT INTO tblExamen (nombre, descripcion)
VALUES ('Español', 'Examen de español.');

INSERT INTO tblExamen (nombre, descripcion)
VALUES ('Ciencias naturales', 'Examen de ciencias naturales.');
