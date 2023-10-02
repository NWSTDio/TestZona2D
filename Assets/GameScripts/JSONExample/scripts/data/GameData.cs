using System;
using System.Collections.Generic;

namespace GameScripts.JSONExample {
    [Serializable]
    public class GameData {

        public List<Item> Items;

        public GameData() {
            Items = new();
            }

        }
    }