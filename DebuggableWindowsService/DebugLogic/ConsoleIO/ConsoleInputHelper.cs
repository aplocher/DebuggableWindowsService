using System;
using DebuggableWindowsService.DebugLogic.ConsoleCommands;

namespace DebuggableWindowsService.DebugLogic.ConsoleIO
{
    internal class ConsoleInputHelper
    {
        public static void PromptForOptionInput()
        {
            Console.WriteLine("---------------------------------------------------------------------");
            Console.Write("> ");
            var keyInfo = Console.ReadKey(true);
            Console.Write(char.ToUpperInvariant(keyInfo.KeyChar));

            HandleInput(keyInfo.KeyChar);
            Console.WriteLine("---------------------------------------------------------------------");
        }

        public static void HandleInput(char inputCharacter)
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
