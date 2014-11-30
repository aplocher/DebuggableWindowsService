namespace DebuggableWindowsService.DebugLogic
{
    public class Constants
    {
        public const string DebugConsoleArg = "/DebugInConsole";
        public const string DebugConsoleStartupActionArg = "/ConsoleServiceStartup";
        public const string DebugConsoleStartupActionStarted = "Start";
        public const string DebugConsoleStartupActionStopped = "Stop";
        public const string DebugServiceArg = "/DebugService";
        public const string DebugServiceModeArg = "/DebugServiceMode";
        public const string DebugServiceModeWait = "Wait";
        public const string DebugServiceModePrompt = "Prompt";

        public enum StartupStatus
        {
            Started,
            Stopped
        }

        public enum ServiceDebugMode
        {
            Prompt,
            Wait
        }
    }
}