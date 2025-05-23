IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Person] (
    [ID] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NOT NULL,
    [ApellidoPaterno] nvarchar(max) NOT NULL,
    [ApellidoMaterno] nvarchar(max) NOT NULL,
    [FechaNacimiento] datetime2 NOT NULL,
    [Sexo] nvarchar(1) NOT NULL,
    CONSTRAINT [PK_Person] PRIMARY KEY ([ID])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250523030053_InitialCreate', N'9.0.5');

COMMIT;
GO

