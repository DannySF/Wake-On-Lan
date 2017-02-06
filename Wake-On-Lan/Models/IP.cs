using System;
using System.Linq;

namespace Wake_On_Lan.Models {
    public class IP {
        public IP(string ip) {
            SetIP(ip);
        }

        public IP() {
            
        }
        public int First { get; set; }
        public int Second { get; set; }
        public int Third { get; set; }
        public int Fourth { get; set; }

        public string GetIP => First + "." + Second + "." + Third + "." + Fourth;

        public void SetIP(string ip) {
            try {
                var numberArray = ip.Split('.');
                if (numberArray.Length != 4) {
                    throw new ArgumentException(@"IP must be given as xxx.xxx.xxx.xxx", ip);
                }
                var ipNumbers = new int[4];
                var bools = new bool[4];
                var i = 0;
                foreach (var s in numberArray) {
                    bools[i] = int.TryParse(s, out ipNumbers[i]);
                    i++;
                }
                if (bools.Any(x => false)) {
                    throw new ArgumentException(@"IP must be given as integers xxx.xxx.xxx.xxx where x is a single digit integer.", ip);
                }

                if (ipNumbers[0] < 1 && ipNumbers[0] > 255) {
                    throw new ArgumentException(@"First number in an IP must not be smaller than 1 or greater than 255");
                }

                if (ipNumbers[1] < 1 && ipNumbers[1] > 255) {
                    throw new ArgumentException(@"Second number in an IP must not be smaller than 1 or greater than 255");
                }

                if (ipNumbers[2] < 1 && ipNumbers[2] > 255) {
                    throw new ArgumentException(@"Third number in an IP must not be smaller than 1 or greater than 255");
                }

                if (ipNumbers[3] < 1 && ipNumbers[3] > 255) {
                    throw new ArgumentException(@"Fourth number in an IP must not be smaller than 1 or greater than 255");
                }

                First = ipNumbers[0];
                Second = ipNumbers[1];
                Third = ipNumbers[2];
                Fourth = ipNumbers[3];
            }
            catch (Exception) {
                throw new ArgumentException(@"IP must be given as xxx.xxx.xxx.xxx", ip);
            }
        }

        public override string ToString() {
            return First + "." + Second + "." + Third + "." + Fourth;
        }
    }
}
