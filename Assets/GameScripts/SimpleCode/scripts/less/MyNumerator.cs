using System;
using System.Collections;
using UnityEngine;

namespace SimpleCode.SimpleMyNumerator {
    public class MyNumerator {

        public MyNumerator() {
            var counter = new Counter(10);
            while (counter.MoveNext())
                Debug.Log(counter.Current);

            var counter2 = new Counter2(10);
            while (counter2.MoveNext())
                Debug.Log(counter2.Current);
            }

        }

    public class Counter : IEnumerator { // создаем свой вариант счетчика
        private int _count;

        public object Current => _count;

        public Counter(int count) {
            if (count < 0)
                count = 0;

            _count = count;
            }

        public bool MoveNext() => _count-- > 0;

        public void Reset() => throw new NotSupportedException();
        }

    public class Counter2 {
        private int _count;

        public Counter2(int count) {
            if (count < 0)
                count = 0;
            _count = count;
            }

        public object Current => _count;

        public bool MoveNext() => _count-- > 0;
        }
    }