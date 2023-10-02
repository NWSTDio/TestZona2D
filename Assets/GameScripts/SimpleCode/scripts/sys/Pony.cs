using UnityEngine;

namespace SimpleCode {
    public class Pony {
        public static int Count = 0;

        public int Age = 7;

        private readonly string Name;

        public Pony(string name) {
            Name = name;
            Count += 1;
            }

        public void Msg() => Debug.Log($"пони {Name} говорит привет!");
        public override string ToString() => $"пони по имени {Name} возврастом {Age} лет.";
        }
    }