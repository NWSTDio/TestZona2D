using UnityEngine;

namespace SimpleCode.SimpleObjectType {
    public class ObjectType {

        public ObjectType() {
            Debug.Log("---> пример работы с обьектами, розпаковка и упаковка типов");

            var stack = new Stack();
            stack.Push("Zipp");
            stack.Push("Pipp");
            stack.Push("Izzy");
            stack.Push("Sunny");

            string name = (string)stack.Pop();// розпаковка типа
            Debug.Log(name);// Sunny

            stack.Push(10);// обьектом может быть все что угодно
            int num = (int)stack.Pop();// главное правильно распаковать
            Debug.Log(num);// 10

            // пример явной упаковки обьекта
            int x = 100;
            object obj = x;// упаковка
            Debug.Log((int)obj);// распаковка

            // пример неявной упаковки обьекта
            obj = 9;
            //long l = (long)obj;// ошибка
            long l = (int)obj;// сработает
            int i = (int)obj;// тоже сработает
            Debug.Log(l + " " + i);

            obj = 3.5;
            int t = (int)(double)obj;// сначала розпакуем а потом явно преобразуем в int
            Debug.Log(t);

            object[] array = new string[10];// допустимо
            // object[] array = new int[10];// не допустимо

            // упаковка прячет значение в новый обьект и изменения с обьектом который упаковали ее не касаются
            i = 100;
            object boxed = i;
            i = 777;
            Debug.Log($"{i} : {boxed}");// 777 : 100
            }

        }

    public class Stack { // пример реализации стэка

        private readonly object[] _data = new object[10];
        private int _position;

        public void Push(object obj) {
            if (_position > _data.Length)
                return;

            _data[_position++] = obj;
            }
        public object Pop() {
            if (_position < 1)
                return null;

            return _data[--_position];
            }

        }
    }