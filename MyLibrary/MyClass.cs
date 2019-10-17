using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;

namespace MyLibrary
{
    public class MyClass
    {
        public long originalLength;
        public MemoryStream Compress(object p) {
            MemoryStream originalStream = Serialize(p);//Length is c2
            originalLength = originalStream.Length;
            MemoryStream compressedStream = new MemoryStream();
            DeflateStream compressor = new DeflateStream(compressedStream, CompressionMode.Compress);
            originalStream.Position = 0;
            originalStream.CopyTo(compressor);
            compressor.Flush();
            return compressedStream;

        }

        /// <summary>
        ///
        /// </summary>
        public MemoryStream Serialize(object p)
        {

            MemoryStream ms = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            
            formatter.Serialize(ms, p);

            return ms;
        }
    }

    [Serializable]
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

