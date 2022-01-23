CREATE TABLE [dbo].[users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[login] TEXT NOT NULL,
	[password] TEXT NOT NULL,
	[email] TEXT NOT NULL
)

CREATE TABLE [dbo].[orders]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[serialNumber] TEXT NOT NULL,
    [userId] INT NOT NULL,
	[orderDate] date DEFAULT GETDATE() NOT NULL,
	[fixedDate] date NOT NULL,
    [employeeSurname] TEXT NOT NULL,
	[status] TEXT NOT NULL
)

ALTER TABLE [dbo].[orders]
ADD
FOREIGN KEY(userId) REFERENCES users (Id)
ON DELETE CASCADE
GO