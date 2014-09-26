using System;
using System.Reflection;
using System.Threading;
using DebuggableWindowsService.DebugLogic.ConsoleCommands;

namespace DebuggableWindowsService.DebugLogic.ConsoleIO
{
    public static class ConsoleOutputHelper
    {
        internal static void DisplayHelpInfo(DebuggableServiceBase service)
        {
            DisplayHeader();
            DisplayHorizontalSeparator();

            DisplayInfo(service);
            DisplayHorizontalSeparator();

            DisplayCommandLineArgsInfo();
            Console.WriteLine();
        }

        public static void DisplayActionResponse(string actionText)
        {
            Console.Write(" - ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(actionText);
            Console.ResetColor();
            Thread.Sleep(300);

            Console.WriteLine();
        }

        public static void DisplayHeader()
        {
            Console.WriteLine("Windows Service Debug Console - By Adam Plocher");
        }

        internal static void DisplayInfo(DebuggableServiceBase service)
        {
            Console.WriteLine("Service        : {0}", service.ServiceName);
            Console.WriteLine("Version        : {0}", Assembly.GetExecutingAssembly().GetName().Version);
            Console.WriteLine("User context   : {0}", Environment.UserName);
            Console.WriteLine("Status         : {0}", CommandController.GetServiceStatusDescription());
            Console.WriteLine("Can Pause/Cont : {0}", service.CanPauseAndContinue);
        }

        internal static void DisplayCommandLineArgsInfo()
        {
            var tabbedNewLine = Environment.NewLine + "\t\t\t  ";

            Console.WriteLine("{0}\t\t- Run service in console wrapper. Useful for{1}debugging your service directly through Visual Studio", Constants.DebugConsoleArg, tabbedNewLine);
            Console.WriteLine("{0}\t- Only applies when {1} is enabled. When{3}not explicitly set, the default value will be used.{3}default: {2}", Constants.DebugConsoleStartupActionArg, Constants.DebugConsoleArg, Constants.DebugConsoleStartupActionStarted, tabbedNewLine);
            Console.WriteLine("{0}\t\t- Enables native debugging as a Windows Service. This{1}flag should be applied using sc.exe or through the{1}Windows Services control panel (services.msc)", Constants.DebugServiceArg, tabbedNewLine);
        }

        private static void DisplayHorizontalSeparator()
        {
            Console.WriteLine("--------------------------------------------");
        }

        internal static void DisplayPromptOptions(bool extended, DebuggableServiceBase service)
        {
            Console.WriteLine();
            Console.WriteLine("{0}", service.ServiceName);
            Console.WriteLine("Status: {0} - Options:", CommandController.GetServiceStatusDescription());

            bool looped = false;
            foreach (var commandEntity in CommandController.CommandEntityList)
            {
                if (looped)
                    Console.Write(" | ");
                Console.ForegroundColor =
                    commandEntity.IsCommandAvailable
                        ? ConsoleColor.White
                        : ConsoleColor.DarkGray;

                Console.Write("{0}- {1}", commandEntity.CommandCharacter.ToString().ToUpper(), commandEntity.Name);
                Console.ResetColor();

                looped = true;
            }

            Console.WriteLine();
        }
    }
}
