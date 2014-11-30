using System;

namespace DebuggableWindowsService.DebugLogic.ConsoleIO
{
    internal class ConsoleInputHelper
    {
        public static char PromptForOptionInput()
        {
            Console.SetCursorPosition(0, 24);
            Console.Write("> ");
            var keyInfo = Console.ReadKey(true);
            Console.Write(char.ToUpperInvariant(keyInfo.KeyChar));

            return keyInfo.KeyChar;

        }
    }
}