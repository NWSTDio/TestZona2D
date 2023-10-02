using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace GameScripts.EncryptingExample {
    public class EncryptingUtills : MonoBehaviour {

        private static readonly byte[] Key = Guid.NewGuid().ToByteArray();// ключ по умолчанию
        private static readonly string SaltKey = "ShMG8hLyZ7k~Ge5@", VIKey = "~6YUi0Sv5@|{aOZO";
        private static readonly int KeyLength = 128;

        // обычное кодирование/декодирование строки в base64
        public static string Encode64(string data) => Convert.ToBase64String(GetBytesFromString(data));
        public static string Decode64(string data) => GetStringFromBytes(Convert.FromBase64String(data));

        // шифрование по AES (Advanced Encryption Standard)
        // шифрование/дешифрование строки по пользовательскому ключу
        public static string Encrypt(string value, string password) => Encrypt(GetBytesFromString(value), password);
        public static string Encrypt(byte[] value, string password) {
            var keyBytes = new Rfc2898DeriveBytes(password, GetBytesFromString(SaltKey)).GetBytes(KeyLength / 8);
            var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, GetBytesFromString(VIKey));

            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

            cryptoStream.Write(value, 0, value.Length);
            cryptoStream.FlushFinalBlock();

            cryptoStream.Close();
            memoryStream.Close();

            return Convert.ToBase64String(memoryStream.ToArray());
            }
        public static string Decrypt(string value, string password) {
            var cipherTextBytes = Convert.FromBase64String(value);
            var keyBytes = new Rfc2898DeriveBytes(password, GetBytesFromString(SaltKey)).GetBytes(KeyLength / 8);
            var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC, Padding = PaddingMode.None };
            var decryptor = symmetricKey.CreateDecryptor(keyBytes, GetBytesFromString(VIKey));

            using var memoryStream = new MemoryStream(cipherTextBytes);
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

            var plainTextBytes = new byte[cipherTextBytes.Length];
            var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

            memoryStream.Close();
            cryptoStream.Close();

            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
            }

        // быстрая шифровка
        public static string FastEncrypt(string value) => FastEncrypt(value, Key);// с использованием ключа сессии
        public static string FastEncrypt(string value, string key) => FastEncrypt(value, GetBytesFromString(key));// со своим ключом
        // быстрая расшифровка
        public static string FastDecrypt(string value) => FastDecrypt(value, Key);// с использованием ключа сессии
        public static string FastDecrypt(string value, string key) => FastDecrypt(value, GetBytesFromString(key));// со своим ключом

        // перевод из строки в байты и наоборот
        public static byte[] GetBytesFromString(string data) => Encoding.UTF8.GetBytes(data);
        public static string GetStringFromBytes(byte[] data) => Encoding.UTF8.GetString(data);

        private static string FastEncrypt(string value, byte[] key) => Convert.ToBase64String(FastEncode(GetBytesFromString(value), key));// шифровка
        private static string FastDecrypt(string value, byte[] key) => Encoding.UTF8.GetString(FastEncode(Convert.FromBase64String(value), key));// разшифровка

        // алгоритм шифровки данных
        private static byte[] FastEncode(byte[] bytes, byte[] key) {
            for (int i = 0, j = 0; i < bytes.Length; i++) { // пройдем по всем символам
                bytes[i] ^= key[j];// инвертируем байты символа

                if (++j == key.Length) // если следующий символ ключа за пределами ключа
                    j = 0;// сбросс индекса символа ключа
                }

            return bytes;
            }

        }
    }