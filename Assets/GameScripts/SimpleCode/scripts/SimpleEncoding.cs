using System.Text;
using UnityEngine;

namespace BookLessons
    {
    internal class SimpleEncoding
        {
        public SimpleEncoding()
            {
            Debug.Log("!!! пример работы Encoding");

            // работа с кодировкой класс Encoding
            Encoding utf8 = Encoding.GetEncoding("utf-8");// аналог ниже
            Encoding utf8_2 = Encoding.UTF8;

            Encoding chinese = Encoding.GetEncoding("GB18030");

            // список всех кодировок
            foreach (var e in Encoding.GetEncodings())
                Debug.Log(e.Name);

            // наиболее растпространненное место работы с кодировками это работа с файлами
            // System.IO.File.WriteAllText("data.txt", "Text", Encoding.Unicode);// запишет тектс Text в файл data.txt c кодировкой utf-16

            // UTF-8 является стандартной кодировкой

            // кодирование и байтовые массивы
            string str = "0123456789";
            
            byte[] utf8bytes = Encoding.UTF8.GetBytes(str);
            byte[] utf16bytes = Encoding.Unicode.GetBytes(str);
            byte[] utf32bytes = Encoding.UTF32.GetBytes(str);

            Debug.Log(utf8bytes.Length);
            Debug.Log(utf16bytes.Length);
            Debug.Log(utf32bytes.Length);

            string original1 = Encoding.UTF8.GetString(utf8bytes);
            string original2 = Encoding.Unicode.GetString(utf16bytes);
            string original3 = Encoding.UTF32.GetString(utf32bytes);

            Debug.Log(original1);
            Debug.Log(original2);
            Debug.Log(original3);
            }
        }
    }