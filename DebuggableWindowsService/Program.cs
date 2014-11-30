using System;
using System.ServiceProcess;
using DebuggableWindowsService.DebugLogic;
using DebuggableWindowsService.DebugLogic.DebugControllers;

namespace DebuggableWindowsService
{
    internal static class Program
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
                var servicesToRun = new ServiceBase[] { new Service1() };
                ServiceBase.Run(servicesToRun);
            }
        }
    }
}