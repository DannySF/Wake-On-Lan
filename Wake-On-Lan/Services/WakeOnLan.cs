using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Wake_On_Lan.Interfaces;
using Wake_On_Lan.Models;

namespace Wake_On_Lan.Services {
    public class WakeOnLan : IWakeOnLan {

        public WakeOnLan() {
            
        }

        public async Task<bool> WakeComputer(Computer computer) {
            var client = new UdpClient();
            var datagram = new byte[102];
            for (var i = 0; i <= 5; i++) {
                datagram[i] = 0xff;
            }

            var macDigits = computer.MAC.ToString().Split('-');

            var start = 6;
            for (var i = 0; i < 16; i++) {
                for (var j = 0; j < 6; j++) {
                    datagram[start + i * 6 + j] = (byte) Convert.ToInt32(macDigits[j], 16);
                }
            }

            await client.SendAsync(datagram, datagram.Length, computer.GetBroadCastIP(), 3);
            computer.AddToCounter();
            return true;
        }
    }
}
