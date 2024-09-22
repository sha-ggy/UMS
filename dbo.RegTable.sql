CREATE TABLE [dbo].[RegTable] (
    [name]     VARCHAR (50) NULL,
    [id]       VARCHAR (50) NOT NULL,
    [age]    INT NOT NULL,
    [Password] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_RegTable] PRIMARY KEY CLUSTERED ([id] ASC)
);

