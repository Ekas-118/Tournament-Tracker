CREATE TABLE [dbo].[Prizes] (
    [id]              INT           IDENTITY (1, 1) NOT NULL,
    [PlaceNumber]     INT           NOT NULL,
    [PlaceName]       NVARCHAR (50) NOT NULL,
    [PrizeAmount]     MONEY         CONSTRAINT [DF_Prizes_PrizeAmount] DEFAULT ((0)) NOT NULL,
    [PrizePercentage] FLOAT (53)    CONSTRAINT [DF_Prizes_PrizePercentage] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Prizes] PRIMARY KEY CLUSTERED ([id] ASC)
);

