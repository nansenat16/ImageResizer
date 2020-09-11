using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageResizer
{
    class Program
    {
        static CancellationTokenSource cts = new CancellationTokenSource();
        static void Main(string[] args)
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(CancelJob);
            string sourcePath = Path.Combine(Environment.CurrentDirectory, "images");
            string destinationPath = Path.Combine(Environment.CurrentDirectory, "output"); ;

            ImageProcess imageProcess = new ImageProcess();

            imageProcess.Clean(destinationPath);

            Stopwatch sw = new Stopwatch();
            //sw.Start();
            //imageProcess.ResizeImages(sourcePath, destinationPath, 2.0);
            //sw.Stop();
            
            //Console.WriteLine($"Sync: {sw.ElapsedMilliseconds} ms");

            sw.Reset();
            sw.Start();
            imageProcess.ResizeImagesAsync(sourcePath, destinationPath, 2.0,cts.Token);
            sw.Stop();
            
            Console.WriteLine($"Async: {sw.ElapsedMilliseconds} ms");
        }

        private static void CancelJob(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("Job Cancel");
            cts.Cancel();
            e.Cancel=true;
        }
    }
}
