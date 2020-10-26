using System;
using System.Collections.Generic;
using System.Text;

namespace Reminder.Storage.Exceptions
{
    public class ReminderItemAlreadyExistException : Exception
    {
        public Guid Id { get; }

        public ReminderItemAlreadyExistException(Guid id) :
            base($"Reminder item with id {id:N} already exist")
        {
            Id = id;    
        }
    }
}
