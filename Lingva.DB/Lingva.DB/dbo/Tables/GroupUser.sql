CREATE TABLE [dbo].[GroupUser] (
    [GroupId]    INT           NOT NULL,
    [UserId]     INT           NOT NULL,
    [Id]         INT           NOT NULL,
    [CreateDate] DATETIME2 (7) NULL,
    [ModifyDate] DATETIME2 (7) NULL,
    CONSTRAINT [PK_GroupUser] PRIMARY KEY CLUSTERED ([GroupId] ASC, [UserId] ASC),
    CONSTRAINT [FK_GroupUser_Groups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_GroupUser_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_GroupUser_UserId]
    ON [dbo].[GroupUser]([UserId] ASC);

