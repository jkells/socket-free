
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace SocketFree
{
    public class SocketFree
    {
        public void KillProcessesByPort(int port)
        {
            var conections = IphlapiWrapper.Ipv4Connections().Select(
                item => new
                            {
                                localPort = (ushort) IPAddress.NetworkToHostOrder(item.localPort1),
                                remotePort = (ushort) IPAddress.NetworkToHostOrder(item.remotePort1),
                                pid = item.owningPid
                            });

            var connectionsForPort = conections
                .Where(x => x.localPort == port && x.remotePort == 0)
                .ToArray();

            if (connectionsForPort.Length == 0)
                return;

            var connection = connectionsForPort.First();
            var processes = Process.GetProcesses();

            var ownerProcess = processes.FirstOrDefault(p => p.Id == connection.pid);
            if (ownerProcess != null)
            {
                Console.WriteLine("Killing Process {0}: {1}", ownerProcess.Id, ownerProcess.ProcessName);
                ownerProcess.Kill();
            }
        }
    }
}