using UnityEngine;

namespace SimpleCode.SimpleGeneralizationType {
    public class GeneralizationType {

        public GeneralizationType() {
            var stack = new Stack<int>();
            stack.Push(2);
            stack.Push(777);
            stack.Push(200);

            Debug.Log(stack.Pop());

            var stack2 = new Stack<Vector3>();
            stack2.Push(new Vector3() {
                x = 0,
                y = 0,
                z = 0
                });
            }

        public static void Clear<T>(T[] ars) {
            for (int i = 0; i < ars.Length; i++)
                ars[i] = default;// установим стандартное значение для указанного типа
            }

        }

    public class Stack<T> { // универсальный стек, для определенного типа
        private readonly T[] _data = new T[100];
        private int _position;

        public void Push(T obj) => _data[_position++] = obj;
        public T Pop() => _data[--_position];
        }
    }