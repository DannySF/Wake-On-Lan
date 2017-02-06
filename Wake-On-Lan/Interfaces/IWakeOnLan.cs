using System.Threading.Tasks;
using Wake_On_Lan.Models;

namespace Wake_On_Lan.Interfaces {
    public interface IWakeOnLan {
        Task<bool> WakeComputer(Computer computer);
    }
}
