using System.Collections.Generic;
using UnityEngine;

namespace GameScripts.BinarySerialization {
    public class Main : MonoBehaviour {

        private Storage _storage;// для работы с сохранением и загрузкой данных
        private GameData gameData;// хранимые данные

        private void Awake() {
            _storage = new Storage();

            if (_storage.IsExistsSave()) // если сохранение есть
                gameData = _storage.Load<GameData>();// грузим его
            else
                GenerateGameData();// создаем новые данные

            PrintGameData();// выводим полученные данные
            }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                gameData.points++;

                Debug.Log(gameData.points);
                }
            }

        private void OnApplicationQuit() { // при закрытии приложения
            _storage.Save(gameData);// сохраним данные
            }

        public void GenerateGameData() {
            var strings = new List<string> {
                "Hello World.",
                "Example string.",
                "Foo Bar",
                "Example title",
                "Example description"
                };

            int i = Random.Range(0, strings.Count);

            gameData = new GameData {
                someStringValue = strings[i],
                someIntValue = i,
                someLongValue = Random.Range(-10000, 10000),
                someDecimalValue = Random.Range(-100, 100),
                shopItems = new List<ShopItem>(),
                position = new Vector2() {
                    x = Random.Range(0, 777),
                    y = Random.Range(0, 777),
                    },
                bounds = new Rect(0, 0, 100, 100)
                };

            for (int j = 0, count = Random.Range(3, 10); j < count; j++) {
                int k = Random.Range(0, strings.Count);

                gameData.shopItems.Add(new ShopItem {
                    id = Random.Range(-10000, 10000),
                    price = Random.Range(-100, 100),
                    description = strings[k],
                    title = strings[k]
                    });
                }
            }

        private void PrintGameData() {
            string msg = "GameData:\n";

            msg += "POINTS: " + gameData.points + "\n";

            msg += gameData.someStringValue + "\n";
            msg += gameData.someDecimalValue + "\n";
            msg += gameData.someLongValue + "\n";
            msg += gameData.someIntValue + "\n";
            msg += "[" + gameData.position.x + " " + gameData.position.y + "]\n";
            msg += "[" + gameData.bounds.x + " " + gameData.bounds.y + "; " + gameData.bounds.width + " * " + gameData.bounds.height + "]\n";

            foreach (ShopItem i in gameData.shopItems) {
                msg += "--- Item: ---\n";
                msg += i.title + "\n";
                msg += i.description + "\n";
                msg += i.id + "\n";
                msg += i.price + "\n";
                }

            Debug.Log(msg);
            }
        }
    }