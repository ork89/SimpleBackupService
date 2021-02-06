using System;
using System.IO;
using System.Linq;
using System.Timers;

namespace SimpleBackupService
{
    public class BackupProcess
    {
        private readonly Timer _timer;
        string sourcePath = "C:/Users/OriKo/Downloads";
        string targetPath = "C:/Users/OriKo/Documents/DocumentsFromDownloads";

        public BackupProcess()
        {
            // Run every 2 hours
            _timer = new Timer(7200000)
            {
                AutoReset = true
            };

            _timer.Elapsed += timer_Elapsed;
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (!Directory.Exists(sourcePath))
                    Directory.CreateDirectory(targetPath);

                var files = Directory.EnumerateFiles(sourcePath, "*.*")
                .Where(s => s.EndsWith(".pdf", System.StringComparison.OrdinalIgnoreCase)
                        || s.EndsWith(".txt", System.StringComparison.OrdinalIgnoreCase)
                        || s.EndsWith(".htm", System.StringComparison.OrdinalIgnoreCase));

                foreach (var item in files)
                {
                    var fileName = Path.GetFileName(item);
                    var sourceFile = Path.Combine(sourcePath, fileName);
                    Console.WriteLine($"\nMoving file: {sourceFile}");

                    var destFile = Path.Combine(targetPath, fileName);

                    if (!File.Exists(destFile))
                    {
                        Console.WriteLine($"Destination: {destFile}");
                        File.Move(item, destFile);
                    }
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine($"Process failed: {exp}");
            }
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
