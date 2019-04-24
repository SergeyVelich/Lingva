CREATE TABLE [dbo].[Languages] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [CreateDate] DATETIME2 (7)  NULL,
    [ModifyDate] DATETIME2 (7)  NULL,
    [Name]       NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED ([Id] ASC)
);

