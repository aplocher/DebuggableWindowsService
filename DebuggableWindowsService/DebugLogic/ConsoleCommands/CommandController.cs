using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using DebuggableWindowsService.DebugLogic.ConsoleIO;
using DebuggableWindowsService.DebugLogic.DebugControllers;

namespace DebuggableWindowsService.DebugLogic.ConsoleCommands
{
    internal class CommandController
    {
        public static ServiceControllerStatus ServiceStatus { get; set; }

        private static List<CommandEntity> _commandEntityList = null;

        public static List<CommandEntity> CommandEntityList
        {
            get { return _commandEntityList; }
        }

        internal static void InitializeCommandEntityList(DebuggableServiceBase service)
        {
            _commandEntityList = new List<CommandEntity>
            {
                new CommandEntity
                {
                    CommandCharacter = '1',
                    Name = "Start",
                    CommandAvailabilityLogic = () => ServiceStatus != ServiceControllerStatus.Running,
                    PerformingActionMessage = "Starting service",
                    CommandAction = () => service.ConsoleStartService()
                },
                new CommandEntity
                {
                    CommandCharacter = '2',
                    Name = "Stop",
                    CommandAvailabilityLogic = () => ServiceStatus == ServiceControllerStatus.Running,
                    PerformingActionMessage = "Stopping service",
                    CommandAction = () => service.ConsoleStopService()
                },
                new CommandEntity
                {
                    CommandCharacter = '3',
                    Name = "Pause",
                    CommandAvailabilityLogic =
                        () => ServiceStatus == ServiceControllerStatus.Running && service.CanPauseAndContinue,
                    PerformingActionMessage = "Pausing service",
                    CommandAction = () => service.ConsolePauseService()
                },
                new CommandEntity
                {
                    CommandCharacter = '4',
                    Name = "Continue",
                    CommandAvailabilityLogic =
                        () => ServiceStatus == ServiceControllerStatus.Paused && service.CanPauseAndContinue,
                    PerformingActionMessage = "Resuming service",
                    CommandAction = () => service.ConsoleContinueService()
                },
                new CommandEntity
                {
                    CommandCharacter = 'H',
                    Name = "Help / Service Info",
                    PerformingActionMessage = "Displaying information",
                    CommandAction = () => ConsoleOutputHelper.DisplayHelpInfo(service)
                },
                new CommandEntity
                {
                    CommandCharacter = 'Q',
                    Name = "Quit",
                    PerformingActionMessage = "Quiting",
                    CommandAction = () => ConsoleDebugController.QuitSignal()
                }
            };

            service.ConsoleStopService();
        }


        public static string GetServiceStatusDescription()
        {
            return Enum.GetName(typeof (ServiceControllerStatus), ServiceStatus);
        }

        public static CommandEntity GetEntityFromCommandChar(char commandCharacter)
        {
            return CommandEntityList.FirstOrDefault(
                x => char.ToUpperInvariant(x.CommandCharacter) == char.ToUpperInvariant(commandCharacter));
        }
    }
}