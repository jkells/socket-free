using System;
using System.Runtime.InteropServices;

namespace SocketFree
{
    public class IphlapiWrapper
    {
        [DllImport("iphlpapi.dll", SetLastError = true)]
        static extern uint GetExtendedTcpTable(IntPtr pTcpTable, ref int dwOutBufLen, bool sort, int ipVersion, TcpTableClass tblClass, int reserved);

        [StructLayout(LayoutKind.Sequential)]
        public struct MibTcprowOwnerPid
        {
            public uint state;
            public uint localAddr;
            public short localPort1;
            public short localPort2;
            public uint remoteAddr;
            public short remotePort1;
            public short remotePort2;
            public int owningPid;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MibTcptableOwnerPid
        {
            public uint dwNumEntries;
            MibTcprowOwnerPid table;
        }

        // ReSharper disable UnusedMember.Local
        enum TcpTableClass
        {
            TcpTableBasicListener,
            TcpTableBasicConnections,
            TcpTableBasicAll,
            TcpTableOwnerPidListener,
            TcpTableOwnerPidConnections,
            TcpTableOwnerPidAll,
            TcpTableOwnerModuleListener,
            TcpTableOwnerModuleConnections,
            TcpTableOwnerModuleAll
        }
        // ReSharper restore UnusedMember.Local

        public static MibTcprowOwnerPid[] Ipv4Connections()
        {
            MibTcprowOwnerPid[] table;
            const int afInet = 2;
            int buffSize = 0;
 
            GetExtendedTcpTable(IntPtr.Zero, ref buffSize, true, afInet, TcpTableClass.TcpTableOwnerPidAll, 0);
            buffSize *= 2;
            var buffTable = Marshal.AllocHGlobal(buffSize);

            try
            {
                uint ret = GetExtendedTcpTable(buffTable, ref buffSize, true, afInet, TcpTableClass.TcpTableOwnerPidAll, 0);
                if (ret != 0)
                {
                    return null;
                }

                var tab = (MibTcptableOwnerPid)Marshal.PtrToStructure(buffTable, typeof(MibTcptableOwnerPid));
                var rowPtr = (IntPtr)((long)buffTable + Marshal.SizeOf(tab.dwNumEntries));
                table = new MibTcprowOwnerPid[tab.dwNumEntries];

                for (int i = 0; i < tab.dwNumEntries; i++)
                {
                    var tcpRow = (MibTcprowOwnerPid)Marshal.PtrToStructure(rowPtr, typeof(MibTcprowOwnerPid));
                    table[i] = tcpRow;
                    rowPtr = (IntPtr)((long)rowPtr + Marshal.SizeOf(tcpRow));
                }

            }
            finally
            {
                Marshal.FreeHGlobal(buffTable);
            }

            return table;
        }
    }
}
