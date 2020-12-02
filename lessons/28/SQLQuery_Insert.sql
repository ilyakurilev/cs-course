INSERT INTO [Status]
VALUES
    ('Created'),
    ('Ready'),
    ('Failed');
GO

INSERT INTO [Contact]
VALUES
    ('ContactId1'),
    ('ContactId2'),
    ('ContactId3');
GO

INSERT INTO [Reminder] ([Id], [StatusId], [DateTime], [Message], [ContactId])
VALUES
    (NEWID(), 1, SYSUTCDATETIME(), 'Message1', 1),
    (NEWID(), 1, SYSUTCDATETIME(), 'Message2', 1),
    (NEWID(), 3, SYSUTCDATETIME(), 'Message3', 2)
GO