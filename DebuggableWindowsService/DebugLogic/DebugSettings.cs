using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebuggableWindowsService.DebugLogic
{
    public class DebugSettings
    {
        private static DebugSettings _current = null;

        public static DebugSettings Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new DebugSettings();
                }

                return _current;
            }
        }

        private DebugSettings()
        {
            ConsoleStartupStatus = Constants.StartupStatus.Stopped;
            ServiceDebugMode = Constants.ServiceDebugMode.Prompt;

        }

        public bool IsConsoleDebugMode { get; set; }

        public bool IsServiceDebugMode { get; set; }

        public Constants.StartupStatus ConsoleStartupStatus { get; set; }

        public Constants.ServiceDebugMode ServiceDebugMode { get; set; }

        public void ProcessArgs(string[] args)
        {
            if (IsServiceDebugMode || IsConsoleDebugMode)
                return;

            foreach (var arg in args)
            {
                if (arg.Equals(Constants.DebugConsoleArg, StringComparison.OrdinalIgnoreCase))
                {
                    IsConsoleDebugMode = true;
                }

                if (arg.StartsWith(Constants.DebugConsoleStartupActionArg + "=", StringComparison.OrdinalIgnoreCase))
                {
                    ConsoleStartupStatus = arg.EndsWith("=" + Constants.DebugConsoleStartupActionStarted, StringComparison.OrdinalIgnoreCase) 
                        ? Constants.StartupStatus.Started 
                        : Constants.StartupStatus.Stopped;
                }

                if (arg.Equals(Constants.DebugServiceArg, StringComparison.OrdinalIgnoreCase))
                {
                    IsServiceDebugMode = true;
                }

                if (arg.StartsWith(Constants.DebugServiceModeArg + "=", StringComparison.OrdinalIgnoreCase))
                {
                    ServiceDebugMode = arg.EndsWith("=" + Constants.DebugServiceModeWait) 
                        ? Constants.ServiceDebugMode.Wait 
                        : Constants.ServiceDebugMode.Prompt;
                    
                }
            }
        }
    }
}
