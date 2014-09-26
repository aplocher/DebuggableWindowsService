using DebuggableWindowsService.DebugLogic.ConsoleCommands;
using DebuggableWindowsService.DebugLogic.ConsoleIO;

namespace DebuggableWindowsService.DebugLogic.DebugControllers
{
    public static class ConsoleDebugController
    {
        private static bool _quiting = false;

        public static void QuitSignal()
        {
            _quiting = true;
        }

        public static void Run(DebuggableServiceBase service)
        {
            CommandController.InitializeCommandEntityList(service);
            NativeMethods.AllocConsole();
            ApplicationLoop(service);
        }

        private static void ApplicationLoop(DebuggableServiceBase service)
        {
            ConsoleOutputHelper.DisplayHeader();

            do
            {
                ConsoleOutputHelper.DisplayPromptOptions(true, service);
                ConsoleInputHelper.PromptForOptionInput();
            } while (!_quiting);
        }
    }
}
