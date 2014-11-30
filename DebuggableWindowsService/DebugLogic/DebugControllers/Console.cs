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
            if (DebugSettings.Current.ConsoleStartupStatus == Constants.StartupStatus.Started)
                service.ConsoleStartService();

            NativeMethods.AllocConsole();
            ApplicationLoop(service);
        }

        private static void ApplicationLoop(DebuggableServiceBase service)
        {
            do
            {
                ConsoleOutputHelper.DisplayHeader(service);
                ConsoleOutputHelper.DisplayPromptOptions(true, service);

                char input = ConsoleInputHelper.PromptForOptionInput();

                HandleInput(input);
            } while (!_quiting);
        }

        private static void HandleInput(char inputCharacter)
        {
            var command = CommandController.GetEntityFromCommandChar(inputCharacter);

            if (command == null)
            {
                ConsoleOutputHelper.DisplayActionResponse("Invalid command");
            }
            else
            {
                command.RunAction();
            }
        }
    }
}