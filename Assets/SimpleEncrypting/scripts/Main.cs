using UnityEngine;

namespace SimpleEncrypting {
    public class Main : MonoBehaviour {

        private readonly string _password = "MLP:FiM";

        private void Start() {
            string mlp = "My Little Pony: Friendship is Magic!";
            Debug.Log("Базовая строка:\n" + mlp);

            TestBase64(mlp);// простое не безопасное шифрование данных
            TestBytesString(mlp);// перевод строки в массив байтов и обратно

            Debug.Log("Основной пароль:\n" + _password);

            // шифровка пароля с системным ключом
            string password = EncryptingUtills.FastEncrypt(_password);
            Debug.Log("Зашифрованный пароль:\n" + password);
            Debug.Log("Разшифрованный пароль:\n" + EncryptingUtills.FastDecrypt(password));

            // шифровка пароля с моим ключом
            string key = "Twilight Sparkle";// мой ключ
            Debug.Log("Мой ключ:\n" + key);

            string password_2 = EncryptingUtills.FastEncrypt(mlp, key);// шифрованный пароль
            Debug.Log("Зашифрованный пароль с моим ключем:\n" + password_2);
            Debug.Log("Разшифрованный пароль c правильным ключем:\n" + EncryptingUtills.FastDecrypt(password_2, key));
            Debug.Log("Разшифрованный пароль c ключем по умолчанию:\n" + EncryptingUtills.FastDecrypt(password_2));
            Debug.Log("Разшифрованный пароль c неправильным ключем:\n" + EncryptingUtills.FastDecrypt(password_2, "bad key"));

            string content = "I have a problem with code for a camera scene for android. I can pan my camera fine, but once i use two fingers my code confuses a pinch with a two finger swipe. I want to zoom when i pinch, and rotate when i swipe with two fingers, but it always does both if i try to swipe. I figured if i could draw a line between both points using their coordinates and then translate that line to the left and right of both points(or if points or horizontal to translate it below and above the points) and then check if either finger goes out of these two lines and if it does cancel that swipe possibility.";
            Debug.Log("Основной контент для шифрования:\n" + content);

            // тестирование шифрования по AES
            TestEncrypt(content, _password);
            TestEncrypt(content, mlp);
            TestEncrypt(content, password);
            TestEncrypt(content, password_2);

            MyTestSecurity();// тест работы шифрования
            }

        private void TestBase64(string data) {
            string en_64 = EncryptingUtills.Encode64(data);
            Debug.Log("Закодированная строка в base64:\n" + en_64);

            string de_64 = EncryptingUtills.Decode64(en_64);
            Debug.Log("Разкодированная строка из base64:\n" + de_64);
            }

        private void TestBytesString(string data) {
            byte[] bytes = EncryptingUtills.GetBytesFromString(data);

            string msg = "";
            for (int i = 0; i < bytes.Length; i++)
                msg += bytes[i] + " ";

            Debug.Log("Строка переведенная в массив байтов:\n" + msg);

            string str = EncryptingUtills.GetStringFromBytes(bytes);
            Debug.Log("Строка полученная из массива байтов:\n" + str);
            }

        private void TestEncrypt(string content, string password) {
            Debug.Log("-- -- -- --  -- -- -- --  -- -- -- --  -- -- -- --");
            Debug.Log("Парлоль для шифрования:\n" + password);

            string encrypt = EncryptingUtills.Encrypt(content, password);
            Debug.Log("Зашифрованый текст:\n" + encrypt);
            Debug.Log("Расшифрованый текст c правильным паролем:\n" + EncryptingUtills.Decrypt(encrypt, password));
            Debug.Log("Расшифрованый текст c неправильным паролем:\n" + EncryptingUtills.Decrypt(encrypt, "some bad password"));
            }

        private void MyTestSecurity() {
            string en_64 = EncryptingUtills.Encode64(_password);
            Debug.Log("Шифрованный пароль в base64:\n" + en_64);

            string password = EncryptingUtills.FastEncrypt(en_64, "Twilight Sparkle");
            Debug.Log("Шифрованный пароль c ключом:\n" + password);

            string content = "Сумеречная Искорка — персонаж из мультсериала «Дружба — это чудо». Она — аликорн; в прошлом была единорогом сиреневого цвета, с гривой цвета индиго и фиолетовой и розовой прядями. Её знак отличия — розовая шестиконечная звезда в окружении пяти маленьких белых звёзд.";
            Debug.Log("Контент для шифрования:\n" + content);

            string encrypt = EncryptingUtills.Encrypt(content, password);
            Debug.Log("Зашифрованный контент:\n" + encrypt);
            Debug.Log("Розшифрованный контент:\n" + EncryptingUtills.Decrypt(encrypt, password));
            }

        }
    }