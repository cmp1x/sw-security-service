USE [SWCredentials]  
GO
--jwek4w6als4
DROP TABLE [UserCredential]
GO

CREATE TABLE [dbo].[UserCredential] (
	[UserId] NVARCHAR (MAX),
    [UserName]   NVARCHAR (MAX) NULL,
    [Password]    NVARCHAR (MAX) NULL
);
go
INSERT INTO [UserCredential] ([UserId], [UserName], [Password]) VALUES ('jwek4w6als4', 'Vladimir', 'fghjk')