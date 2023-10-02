using System;
using UnityEngine;

namespace SimpleCode.SimpleDelegates {
    public delegate int TestDelegate(int x);
    public delegate void GroupDelegate(string str);
    public delegate T MyDelegate<T>(T arg);// обобщенный делегат
    //public delegate T MyDelegate<T, U>(T arg);// обобщенный делегат
    public delegate void MyDelegate2<T>(T arg);// обобщенный делегат
    public delegate void PriceChangedHandler(float oldPrice, float newPrice);
    public class Delegates {
        private PriceChangedHandler _priceHandler;
        public event PriceChangedHandler PriceHandler {
            add { _priceHandler += value; }
            remove { _priceHandler -= value; }
            }
        public Delegates() {
            Debug.Log("!!! пример работы делегатов");

            TestDelegate test = Square;// сокращенно от new TestDelegate(Square);
            Debug.Log(test(10));// сокращенно от test.Invoke(10);

            int[] values = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            Util.Transform(values, Square);
            foreach (int i in values)
                Debug.Log(i);

            Util.Transform(values, Sum);
            foreach (int i in values)
                Debug.Log(i);

            GroupDelegate group = null;

            group += Msg;
            group += Test;
            group += Log;

            group("Zipp");// вызовет все три метода Msg Test и Log
            Debug.Log("===> " + group?.Method);
            Debug.Log("T ===> " + group?.Target);

            // не забываем удалить методы, после того как по пользовались ими
            group -= Log;
            group -= Test;

            group("Izzy");// вызовет единственный метод Msg()
            Debug.Log("===> " + group?.Method);
            Debug.Log("T ===> " + group?.Target);

            group -= Msg;

            // group("Pipp");// ошибка
            group?.Invoke("Pipp");// безопасней
            Debug.Log("===> " + group?.Method);
            Debug.Log("T ===> " + group?.Target);

            var simple = new Simple();
            group += simple.Test;

            group?.Invoke("Zipp Storm!");
            Debug.Log("===> " + group?.Method);
            Debug.Log("T ===> " + group?.Target);

            int[] values2 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Util.Transformer(values2, MyTest);
            foreach (int i in values2)
                Debug.Log(i);

            int[] values3 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Util.Transformer2(values3, MyTest);
            foreach (int i in values2)
                Debug.Log(i);

            Util.Test("Hello", simple.Test);

            var group2 = new GroupDelegate(Log);
            group2?.Invoke("Horse");

            PriceHandler += Price;
            _priceHandler?.Invoke(100, 200);
            }

        public void Price(float oldPrice, float newPrice) => Debug.Log($"старая цена {oldPrice} новая цена {newPrice}");
        public int MyTest(int num) => num * num + num;
        private int Square(int x) => x * x;
        private int Sum(int x) => x + x;
        public void Msg(string str) => Debug.Log("MSG(string str) => " + str);
        public void Test(string str) => Debug.Log("Test(string str) => " + str);
        public void Log(string str) => Debug.Log("Log(string str) => " + str);

        }

    public class Util {
        public static void Transform(int[] values, TestDelegate test) {
            for (int i = 0; i < values.Length; i++)
                values[i] = test(values[i]);
            }
        public static void Transformer<T>(T[] values, MyDelegate<T> deleg) {
            for (int i = 0; i < 10; i++)
                values[i] = deleg(values[i]);
            }
        public static void Transformer2<T>(T[] values, Func<T, T> deleg) {
            for (int i = 0; i < 10; i++)
                values[i] = deleg(values[i]);
            }
        public static void Test<T>(T str, Action<T> action) {
            action?.Invoke(str);
            }
        }
    public class Simple {
        public void Test(string num) => Debug.Log("Test(string num) => " + num);
        }
    }

/*

делегаты в основном имеют тип возвращаемого значения void
лучьше использовать ?.Invoke() чтобы избежать ошибок если делегат пустой

 */