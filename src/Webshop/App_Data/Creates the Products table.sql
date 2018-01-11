CREATE TABLE [dbo].[Products] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [Number]      INT             NOT NULL,
    [Title]       NVARCHAR (MAX)  NOT NULL,
    [Price]       DECIMAL (18, 2) NOT NULL,
    [Description] NVARCHAR (MAX)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Products_Number]
    ON [dbo].[Products]([Number] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Products_Price]
    ON [dbo].[Products]([Price] ASC);
