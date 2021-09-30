CREATE TABLE [dbo].[Matchups] (
    [id]           INT IDENTITY (1, 1) NOT NULL,
    [TournamentId] INT NOT NULL,
    [WinnerId]     INT NULL,
    [MatchupRound] INT NULL,
    CONSTRAINT [PK_Matchups] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Matchups_Teams] FOREIGN KEY ([WinnerId]) REFERENCES [dbo].[Teams] ([id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_Matchups_Tournaments] FOREIGN KEY ([TournamentId]) REFERENCES [dbo].[Tournaments] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
);

