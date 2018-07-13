using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Encryptionv2 {
    class Program {
        static void Main(string[] args) { new Program(); }

        public bool unitTest = false;

        public Program() {

            if (unitTest) {

                /* Key Cycle unit test
                 * 
                string _key = "abcdefg";
                List<string> cycled = CycleKey(_key);
                Console.Write("'" + _key + "' to '" + cycled + "'");*/

                /* Encrypt Decrypt unit test (ERROR) Unit test comparison is shit.
                 * 
                byte a = 130, b = 130;
                if (a.CompareTo(b) == a) {
                    Console.Write("FUCK");
                }
                byte[] originalData = File.ReadAllBytes("C:\\Users\\Hunter\\Desktop\\boi.txt");
                byte[] cipheredData = EncryptKey("C:\\Users\\Hunter\\Desktop\\boi.txt", "abcdefg");
                File.WriteAllBytes("C:\\Users\\Hunter\\Desktop\\boi ciphered.txt", cipheredData);
                byte[] decipheredData = DecryptKey("C:\\Users\\Hunter\\Desktop\\boi ciphered.txt", "abcdefg");
                File.WriteAllBytes("C:\\Users\\Hunter\\Desktop\\boi deciphered.txt", decipheredData);
                if (CompareUnitTest(originalData, decipheredData)) {
                    Console.Write("\n Cipher Succeeded");
                } else {
                    Console.Write("\n Cipher Failed");
                }*/

                Console.ReadKey();
                return;
            }

            Console.Write("\n Cipher or Decipher > ");
            string resp = Console.ReadLine();
            Console.Write("\n File > ");
            string path = Console.ReadLine();
            Console.Clear();
            Console.Write("\n Key > ");
            string key = Console.ReadLine();

            byte[] buffer;
            if (resp.ToLower().Equals("cipher")) {
                buffer = EncryptKey(path, key);
            } else {
                buffer = DecryptKey(path, key);
            }

            File.WriteAllBytes(path, buffer);

            Console.ReadKey();

        }

        public byte[] EncryptKey(string path, string _key) {

            string[] key = CycleKey(_key).ToArray();

            byte[] buffer = File.ReadAllBytes(path);

            for (int j = 0; j < key.Length; j++) {
                int keyCount = 0;
                List<byte> result = new List<byte>();

                for (int i = 0; i < buffer.Length; i++) {

                    byte a = Convert.ToByte(buffer[i]);
                    byte b = Convert.ToByte(key[j][keyCount]);

                    a += b;

                    result.Add(a);

                    keyCount++;
                    if (keyCount <= key.Length) {
                        keyCount = 0;
                    }
                }

                buffer = result.ToArray();
            }

            return buffer;
        }

        public byte[] DecryptKey(string path, string _key) {

            string[] key = CycleKey(_key).ToArray();

            byte[] buffer = File.ReadAllBytes(path);

            for (int j = 0; j < key.Length; j++) {
                int keyCount = 0;
                List<byte> result = new List<byte>();

                for (int i = 0; i < buffer.Length; i++) {

                    byte a = Convert.ToByte(buffer[i]);
                    byte b = Convert.ToByte(key[key.Length - (1 + j)][keyCount]);

                    a -= b;

                    result.Add(a);

                    keyCount++;
                    if (keyCount <= key.Length) {
                        keyCount = 0;
                    }
                }

                buffer = result.ToArray();
                //return result.ToArray();
            }

            return buffer;
        }

        public List<string> CycleKey(string key) {

            bool done = false;

            List<string> keys = new List<string>();
            keys.Add(key);

            while (!done) {

                byte offset = Convert.ToByte(key[key.Length - 1]);

                string newKey = "";

                foreach (char a in key.Substring(0, key.Length - 1)) {
                    byte newByte = Convert.ToByte(a);
                    newByte += offset;
                    char newChar = Convert.ToChar(newByte);
                    newKey += newChar;
                }

                keys.Add(newKey);

                key = newKey;
                if (key.Length <= 1) {
                    done = true;
                }
            }

            return keys;
        }

        public bool CompareUnitTest(byte[] a, byte[] b) {

            bool c = true;

            try {
                for (int i = 0; i < a.Length; i++) {
                    if (!(a[i].CompareTo(b[i]) == a[i])) {
                        c = false;
                    }
                }
            } catch (Exception e) {
                Console.Write("Error Occured");
                c = false;
            }

            return c;
        }
    }
}