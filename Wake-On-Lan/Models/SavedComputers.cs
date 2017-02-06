using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wake_On_Lan.Models {
    public class SavedComputers {
        [XmlArray("Computers"), XmlArrayItem(typeof(Computer))]
        public List<Computer> ComputerList { get; set; }
    }
}
