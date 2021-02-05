CREATE TABLE "ReminderStatuses"(
	"Id" INT NOT NULL,
	"Status" VARCHAR(32) NOT NULL,
	CONSTRAINT PK_ReminderStatuses PRIMARY KEY ("Id"),
	CONSTRAINT UQ_ReminderStatuses_Status UNIQUE ("Status")
);