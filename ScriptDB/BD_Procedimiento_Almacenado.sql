CREATE   PROCEDURE [dbo].[SP_OBTENER_TRABAJADORES]
	 @Nombre VARCHAR(150)
AS
BEGIN
    SELECT
       *
    FROM dbo.Trabajador AS T
	WHERE T.Nombres LIKE '%'+ @Nombre +'%'
END;
GO