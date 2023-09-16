using System;
using System.Collections.Generic;

namespace SimpleJSON {
    [Serializable]
    public class GameData {

        public List<Item> Items;

        public GameData() {
            Items = new();
            }

        }
    }