using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace SimpleBackupService
{
    class Program
    {
        static void Main(string[] args)
        {
            var exitCode = HostFactory.Run(host =>
            {
                host.Service<BackupProcess>(s =>
                {
                    s.ConstructUsing(backup => new BackupProcess());
                    s.WhenStarted(backup => backup.Start());
                    s.WhenStopped(backup => backup.Stop());
                });

                host.RunAsLocalSystem();
                host.SetServiceName("SimpleBackupService");
                host.SetDisplayName("My Simple Backup Service");
                host.SetDescription("This simple backup service moves files from my Downloads folder to a folder in my Documents folder");
            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}
