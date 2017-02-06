using System.Threading.Tasks;
using Wake_On_Lan.Models;

namespace Wake_On_Lan.Interfaces {
    public interface IPingComputer {
        Task<bool> IsAlive(Computer computer);
    }
}
