using System;
using System.IO;

namespace ConsoleApp1
{
    class WritingPerformedEventArgs : EventArgs
    {
        private byte _percentage;

        public WritingPerformedEventArgs(byte percentage)
        {
            Percentage = percentage;
        }

        public byte Percentage
        {
            get => _percentage;
            private set
            {
                if (value > 100)
                {
                    throw new ArgumentException("Percentage can not be greater than 100!");
                }
                _percentage = value;
            }
        }
    }

    class WritingCompletedEventArgs : EventArgs
    {

    }

    class FileWriterWithProgress
    {
        public event EventHandler<WritingPerformedEventArgs> WritingPerformed;
        public event EventHandler<WritingCompletedEventArgs> WritingCompleted;


        public void WriteBytes(string fileName, byte[] data, float percentageToFireEvent)
        {
            if (data == null)
            {
                throw new ArgumentException();
            }

            using (var file = File.OpenWrite(fileName))
            {
                var iterationsToEvent = (int)(data.Length * percentageToFireEvent);
                var countIterations = 0;

                byte percent = 0;
                byte step = (byte)(percentageToFireEvent * 100);

                for (int i = 0; i < data.Length; i++)
                {
                    file.WriteByte(data[i]);
                    
                    countIterations++;

                    if (countIterations == iterationsToEvent)
                    {
                        percent += step;
                        WritingPerformed?.Invoke(this, new WritingPerformedEventArgs(percent));
                        countIterations = 0;
                    }
                }

                WritingCompleted?.Invoke(this, new WritingCompletedEventArgs());
            }
        }
    }
}
