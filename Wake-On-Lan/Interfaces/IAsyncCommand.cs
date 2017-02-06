using System.Threading.Tasks;
using System.Windows.Input;

namespace Wake_On_Lan.Interfaces {
    public interface IAsyncCommand : ICommand {
        Task ExecuteAsync(object parameter);
    }
}
