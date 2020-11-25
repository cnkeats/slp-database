CREATE TABLE [dbo].[Stage] (
    [Id]           INT          IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (32) NOT NULL,
    [SinglesLegal] BIT          DEFAULT ((0)) NOT NULL,
    [DoublesLegal] BIT          DEFAULT ((0)) NOT NULL,
    [RJJLegal]     BIT          DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);