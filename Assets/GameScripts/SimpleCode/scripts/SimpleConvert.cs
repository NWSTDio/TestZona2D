using System;
using UnityEngine;

namespace BookLessons
    {
    internal class SimpleConvert
        {
        public SimpleConvert()
            {
            Debug.Log("!!! работа с классом Convert");

            float f = 1.5f;
            Debug.Log(Convert.ToBoolean(f));// true
            Debug.Log(Convert.ToByte(f));// 2
            int i = 100;
            Debug.Log(Convert.ToChar(i));// d
            Debug.Log(Convert.ToInt32(f));// 2
            Debug.Log(Convert.ToString(f));// "1.5"

            // в Convert работает правильное округление
            float summa = 123.456f;
            Debug.Log(Convert.ToInt32(summa));// 123
            summa += .1f;
            Debug.Log(Convert.ToInt32(summa));// 124

            // числа можно переводить в другие системы счислений
            string bin = "10100110";
            Debug.Log(Convert.ToInt32(bin, 2));// 166
            i = Convert.ToInt32(bin, 2);
            Debug.Log(Convert.ToString(i, 16));// a6

            int oct = 9;
            Debug.Log(Convert.ToString(oct, 8));// 11

            // динамическое преобразование
            Type target = typeof(int);
            object source = "42";
            object result = Convert.ChangeType(source, target);
            Debug.Log(result);// 42
            Debug.Log(result.GetType());// System.Int32
            }
        }
    }