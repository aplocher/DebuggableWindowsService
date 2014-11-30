using System;
using DebuggableWindowsService.DebugLogic.ConsoleIO;

namespace DebuggableWindowsService.DebugLogic.ConsoleCommands
{
    public class CommandEntity
    {
        public string Name { get; set; }
        public char CommandCharacter { get; set; }
        public string PerformingActionMessage { get; set; }
        public Action CommandAction { get; set; }
        public Func<bool> CommandAvailabilityLogic { get; set; }

        public bool IsCommandAvailable
        {
            get
            {
                return
                    this.CommandAvailabilityLogic == null ||
                    this.CommandAvailabilityLogic();
            }
        }

        public void RunAction()
        {
            if (IsCommandAvailable)
            {
                ConsoleOutputHelper.DisplayActionResponse(PerformingActionMessage);

                CommandAction();
            }
            else
            {
                ConsoleOutputHelper.DisplayActionResponse("Command not available");
            }
        }
    }
}