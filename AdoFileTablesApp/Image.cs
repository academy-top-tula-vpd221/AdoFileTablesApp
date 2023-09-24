using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoFileTablesApp
{
    internal class Image
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string FileName { get; private set; }
        public byte[] Data { get; private set; }

        public Image(int id, string title, string fileName, byte[] data)
        {
            Id = id;
            Title = title;
            FileName = fileName;
            Data = data;
        }
    }
}
