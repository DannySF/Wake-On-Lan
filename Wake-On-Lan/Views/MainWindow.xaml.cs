using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using Wake_On_Lan.Services;
using Wake_On_Lan.ViewModels;

namespace Wake_On_Lan {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private ComputerViewModel _computerViewModel;

        public MainWindow() {
            
            InitializeComponent();
            Load();
            
        }

        private void Load() {
            var wakeOnLan = new WakeOnLan();
            _computerViewModel = new ComputerViewModel(wakeOnLan);
            _computerViewModel.LoadComputers();
            ComputerViewControl.DataContext = _computerViewModel;
            var pingTimer = new DispatcherTimer {Interval = TimeSpan.FromMinutes(1)};
            pingTimer.Tick += _computerViewModel.PingAll;
            pingTimer.Start();
            _computerViewModel.PingAll(null, null);
        }

        private void SaveOnClosing(object sender, CancelEventArgs e) {
            _computerViewModel.SaveComputers();
        }
    }
}
