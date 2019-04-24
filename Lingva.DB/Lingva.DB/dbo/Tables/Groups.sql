CREATE TABLE [dbo].[Groups] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [CreateDate]  DATETIME2 (7)  NULL,
    [ModifyDate]  DATETIME2 (7)  NULL,
    [Name]        NVARCHAR (MAX) NULL,
    [Date]        DATETIME2 (7)  NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [Picture]     NVARCHAR (MAX) NULL,
    [LanguageId]  INT            NOT NULL,
    CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Groups_Languages_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Groups_LanguageId]
    ON [dbo].[Groups]([LanguageId] ASC);

