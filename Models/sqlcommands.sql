CREATE DATABASE [ComputerLocationDB];



CREATE TABLE [ComputerLocation] (
          [ID] int NOT NULL IDENTITY,
          [ComputerName] nvarchar(max) NOT NULL,
          [UserName] nvarchar(max) NOT NULL,
          [Latitude] float NOT NULL,
          [Longitude] float NOT NULL,
          [SavedTime] datetime2 NOT NULL,
          CONSTRAINT [PK_ComputerLocation] PRIMARY KEY ([ID])
      );
