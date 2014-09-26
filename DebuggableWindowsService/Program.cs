using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using DebuggableWindowsService.DebugLogic;
using DebuggableWindowsService.DebugLogic.DebugControllers;

namespace DebuggableWindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            DebugSettings.Current.ProcessArgs(Environment.GetCommandLineArgs());
            if (DebugSettings.Current.IsConsoleDebugMode)
            {
                ConsoleDebugController.Run(new Service1());
            }
            else
            {
                var servicesToRun = new ServiceBase[] {new Service1()};
                ServiceBase.Run(servicesToRun);
            }
        }
    }
}
