CREATE TABLE [dbo].[MatchupEntries] (
    [id]              INT        IDENTITY (1, 1) NOT NULL,
    [MatchupId]       INT        NOT NULL,
    [ParentMatchupId] INT        NULL,
    [TeamCompetingId] INT        NULL,
    [Score]           FLOAT (53) NULL,
    CONSTRAINT [PK_MatchupEntries] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_MatchupEntries_Matchups] FOREIGN KEY ([MatchupId]) REFERENCES [dbo].[Matchups] ([id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_MatchupEntries_Matchups_Parent] FOREIGN KEY ([ParentMatchupId]) REFERENCES [dbo].[Matchups] ([id]),
    CONSTRAINT [FK_MatchupEntries_Teams] FOREIGN KEY ([TeamCompetingId]) REFERENCES [dbo].[Teams] ([id])
);

