using System;
using UnityEngine;

namespace GameScripts.JSONExample {
    [Serializable]
    public class Item {

        public string Name;
        public int Count;
        public bool IsStackable;
        public Vector3 Posiition;

        }
    }