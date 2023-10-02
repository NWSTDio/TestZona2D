
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SimpleCode.SimpleStrings {
    public class Strings {
        public Strings() {
            var str = new string('*', 10);// создаст строку **********

            str = "Zipp Storm";
            char[] ch = str.ToCharArray();// создаст массив символов из строки
            string newstr = new(ch);// создаст строку из массива символов

            string empty = "";
            Debug.Log(empty == "");// true
            Debug.Log(empty == string.Empty);// true
            Debug.Log(empty.Length == 0);// true

            string nullstr = null;
            Debug.Log(nullstr == null);// true
            Debug.Log(nullstr == "");// false
                                     // Debug.Log(nullstr.Length == 0);// ошибка!
            Debug.Log(string.IsNullOrEmpty(nullstr));// true

            string strarr = "Zipp Storm";
            char letter = strarr[1];// i

            string with = "The game UnblockMe3D was created to teach you the basics of working in Unity";

            Debug.Log(with.Contains("game"));// true содержит ли строка слово
            Debug.Log(with.StartsWith("The"));// true начинается ли строка со слова
            Debug.Log(with.EndsWith("Unity"));// true заканчивается ли строка со слова

            Debug.Log(with.IndexOf("g"));// индекс первого вхождения символа или -1 если нет
            Debug.Log(with.LastIndexOf("i"));// индекс первого вхождения символа, но с конца, или -1 если нет

            Debug.Log(with.IndexOfAny(new char[] { 'a', 'q' }));// возвратит индекс первого вхождения любого из символов указанных в массива
            Debug.Log(with.IndexOfAny("1234567890".ToCharArray()));

            Debug.Log(with.LastIndexOfAny(new char[] { 'a', 'q' }));// возвратит индекс первого вхождения любого из символов указанных в массива но с конца
            Debug.Log(with.LastIndexOfAny("1234567890".ToCharArray()));

            Debug.Log(with.Substring(0, 10));// вырежит первые 10 символов строки "The game U"
            Debug.Log(with.Substring(10));// вырежит все символы начиная с 10-го символа

            // вставка и удаление символов в строке 
            string hello = "helloworld!";
            Debug.Log(hello.Insert(5, ", "));// hello, world
            hello = "hello,888 world!";
            Debug.Log(hello.Remove(6, 3));// hello, world

            // заполнение строки символами справа или с лева
            string padstr = "1234";
            Debug.Log(padstr.PadLeft(16, '*'));//   "************1234"
            Debug.Log(padstr.PadLeft(16));//        "            1234"
            Debug.Log(padstr.PadRight(16, '*'));//  "1234************"
            Debug.Log(padstr.PadRight(16));//       "1234            "

            // если строка будет длиннее чем указанное число символов в методах PadLeft и PadRight то строка будет без изменений

            string trimstr = "   abcdef \n\t\r ";
            Debug.Log(trimstr.Trim().Length);// 6 удалит все пробельные символы из начала и конца строки
            trimstr = "aaaazippaaaa";
            Debug.Log(trimstr.Trim('a'));// удаляет указанный символ из начала и конца строки
            trimstr = "1234zipp5678";
            Debug.Log(trimstr.Trim("12345678".ToCharArray()));// удалит любые указанные символы в массиве из начала и конца строки

            // мои варианты))
            trimstr = "aaaazaaaaiaaaapaaaapaaaa";
            Debug.Log(trimstr.TrimAll('a'));
            trimstr = "aaaazbbbbiccccpddddpaaaa";
            Debug.Log(trimstr.TrimAll(new char[] { 'a', 'b', 'c', 'd' }));

            trimstr = "   abcdef \n\t\r ";
            Debug.Log(trimstr.TrimStart());
            Debug.Log(trimstr.TrimStart('a'));

            trimstr = "   abcdef \n\t\r ";
            Debug.Log(trimstr.TrimEnd());
            Debug.Log(trimstr.TrimEnd('a'));

            string repstr = "a,b,c,d,e";
            Debug.Log(repstr.Replace(",", " "));

            string upstr = "test";
            Debug.Log(upstr.ToUpper());
            Debug.Log(upstr.ToUpperInvariant());

            string lostr = "TEST";
            Debug.Log(lostr.ToLower());
            Debug.Log(lostr.ToLowerInvariant());

            string[] words = with.Split();
            foreach (string word in words)
                Debug.Log(word);

            string str2 = "a|b c/d";
            words = str2.Split('|');
            foreach (string word in words)
                Debug.Log(word);

            words = str2.Split(new char[] { '|', '/', ' ' });
            foreach (string word in words)
                Debug.Log("word: " + word);

            var tarr = new string[] { "one", "two", "three", "four", "five" };
            string text = string.Join(' ', tarr);
            Debug.Log(text);

            string sentence = string.Concat("The ", "Doctor ", " team");
            Debug.Log(sentence);

            string composite = "It's {0} degress in {1} on this {2} morning";
            Debug.Log(string.Format(composite, 35, "Perth", DateTime.Now.DayOfWeek));// форматирование строки, экивалентно тому что ниже

            composite = $"It's {35} degress in {"Perth"} on this {DateTime.Now.DayOfWeek} morning";
            Debug.Log(composite);

            /* 
                для первого варианта:
                    - каждое число в {} называют форматным элементом
                    - число соответствует позиции аргумента, и за ним может следовать
                        * запятая и минимальная ширина (удобная для выравнивания колонок), если значение отрицательно, то выравнивание влево, иначе вправо
                        * двоеточие и форматная строка
            */

            composite = "Name = {0,-20} Credit Limit = {1,15:C}\n";

            var jobs = new Dictionary<string, int> {
                    { "Zipp", 25700 },
                    { "Pipp", 22400 },
                    { "Sunny", 15760 },
                    { "Izzy", 15200 },
                    { "Hitch", 12000 }
                };

            string msg = "";
            foreach (var j in jobs)
                msg += string.Format(composite, j.Key, j.Value);
            Debug.Log(msg);

            msg = "";
            foreach (var j in jobs)
                msg += $"Name = {j.Key,-20} Credit Limit = {j.Value,15:C}\n";
            Debug.Log(msg);

            msg = "";
            foreach (var j in jobs)
                msg += $"Name = {j.Key.PadRight(20)} Credit Limit = {j.Value.ToString("C").PadLeft(15)}\n";
            Debug.Log(msg);

            // буква "C" обозначает форматные строку в виде денежной еденицы

            /*
                сравнение строк:
                    ординальное по таблице Unicode и культурное по значимости букв
                ординальное сравноение проблемное если мы хотим например такие слова сортировать
                    Pipp, Zipp, pipp, zipp должно быть так:
                        Pipp, pipp, Zipp, zipp
                    но будет так:
                        Pipp, Zipp, pipp, zipp потому что в Unicode сначала идут все большие а потом все малые буквы,
                            а ординальное сравнение сравнивает их коды
                ы культурном сравнении бувы идут так aAbBcCdD
            */

            // операция == всегда выполняет ординальное сравнение чусвительное к регистру
            // метод Equals без параметров делает тоже самое

            string sa = "Zipp", sb = "zipp";
            Debug.Log(sa == sb);// false
            Debug.Log(sa.Equals(sb));// false


            Debug.Log(sa.Equals(sb, StringComparison.InvariantCulture));// сравнение с учетом культуры
            Debug.Log(sa.Equals(sb, StringComparison.Ordinal));// по умолчанию
            Debug.Log(sa.Equals(sb, StringComparison.CurrentCulture));// текущий алфавит
                                                                      // такие же но без учета регистра
            Debug.Log(sa.Equals(sb, StringComparison.InvariantCultureIgnoreCase));
            Debug.Log(sa.Equals(sb, StringComparison.OrdinalIgnoreCase));// по умолчанию
            Debug.Log(sa.Equals(sb, StringComparison.CurrentCultureIgnoreCase));// текущий алфавит

            // сравнение порядка строк, CompareTo работает с учетом культуры
            Debug.Log("B".CompareTo("a"));// 1 т.к. B находится правей а
            Debug.Log("B".CompareTo("A"));// 1 т.к. B находится правей A
            Debug.Log("a".CompareTo("A"));// -1 т.к. a находится левей A
            Debug.Log("a".CompareTo("B"));// -1 т.к. a находится правей B
            Debug.Log("a".CompareTo("a"));// 0 т.к. они рядом
            Debug.Log("A".CompareTo("A"));// 0 т.к. они рядом
            Debug.Log("zIPP".CompareTo("ZIPP"));// -1

            /*

                puЫic static int Compare (string strA, string strB, StringComparison comparisonType);
                puЫic static int Compare (string strA, string strB, bool ignoreCase, Cultureinfo culture);
                puЫic static int Compare (string strA, string strB, bool ignoreCase);
                puЫic static int CompareOrdinal (string strA, string strB);

            */

            // StringBuilder намного эфективней чем конкатенация строк

            var sb_1 = new StringBuilder();
            for (int i = 0; i < 100; i++)
                sb_1.Append(i + ", ");// i + ", " для небольших генераций не критично но

            for (int i = 100; i < 1000; i++) {
                sb_1.Append(i);
                sb_1.Append(", ");
                // намного эфективней
                }

            Debug.Log(sb_1.ToString());

            // новая линия для строик
            var sb_2 = new StringBuilder();
            sb_2.Append("Zipp Strom");
            sb_2.AppendLine();// дабавит \r\n в строку
            sb_2.Append("Pipp Petals");
            Debug.Log(sb_2.ToString());

            // форматирование строки
            var sb_3 = new StringBuilder();
            string format = "It's {0} degress in {1} on this {2} morning";
            sb_3.AppendFormat(format, 35, "Novograd", DateTime.Now.DayOfWeek);// аналог string.Format
            Debug.Log(sb_3.ToString());

            // очистка строки
            var sb_4 = new StringBuilder();
            sb_4.Append("Zipp Storm");
            Debug.Log(sb_4);
            sb_4.Length = 0;// очистка строки
            Debug.Log(sb_4);
            sb_4.Append("Pipp Petals");
            Debug.Log(sb_4);
            sb_4 = new StringBuilder();// очистка строки
            Debug.Log(sb_4);
            // !!! установки Length не освободит память, а очистит строку))

            // длинна строки
            var sb_5 = new StringBuilder();
            sb_5.Append("Sunny Starscout");
            Debug.Log(sb_5.Length);

            // методы Insert Replace и Remove
            var sb_6 = new StringBuilder();
            sb_6.Append("Sunny Starscout");
            sb_6.Insert(5, "+Zipp");
            Debug.Log(sb_6);// Sunny+Zipp Starscout
            sb_6.Append(" blablabla");
            sb_6.Remove(5, 5);
            Debug.Log(sb_6);// Sunny Starscout blablabla
            sb_6.Remove(15);// мой варинат
            Debug.Log(sb_6.ToString() + " длинна: " + sb_6.Length);
            sb_6.Replace('n', '0');// аналог string.Replace
            Debug.Log(sb_6);
            }
        }
    public static class Utils {
        public static string TrimAll(this string str, char ch) {
            string lstr = "";
            foreach (var c in str) {
                if (c.Equals(ch))
                    continue;
                lstr += c;
                }
            return lstr;
            }
        public static string TrimAll(this string str, char[] arr) {
            string lstr = "";
            string badchar = new(arr);
            foreach (var c in str) {
                if (badchar.Contains(c))
                    continue;
                lstr += c;
                }
            return lstr;
            }
        public static StringBuilder Remove(this StringBuilder sb, int start) => sb.Remove(start, sb.Length - start);
        }
    }