using System.Globalization;
using UnityEngine;

namespace BookLessons
    {
    internal class SimpleToStringAndParse
        {
        public SimpleToStringAndParse()
            {
            Debug.Log("!!! пример работы методов ToString и Parse");

            string str = true.ToString();
            Debug.Log(str);
            bool b = bool.Parse(str);// если не удастся, то будет ошибка
            Debug.Log(b);

            int i;
            bool fail = int.TryParse("qwerty", out i);
            Debug.Log(i);
            bool success = int.TryParse("123", out i);
            Debug.Log(i);
            Debug.Log($"{fail} {success}");

            //Debug.Log(double.Parse("1.234"));// даст в германии 1234, но
            //Debug.Log(double.Parse("1.234", CultureInfo.InvariantCulture));// это исправит его
            }
        }
    }