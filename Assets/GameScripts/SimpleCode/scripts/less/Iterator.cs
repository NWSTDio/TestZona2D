using System.Collections.Generic;
using UnityEngine;

namespace SimpleCode.SimpleIterator {
    public class Iterator {

        public Iterator() {
            Debug.Log("!!! пример работы итератора");

            foreach (int fib in Fibs(6))
                Debug.Log(fib);

            foreach (string i in Nums(10))
                Debug.Log(i);

            foreach (string str in Test())
                Debug.Log(str);
            // итераторы можно компоновать

            foreach (int fib in EvenNumberOnly(Fibs(6)))
                Debug.Log(fib);// выдаст только четные числа последовательности фибоначчи
            }

        private IEnumerable<int> Fibs(int fib) {
            for (int i = 0, prevFibs = 1, curFibs = 1; i < fib; i++) {
                yield return prevFibs;

                int newFib = prevFibs + curFibs;

                prevFibs = curFibs;

                curFibs = newFib;
                }
            }
        private IEnumerable<int> EvenNumberOnly(IEnumerable<int> sequence) {
            foreach (int x in sequence)
                if (x % 2 == 0)
                    yield return x;
            }
        private IEnumerable<string> Nums(int num) {
            for (int i = 0; i < num; i++)
                yield return "число " + i;
            }
        private IEnumerable<string> Test() {
            yield return "ONE";

            yield return "TWO";

            if (Random.Range(0, 2) == 0)
                yield break;// может и не выполнится третий итератор

            yield return "THREE";
            }
        }
    }