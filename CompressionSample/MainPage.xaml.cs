using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Formatters.Binary;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CompressionSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void btnClick_Click(object sender, RoutedEventArgs e)
        {
            Person person1 = new Person { FirstName = "Bill", LastName = "Smith" };
            MyLibrary.Person person2 = new MyLibrary.Person { FirstName = "Bill", LastName = "Smith" };
            MyLibrary.MyClass cls = new MyLibrary.MyClass();
            MemoryStream compressedStream=cls.Compress(person2);
            tbOriginal.Text = "Original Stream Length: " + cls.originalLength;
            MemoryStream decompressedStream=Decompress(compressedStream);//length is still c2
            tbDecompressed.Text = "Decompressed Stream Length: " + decompressedStream.Length;
            
        }
        public MemoryStream Decompress(Stream compressedStream)
        {
            compressedStream.Position = 0;
            MemoryStream decompressedStream = new MemoryStream();
            
            using (DeflateStream decompressor = new DeflateStream(compressedStream, CompressionMode.Decompress))
            {
                decompressor.CopyTo(decompressedStream);
                decompressor.Flush();
                //decompressor.Close();
            }  // end using.

            return decompressedStream;
        }

    }
    [Serializable]
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
