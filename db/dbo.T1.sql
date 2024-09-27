CREATE TABLE [dbo].[T1] (
    [name]     VARCHAR (50) NOT NULL,
    [id]       VARCHAR (50) NOT NULL,
    [email]    VARCHAR (50) NOT NULL,
    [password] VARCHAR (50) NOT NULL,
    [otp1]      INT          NOT NULL,
    [otp2] INT NOT NULL, 
    CONSTRAINT [PK_Table] PRIMARY KEY CLUSTERED ([id] ASC)
);

