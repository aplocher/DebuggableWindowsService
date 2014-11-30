using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using DebuggableWindowsService.DebugLogic.ConsoleCommands;

namespace DebuggableWindowsService.DebugLogic
{
    public class DebuggableServiceBase : ServiceBase
    {
        protected override void OnStart(string[] args)
        {
            DebugSettings.Current.ProcessArgs(args);
            if (DebugSettings.Current.IsServiceDebugMode)
            {
                if (DebugSettings.Current.ServiceDebugMode == Constants.ServiceDebugMode.Wait)
                {
                    while (!Debugger.IsAttached)
                    {
                        Thread.Sleep(100);
                    }
                }
                else
                {
                    if (!Debugger.IsAttached)
                    {
                        Debugger.Launch();
                    }
                }
            }

            base.OnStart(args);
        }

        internal void ConsoleStartService()
        {
            CommandController.ServiceStatus = ServiceControllerStatus.StartPending;
            base.OnStart(Environment.GetCommandLineArgs());
            CommandController.ServiceStatus = ServiceControllerStatus.Running;
        }

        internal void ConsoleStopService()
        {
            CommandController.ServiceStatus = ServiceControllerStatus.StopPending;
            base.OnStop();
            CommandController.ServiceStatus = ServiceControllerStatus.Stopped;
        }

        internal void ConsolePauseService()
        {
            CommandController.ServiceStatus = ServiceControllerStatus.PausePending;
            base.OnPause();
            CommandController.ServiceStatus = ServiceControllerStatus.Paused;
        }

        internal void ConsoleContinueService()
        {
            CommandController.ServiceStatus = ServiceControllerStatus.ContinuePending;
            base.OnContinue();
            CommandController.ServiceStatus = ServiceControllerStatus.Running;
        }
    }
}