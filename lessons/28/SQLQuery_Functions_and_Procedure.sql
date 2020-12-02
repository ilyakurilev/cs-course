CREATE FUNCTION GetReminder(@id UNIQUEIDENTIFIER)
RETURNS TABLE
    RETURN 
    (
            SELECT
                R.Id,
                S.Name as 'Status',
                R.DateTime,
                R.Message,
                C.ContactId
           FROM [Reminder] AS R
                JOIN [Status] AS S ON R.StatusId = S.Id
                JOIN [Contact] AS C ON R.ContactId = C.Id
           WHERE R.Id = @id
    );
GO

CREATE FUNCTION FindReminders(@datetime DATETIMEOFFSET, @statusName VARCHAR(20))
RETURNS TABLE
    RETURN 
    (
            SELECT
                R.Id,
                S.Name as 'Status',
                R.DateTime,
                R.Message,
                C.ContactId
           FROM [Reminder] AS R
                JOIN [Status] AS S ON R.StatusId = S.Id
                JOIN [Contact] AS C ON R.ContactId = C.Id
           WHERE R.DateTime < @datetime AND S.Name = @statusName
    );
GO

CREATE FUNCTION FindContactId(@contactId VARCHAR(32))
RETURNS INT
AS
BEGIN
    DECLARE @id INT =
    (
        SELECT TOP 1
            C.Id
        FROM [Contact] AS C
        WHERE C.ContactId = @contactId
    );
    RETURN @id
END;
GO

CREATE PROCEDURE GetOrCreateContactId
    @contact VARCHAR(32),
    @contactId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    SET @contactId = i_kurilev_schema.GetContactId(@contact)

    IF @contactId IS NULL
    BEGIN
        INSERT INTO [Contact]
        VALUES(@contact)

        SET @contactId = i_kurilev_schema.GetContactId(@contact)
    END;
END;
GO

CREATE PROCEDURE AddReminder
    @statusName VARCHAR(20),
    @datetime DATETIMEOFFSET,
    @message VARCHAR(512),
    @contact VARCHAR(32)
AS
BEGIN
    SET NOCOUNT ON

    DECLARE @contactId INT
    EXECUTE i_kurilev_schema.GetOrCreateContactId
        @contact = @contact,
        @contactId = @contactId OUTPUT

    DECLARE @statusId INT = 
    (
        SELECT TOP 1 
            S.Id
        FROM [Status] AS S
        WHERE S.Name = @statusName
    );

    INSERT INTO [Reminder]
    VALUES (NEWID(), @statusId, @datetime, @message, @contact) 

END;
GO

CREATE PROCEDURE UpdateReminder
    @id UNIQUEIDENTIFIER,
    @statusName VARCHAR(20),
    @message VARCHAR(512)
AS
BEGIN
    SET NOCOUNT ON

    IF EXISTS
    (
        SELECT R.Id
        FROM [Reminder] AS R
        WHERE R.Id = @id
    )
    BEGIN
        DECLARE @statusId INT = 
        (
            SELECT TOP 1
                S.Id
            FROM [Status] AS S
            WHERE S.Name = @statusName
        );
        
        UPDATE [Reminder]
        SET [StatusId] = @StatusId, [Message] = @message
        WHERE Id = @id
    END
    ELSE
        BEGIN
            DECLARE @msg VARCHAR(128) = 'Reminder with id = ' + CONVERT(varchar(32), @id) + ' not exists';
            THROW 666, @msg, 1
        END
END;
GO
