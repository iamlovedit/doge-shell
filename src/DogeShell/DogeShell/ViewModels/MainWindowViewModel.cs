using Renci.SshNet;

namespace DogeShell.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public string Greeting { get; } = "Welcome to Avalonia!";

        private SshClient _sshClient;
        private ShellStream _shellStream;
     
    }
}
