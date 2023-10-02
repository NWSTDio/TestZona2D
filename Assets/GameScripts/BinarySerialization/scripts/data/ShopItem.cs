using System;

namespace GameScripts.BinarySerialization {
    [Serializable]
    public struct ShopItem {

        public string title;
        public string description;
        public int id;
        public float price;

        }
    }