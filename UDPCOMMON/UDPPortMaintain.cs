using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UDPCOMMON
{
    public class UDPPortMaintain
    {
        private int startPort = Globals.LOCAL_PORT;
        private const int PeerPort = 100;
        public UDPPortMaintain(int portStart)
        {
            startPort = portStart;
        }

        public UPDPortRange GetLaterPort(int times)
        {
            UPDPortRange retunvalue = new UPDPortRange();
            if (startPort - times * PeerPort > 1025)
            {
                retunvalue.FrontPort = new int[PeerPort];
                for (int i = 0; i < PeerPort; i++)
                {
                    retunvalue.FrontPort[i] = (startPort - times * PeerPort) - i;
                }
            }

            if (startPort + times * PeerPort < 65530)
            {
                retunvalue.RearPort = new int[PeerPort];
                for (int i = 0; i < PeerPort; i++)
                {
                    retunvalue.RearPort[i] = (startPort + times * PeerPort) - i;
                }
            }
            return retunvalue;
        }
    }

    public class UPDPortRange
    {
        public int[] FrontPort { get; set; }

        public int[] RearPort { get; set; }

        public UPDPortRange()
        {
        }
    }
}
