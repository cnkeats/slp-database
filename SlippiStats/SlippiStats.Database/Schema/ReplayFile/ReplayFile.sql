CREATE TABLE [dbo].[ReplayFile] (
    [Id]         INT             IDENTITY (1, 1) NOT NULL,
    [GameId]     INT             NULL,
    [UploaderId] INT             NULL,
    [FileData]   VARBINARY (MAX) NOT NULL,
	Created		DATETIME	NOT NULL,
	Updated		DATETIME	NULL,
	Deleted		DATETIME	NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);