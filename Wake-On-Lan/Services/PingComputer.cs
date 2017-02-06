using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Wake_On_Lan.Interfaces;
using Wake_On_Lan.Models;

namespace Wake_On_Lan.Services {
    public class PingComputer : IPingComputer {
        public async Task<bool> IsAlive(Computer computer) {
            var pingOptions = new PingOptions(128, true);
            var ping = new Ping();
            var buffer = new byte[32];
            for (var i = 0; i < 4; i++) {
                var pingReply = await ping.SendPingAsync(computer.IP, 500, buffer, pingOptions);

                if (pingReply != null) {
                    switch (pingReply.Status) {
                        case IPStatus.Success:
                            return true;
                        case IPStatus.DestinationNetworkUnreachable:
                        case IPStatus.DestinationHostUnreachable:
                        case IPStatus.DestinationProtocolUnreachable:
                        case IPStatus.DestinationPortUnreachable:
                        case IPStatus.NoResources:
                        case IPStatus.BadOption:
                        case IPStatus.HardwareError:
                        case IPStatus.PacketTooBig:
                        case IPStatus.TimedOut:
                        case IPStatus.BadRoute:
                        case IPStatus.TtlExpired:
                        case IPStatus.TtlReassemblyTimeExceeded:
                        case IPStatus.ParameterProblem:
                        case IPStatus.SourceQuench:
                        case IPStatus.BadDestination:
                        case IPStatus.DestinationUnreachable:
                        case IPStatus.TimeExceeded:
                        case IPStatus.BadHeader:
                        case IPStatus.UnrecognizedNextHeader:
                        case IPStatus.IcmpError:
                        case IPStatus.DestinationScopeMismatch:
                        case IPStatus.Unknown:
                        default:
                            return false;
                    }
                }
            }
            return false;
        }
    }
}
