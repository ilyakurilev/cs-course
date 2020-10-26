﻿using System;

namespace Reminder.Storage.Exceptions
{
    public class ReminderItemAlreadyExistsException : Exception
    {
        public Guid Id { get; }

        public ReminderItemAlreadyExistsException(Guid id) :
            base($"Reminder item with id {id:N} already exists")
        {
            Id = id;
        }
    }
}
