CREATE TABLE [dbo].[TournamentPrizes] (
    [id]           INT IDENTITY (1, 1) NOT NULL,
    [TournamentId] INT NULL,
    [PrizeId]      INT NULL,
    CONSTRAINT [PK_TournamentPrizes] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_TournamentPrizes_Prizes] FOREIGN KEY ([PrizeId]) REFERENCES [dbo].[Prizes] ([id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_TournamentPrizes_Tournaments] FOREIGN KEY ([TournamentId]) REFERENCES [dbo].[Tournaments] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
);

