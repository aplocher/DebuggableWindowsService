using DebuggableWindowsService.DebugLogic;

namespace DebuggableWindowsService
{
    public partial class Service1 : DebuggableServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
    }
}