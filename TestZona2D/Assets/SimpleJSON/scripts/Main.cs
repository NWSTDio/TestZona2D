using System.Collections.Generic;
using UnityEngine;

namespace SimpleJSON {
    public class Main : MonoBehaviour {

        private GameData _gameData;// данные для сохранения
        private string _path;// путь хранения файла с данными
        private int _index;// индекс в инвентаря для работы с предметом

        public List<Item> Items => _gameData.Items;// инвентарь

        private void Start() {
            _path = Application.persistentDataPath + "/test.json";

            if (Json.IsExistsSave(_path)) // если есть сохранение
                _gameData = Json.Load<GameData>(_path);// загрузим сохранение
            else
                InitItems();// создадим новые данные

            foreach (Item item in Items) // выведем данные инвентаря
                Print(item);
            }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Q))
                PrevIndex();// предыдущий предмет

            if (Input.GetKeyDown(KeyCode.E))
                NextIndex();// следующий предмет

            if (Input.GetKeyDown(KeyCode.Space))
                RemoveItem();// убрать 1 эдиницу предмета
            }

        private void OnApplicationQuit() { // при закрытии приложения
            Json.Save(_path, _gameData);// сохраним данные
            }

        private void NextIndex() => ChangeIndex(_index + 1);// следующий индекс инвентаря
        private void PrevIndex() => ChangeIndex(_index - 1);// предыдущий индекс инвентаря
        private void ChangeIndex(int index) { // обновим индекс инвентаря
            _index = Mathf.Clamp(index, 0, Items.Count - 1);

            Debug.Log("Работаем с предметом " + Items[_index].Name + " по индексу " + _index);
            }

        private void RemoveItem() { // удалим 1 эдиницу предмета
            Item item = Items[_index];

            item.Count--;// удалим

            Print(item);

            if (item.Count <= 0) { // если предметов не осталось
                Debug.Log("Предмет " + item.Name + " полностью удален!");

                Items.Remove(item);// удалим предмет полностью

                ChangeIndex(_index);// обновим индекс инвентаря
                }
            }

        private void InitItems() { // инициализация предметов инвентаря
            _gameData = new GameData();

            var names = new string[] { "факел", "яблоко", "камень", "земля", "саженец" };

            for (int i = 0, max = Random.Range(5, 20); i < max; i++) {
                var item = new Item() {
                    Name = names[Random.Range(0, names.Length)],
                    Count = 64,
                    IsStackable = true,
                    Posiition = new Vector3() {
                        x = Random.Range(0, 100),
                        y = Random.Range(0, 100),
                        z = Random.Range(0, 100)
                        }
                    };

                Items.Add(item);
                }
            }

        private void Print(Item item) { // вывод данных предмета
            string msg = "Предмет: " + item.Name + "\n";

            msg += "В колличестве: " + item.Count + "\n";
            msg += (item.IsStackable ? "Стакабельный" : "Не стакабельный") + "\n";
            msg += "Находится в позиции: [" + item.Posiition.x + ", " + item.Posiition.y + ", " + item.Posiition.z + "]";

            Debug.Log(msg);
            }

        }
    }