CREATE TABLE "ReminderItems"(
	"Id" UNIQUEIDENTIFIER NOT NULL,
	"StatusId" INT NOT NULL,
	"DateTime" DATETIMEOFFSET NOT NULL,
	"Message" NVARCHAR(512) NULL,
	"ContactId" INT NOT NULL,
	CONSTRAINT PK_ReminderItems PRIMARY KEY ("Id"),
	CONSTRAINT FK_ReminderItems_ReminderStatuses FOREIGN KEY ("StatusId")
		REFERENCES "ReminderStatuses" ("Id")
			ON DELETE NO ACTION
			ON UPDATE CASCADE,
	CONSTRAINT FK_ReminderItems_ReminderContacts FOREIGN KEY ("ContactId")
		REFERENCES "ReminderContacts" ("Id")
			ON DELETE CASCADE
			ON UPDATE CASCADE
);