CREATE PROCEDURE AddReminderItem
	@id UNIQUEIDENTIFIER,
	@statusId INT,
	@dateTime DATETIMEOFFSET,
	@message NVARCHAR(512),
	@chatId VARCHAR(32)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @contactId INT;
	SELECT @contactId = "Id" FROM "ReminderContacts" WHERE "ChatId" = @chatId;

	IF @contactId IS NULL
	BEGIN
		INSERT INTO "ReminderContacts" VALUES (@chatId);
		SET @contactId = @@IDENTITY;
	END;

	INSERT INTO "ReminderItems" VALUES (@id, @statusId, @dateTime, @message, @contactId);
END;