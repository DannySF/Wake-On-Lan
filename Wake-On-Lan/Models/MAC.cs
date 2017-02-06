using System;

namespace Wake_On_Lan.Models {
    public class MAC {

        public MAC(string mac) {
            SetMAC(mac);
        }

        public MAC() {
            
        }

        public int First { get; set; }
        public int Second { get; set; }
        public int Third { get; set; }
        public int Fourth { get; set; }
        public int Fifth { get; set; }
        public int Sixth { get; set; }

        public void SetMAC(string mac) {
            try {
                var array = mac.Split('-');
                if (array.Length != 6) {
                    throw new ArgumentException(@"MAC Address must contain 6 hexadecimals. XX-XX-XX-XX-XX-XX", mac);
                }
                var intArray = new int[6];
                var i = 0;
                foreach (var s in array) {
                    intArray[i] = Convert.ToInt32(s, 16);
                    i++;
                }
                First = intArray[0];
                Second = intArray[1];
                Third = intArray[2];
                Fourth = intArray[3];
                Fifth = intArray[4];
                Sixth = intArray[5];
            }
            catch (Exception) {
                throw new ArgumentException(@"MAC Address must be split with - after every hex. XX-XX-XX-XX-XX-XX", mac);
            }
        }

        public override string ToString() {
            return First.ToString("X") + "-" + Second.ToString("X") + "-" + Third.ToString("X") + "-" + Fourth.ToString("X") + "-" + Fifth.ToString("X") + "-" + Sixth.ToString("X");
        }
    }
}
