using System;
using System.Security.Cryptography;
using System.Text;

namespace Ui.Logic {
    internal class Hash {
        internal static string ComputeSHA256(string s) {
            // Erstellt den String hash
            string hash = String.Empty;

            // Erstellt ein SHA256-Hashobjekt
            using (SHA256 sha256 = SHA256.Create()) {
                // Errechnet den Hash aus dem gegebenen String s
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));

                // Konvertiert den Datentyp Byte in den String 'hash' um
                // 'hash' besteht aus einer Zusammensetzung aus allen Werten aus dem Byte-Array
                foreach (byte b in hashValue) {
                    hash += $"{b:X2}";
                }
            }

            // Gibt den fertigen 'hash'-String zurück
            return hash;
        }
    }
}