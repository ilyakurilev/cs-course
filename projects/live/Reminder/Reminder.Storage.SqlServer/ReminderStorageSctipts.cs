using System.IO;
using System.Reflection;
using Microsoft.Extensions.FileProviders;

namespace Reminder.Storage.SqlServer
{
    public static class ReminderStorageSctipts
    {
        public static string CreateTableReminderItems =>
            ReadScript(nameof(CreateTableReminderItems));

        public static string CreateTableReminderContacts =>
            ReadScript(nameof(CreateTableReminderContacts));

        public static string CreateTableReminderStatuses =>
            ReadScript(nameof(CreateTableReminderStatuses));

        public static string CreateProcedureAddReminderItem =>
            ReadScript(nameof(CreateProcedureAddReminderItem));

        public static string CreateProcedureUpdateReminderItem =>
            ReadScript(nameof(CreateProcedureUpdateReminderItem));

        public static string InsertReminderStatuses =>
            ReadScript(nameof(InsertReminderStatuses));

        private static string ReadScript(string script)
        {
            var provider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());
            var file = provider.GetFileInfo($"Scripts\\{script}.sql");
            using var reader = new StreamReader(file.CreateReadStream());
            return reader.ReadToEnd();
        }
    }
}
