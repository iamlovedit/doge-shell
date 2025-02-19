using Avalonia.Threading;
using CommunityToolkit.Mvvm.Input;
using Renci.SshNet;
using Renci.SshNet.Common;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DogeShell.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Greeting { get; set; } = "Welcome to Avalonia!";

        private SshClient _sshClient;
        private ShellStream _shellStream;

        public async void HandleConnect()
        {
            _sshClient = new SshClient("10.30.0.211", "root", "password");
            await _sshClient.ConnectAsync(CancellationToken.None);
            _shellStream = _sshClient.CreateShellStream("bash", 160, 40, 800, 600, 1024);
            _shellStream.DataReceived += Stream_DataReceived;

        }

        private async void Stream_DataReceived(object? sender, ShellDataEventArgs e)
        {
            var r = Encoding.UTF8.GetString(e.Data);
            Name += r;
            Console.WriteLine(r);
            await Task.Delay(500);
        }

        public void SendCommand()
        {
            _shellStream.WriteLine("ls -ah");
        }
    }

}
