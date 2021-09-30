CREATE TABLE [dbo].[TeamMembers] (
    [id]       INT IDENTITY (1, 1) NOT NULL,
    [TeamId]   INT NULL,
    [PersonId] INT NULL,
    CONSTRAINT [PK_TeamMembers] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_TeamMembers_People] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[People] ([id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_TeamMembers_Teams] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Teams] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
);

