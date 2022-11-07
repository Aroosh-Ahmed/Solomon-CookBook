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
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220929033225_initial')
BEGIN
    CREATE TABLE [Admins] (
        [AdminId] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Admins] PRIMARY KEY ([AdminId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220929033225_initial')
BEGIN
    CREATE TABLE [Recepie_Comments] (
        [R_Comment_ID] int NOT NULL IDENTITY,
        [User_ID] int NULL,
        [Recepie_ID] int NULL,
        [Date] datetime2 NULL,
        [Comment] nvarchar(max) NULL,
        CONSTRAINT [PK_Recepie_Comments] PRIMARY KEY ([R_Comment_ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220929033225_initial')
BEGIN
    CREATE TABLE [Recepies] (
        [Recepie_ID] int NOT NULL IDENTITY,
        [Recepie_Name] nvarchar(max) NULL,
        [Category] nvarchar(max) NULL,
        [video_url] nvarchar(max) NULL,
        [image_url] nvarchar(max) NOT NULL,
        [Ingredients] nvarchar(max) NULL,
        [Likes] int NULL,
        [Directions] nvarchar(max) NULL,
        [Country] nvarchar(max) NULL,
        [type] nvarchar(max) NULL,
        [status] nvarchar(max) NULL,
        CONSTRAINT [PK_Recepies] PRIMARY KEY ([Recepie_ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220929033225_initial')
BEGIN
    CREATE TABLE [Users] (
        [User_ID] int NOT NULL IDENTITY,
        [First_name] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [Phone_number] nvarchar(max) NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([User_ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220929033225_initial')
BEGIN
    CREATE TABLE [Comments] (
        [Id] int NOT NULL IDENTITY,
        [Comment] nvarchar(max) NOT NULL,
        [RecepiesRecepie_ID] int NULL,
        CONSTRAINT [PK_Comments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Comments_Recepies_RecepiesRecepie_ID] FOREIGN KEY ([RecepiesRecepie_ID]) REFERENCES [Recepies] ([Recepie_ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220929033225_initial')
BEGIN
    CREATE TABLE [Recepie_Likes] (
        [R_like_ID] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [Recepie_ID] int NULL,
        CONSTRAINT [PK_Recepie_Likes] PRIMARY KEY ([R_like_ID]),
        CONSTRAINT [FK_Recepie_Likes_Recepies_Recepie_ID] FOREIGN KEY ([Recepie_ID]) REFERENCES [Recepies] ([Recepie_ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220929033225_initial')
BEGIN
    CREATE INDEX [IX_Comments_RecepiesRecepie_ID] ON [Comments] ([RecepiesRecepie_ID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220929033225_initial')
BEGIN
    CREATE INDEX [IX_Recepie_Likes_Recepie_ID] ON [Recepie_Likes] ([Recepie_ID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220929033225_initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220929033225_initial', N'6.0.1');
END;
GO

COMMIT;
GO

