using System;
using System.Collections.Generic;
using UnityEngine;

namespace BinarySerialization {
    [Serializable]
    public struct GameData {

        public List<ShopItem> shopItems;
        public Vector2 position;
        public string someStringValue;
        public decimal someDecimalValue;
        public long someLongValue;
        public int someIntValue;
        public int points;
        public Rect bounds;

        }
    }