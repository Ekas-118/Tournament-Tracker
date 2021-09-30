CREATE TABLE [dbo].[TournamentEntries] (
    [id]           INT IDENTITY (1, 1) NOT NULL,
    [TournamentId] INT NULL,
    [TeamId]       INT NULL,
    CONSTRAINT [PK_TournamentEntries] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_TournamentEntries_Teams] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Teams] ([id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_TournamentEntries_Tournaments] FOREIGN KEY ([TournamentId]) REFERENCES [dbo].[Tournaments] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
);

