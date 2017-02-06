using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Wake_On_Lan.Annotations;

namespace Wake_On_Lan.Services {
    public class AsyncCommand<TResult> : AsyncCommandBase, INotifyPropertyChanged {
        private readonly Func<Task<TResult>> _command;
        private NotifyTaskCompletion<TResult> _execution;
        public AsyncCommand(Func<Task<TResult>> command) {
            _command = command;
        }
        public override bool CanExecute(object parameter) {
            return true;
        }
        public override Task ExecuteAsync(object parameter) {
            Execution = new NotifyTaskCompletion<TResult>(_command());
            return Execution.TaskCompletion;
        }
        // Raises PropertyChanged
        public NotifyTaskCompletion<TResult> Execution { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
