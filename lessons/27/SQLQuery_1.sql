CREATE TABLE [Status](
    [Id] INT IDENTITY(1, 1) NOT NULL,
    [Name] VARCHAR(20) NOT NULL,
    CONSTRAINT PK_Status_Id PRIMARY KEY([Id]),
    CONSTRAINT UQ_Status_Name UNIQUE ([Name])
);

CREATE TABLE [Contact](
    [Id] INT IDENTITY(1, 1) NOT NULL,
    [ContactId] VARCHAR(32) NOT NULL,
    CONSTRAINT PK_Contact_Id PRIMARY KEY ([Id]),
    CONSTRAINT UQ_Contact_ContactId UNIQUE ([ContactId])
)

CREATE TABLE [Reminder](
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [StatusId] INT NOT NULL,
    [DateTime] DATETIMEOFFSET NOT NULL,
    [Message] VARCHAR(512) NOT NULL,
    [ContactId] INT NOT NULL,
    CONSTRAINT PK_Reminder_Id PRIMARY KEY ([Id]),
    CONSTRAINT FK__StatusId_StatusId FOREIGN KEY ([StatusId])
        REFERENCES [Status]([Id])
            ON DELETE CASCADE
            ON UPDATE CASCADE,
    CONSTRAINT FK_ContactId_Contact_Id FOREIGN KEY ([ContactId])
        REFERENCES [Contact]([Id])
            ON DELETE CASCADE
            ON UPDATE CASCADE
);