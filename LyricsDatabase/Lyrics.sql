CREATE TABLE [dbo].[Lyrics]
(
	[Id] INT NOT NULL PRIMARY KEY identity,
	[Artist] nvarchar(100) not null,
	[Song] nvarchar(100),
	[Lyric] nvarchar(max) not null,
	[Rating] int
)
