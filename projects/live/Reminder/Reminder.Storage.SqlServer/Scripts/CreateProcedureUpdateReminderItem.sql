CREATE PROCEDURE UpdateReminderItem
	@id UNIQUEIDENTIFIER,
	@statusId INT,
	@message NVARCHAR(512),
	@rows INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE "ReminderItems"
	   SET "StatusId" = @statusId,
	       "Message" = @message
	 WHERE Id = @id;

	 SET @rows = @@ROWCOUNT;
END;