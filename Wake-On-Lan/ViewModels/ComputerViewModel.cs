using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using System.Xml.Serialization;
using Wake_On_Lan.Interfaces;
using Wake_On_Lan.Models;
using Wake_On_Lan.Services;

namespace Wake_On_Lan.ViewModels {
    public class ComputerViewModel {

        private readonly IWakeOnLan _wakeOnLan;
        private readonly PingComputer _pinger;
        
        public ComputerViewModel(IWakeOnLan wakeOnLan) {
            _wakeOnLan = wakeOnLan;
            _pinger = new PingComputer();
            Command = new AsyncCommand<bool>(() => _wakeOnLan.WakeComputer(SelectedComputer));
            Ping = new AsyncCommand<bool>(Alive);
        }

        public IAsyncCommand Command { get; private set; }
        public IAsyncCommand Ping { get; private set; }

        public ObservableCollection<Computer> Computers { get; set; }
        private Computer _computer;
        public Computer SelectedComputer {
            get { return _computer; }
            set {
                if (_computer != value) {
                    _computer = value;
                }
            }
        }
       
        public void LoadComputers() {
            var loadedComputers = new ObservableCollection<Computer>();
            try {
                if (!File.Exists(@"..\\..\\Computers.xml")) {
                    var document = new XDocument(new XElement("Computers"));
                    document.Save(@"..\\..\\Computers.xml");
                }
                var xElement = XElement.Load(@"..\\..\\Computers.xml");
                var comps = DeSerializer(xElement);
                foreach (var computer in comps.ComputerList) {
                    loadedComputers.Add(computer);
                }
            }
            catch (Exception e) {
                MessageBox.Show(e.Message);
            }
            Computers = loadedComputers;
        }

        public void SaveComputers() {
            var output = new SavedComputers {ComputerList = Computers.ToList()};
            try {
                var serializer = new XmlSerializer(typeof(SavedComputers));
                using (var file = new StreamWriter(@"..\\..\\Computers.xml")) {
                    serializer.Serialize(file, output);
                }
            }
            catch (Exception e) {
                MessageBox.Show(e.Message);
            }
        }

        private async Task<bool> Alive() {
            var result = await _pinger.IsAlive(SelectedComputer);
            MessageBox.Show(result.ToString());
            return result;
        }

        public async void PingAll(object sender, object e) {
            foreach (var computer in Computers) {
                computer.Alive = await _pinger.IsAlive(computer);
            }
        }

        private SavedComputers DeSerializer(XElement element) {
            var serializer = new XmlSerializer(typeof(SavedComputers));
            var output = (SavedComputers) serializer.Deserialize(element.CreateReader());
            return output;
        }

    }
}
