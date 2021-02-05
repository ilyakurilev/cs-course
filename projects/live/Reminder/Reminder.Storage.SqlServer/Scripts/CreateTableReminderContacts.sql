CREATE TABLE "ReminderContacts"(
	"Id" INT IDENTITY NOT NULL,
	"ChatId" VARCHAR(32) NOT NULL,
	CONSTRAINT PK_ReminderContacts PRIMARY KEY ("Id"),
	CONSTRAINT UQ_ReminderContacts_ChatId UNIQUE ("ChatId")
);