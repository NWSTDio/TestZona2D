using System;
using UnityEngine;

namespace BookLessons
    {
    internal class SimpleTuple
        {
        public SimpleTuple()
            {
            Debug.Log("!!! работа с кортежами");

            // создание:
            var t = new Tuple<int, string>(123, "hello");
            Debug.Log(t.Item1 * 2);
            Debug.Log(t.Item2.ToUpper());
            // каждый кортеж содержит свойство, только для чтения и начинается с Item....
            // существует 8 видов кортежей, от 1 до 8 параметров принимающих

            Tuple<int> t2 = Tuple.Create(100);
            Debug.Log(t2.Item1 - 100);

            var t3 = Tuple.Create(100, 200, 300);
            Debug.Log(t3.Item1);
            int i = t3.Item2 - 100;
            Debug.Log(t3.Item2);

            // кортежи удобны при возвращении из метода более одного значения
            Vector3 position = new Vector3(100, 0, 0);
            var ttt = Moved(position);
            if (ttt.Item1)
                Debug.Log("moved!");

            // если сравнивать два одинаковых кортежа через == будет false в любом случае
            Tuple<int> test1 = Tuple.Create(100);
            Tuple<int> test2 = Tuple.Create(100);
            Debug.Log(test1 == test2);// false
            // нужно применить
            Debug.Log(test1.Equals(test2));// true, т.к. сравниваются между собой значения в кортежах
            }
        private Tuple<bool, Vector3> Moved(Vector3 start)
            {
            Vector3 pos = start - Vector3.left;
            
            if (pos.x <= 0)
                return Tuple.Create(false, start);

            return Tuple.Create(true, pos);
            }
        }
    }