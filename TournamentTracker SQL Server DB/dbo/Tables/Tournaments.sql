CREATE TABLE [dbo].[Tournaments] (
    [id]               INT            IDENTITY (1, 1) NOT NULL,
    [TournamentName]   NVARCHAR (200) NOT NULL,
    [EntryFee]         MONEY          NOT NULL,
    [InitialPrizePool] MONEY          NOT NULL,
    [Active]           BIT            NOT NULL,
    [GreaterScoreWins] BIT            NOT NULL,
    CONSTRAINT [PK_Tournaments] PRIMARY KEY CLUSTERED ([id] ASC)
);

