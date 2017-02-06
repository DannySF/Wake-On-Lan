using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using Wake_On_Lan.Annotations;

namespace Wake_On_Lan.Models {
    [Serializable]
    [XmlRoot("Computers")]
    public class Computer : INotifyPropertyChanged {
        private string _name;
        private string _mac;
        private string _ip;
        private bool _alive;

        public Computer() {
            IP = "0.0.0.0";
            MAC = "__-__-__-__-__-__";
            Name = "No Name";
        }

        public Computer(string name, string mac, string ip) {
            Name = name;
            IP = ip;
            MAC = mac;
        }

        [XmlElement("Name")]
        public string Name {
            get { return _name; }
            set {
                if (_name != value) {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        [XmlElement("MAC")]
        public string MAC {
            get { return _mac; }
            set {
                if (_mac != value) {
                    _mac = value;
                    OnPropertyChanged(nameof(MAC));
                }
            }
        }
        [XmlElement("IPAddr")]
        public string IP {
            get { return _ip; }
            set {
                if (_ip != value) {
                    _ip = value;
                    OnPropertyChanged(nameof(IP));
                }
            }
        }

        public string GetBroadCastIP() => _ip.Split('.')[0] + "." + _ip.Split('.')[1] + "." + _ip.Split('.')[2] + ".255";

        public int Counter { get; set; }

        public void AddToCounter() {
            Counter++;
            OnPropertyChanged(nameof(Counter));
        }

        public bool Alive {
            get { return _alive;}
            set {
                if (_alive != value) {
                    _alive = value;
                    OnPropertyChanged(nameof(Alive));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString() {
            return Name;
        }
    }
}
