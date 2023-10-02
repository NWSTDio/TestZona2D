using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


namespace GameScripts.BinarySerialization {
    public class Storage {

        private readonly BinaryFormatter _formatter;// упаковщик
        private readonly string _fileName = "save.sav";// файл для сохранений данных класса
        private string _path;// путь хранения файла

        public Storage() {
            _path = Path.Combine(Application.persistentDataPath, _fileName);

            _formatter = new BinaryFormatter();

            // обьявление типов данных что нельзя так просто упаковать
            var selector = new SurrogateSelector();

            // типы данных что нельзя так просто упаковать
            var vectorSurrogate = new Vector2Serialization();
            var rectSurrogate = new RectSerialization();

            // добавим их
            selector.AddSurrogate(typeof(Vector2), new StreamingContext(StreamingContextStates.All), vectorSurrogate);
            selector.AddSurrogate(typeof(Rect), new StreamingContext(StreamingContextStates.All), rectSurrogate);

            _formatter.SurrogateSelector = selector;
            }

        public bool IsExistsSave() => File.Exists(_path);// есть ли сохранение

        public T Load<T>() { // загрузим данные
            using FileStream stream = new(_path, FileMode.Open);

            return (T)_formatter.Deserialize(stream);
            }

        public void Save(object data) { // сохраним данные
            using FileStream stream = new(_path, FileMode.OpenOrCreate);

            _formatter.Serialize(stream, data);
            }

        }
    }
