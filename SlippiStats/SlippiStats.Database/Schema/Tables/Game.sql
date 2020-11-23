CREATE TABLE [dbo].[Game] (
    [Id]         INT          IDENTITY (1, 1) NOT NULL,
    [Player1]    VARCHAR (64) NULL,
    [Player2]    VARCHAR (64) NULL,
    [GameLength] INT          NOT NULL,
    [Created]    DATETIME     NOT NULL,
    [Updated]    DATETIME     NULL,
    [Deleted]    DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);