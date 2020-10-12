using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class DataGeneratedEventArgs : EventArgs
    {
        public byte[] Data { get; set; }
    }

    class Generator
    {
        public event EventHandler<DataGeneratedEventArgs> DataGenerated;

        public void Generate(int count)
        {
            var data = new byte[count];
            var random = new Random();
            random.NextBytes(data);
            DataGenerated?.Invoke(this, new DataGeneratedEventArgs { Data = data });
        }
    }
}
