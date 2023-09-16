using System.IO;
using UnityEngine;

namespace SimpleJSON {
    public static class Json {

        public static bool IsExistsSave(string path) => File.Exists(path);// проверка есть ли файл с данными

        public static T Load<T>(string path) { // загрузка данных
            string data = File.ReadAllText(path);

            return JsonUtility.FromJson<T>(data);
            }

        public static void Save(string path, object obj) { // сохранения данных
            string data = JsonUtility.ToJson(obj);

            File.WriteAllText(path, data);
            }

        }
    }