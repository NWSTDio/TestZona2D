using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleCode.SimpleReloadOperations {
    public class ReloadOperations {

        public ReloadOperations() {
            Debug.Log("!!! перегрузка операций");

            /* 
                допускающие перегрузку операторы:
                + - (унарные)
                ! ~ & | ^ << >>
                + - * / ++ -- %
                == != > < >= <=
                явные и не явные преобразования (с ключевыми словами explicit и implicit)
                операции true и false (не литералы)
                косвенно перегружаются += -= /= *= %= && ||
            */

            var b = new Note(222);
            var sc = b + 100;
            Debug.Log(b.Value);
            Debug.Log(sc.Value);
            sc += 2;
            Debug.Log(sc.Value);
            // sc -= 2;// не определено
            b += 1000;
            Debug.Log(b.Value);
            sc += b.Value;
            Debug.Log(sc.Value);

            var vector = new Vector(100, 200);
            vector += new Vector(200, 500);
            Debug.Log($"{vector}");
            // vector -= new Vector(2, 4);// ошибка

            // перегрузка операций сравнения и эквивалентности должны соблюдать правила:
            // == и != обязаны обе быть перегружены + доп. желательно переопределять Equals и GetHashCode
            // <> и <= и >= также должны быть перегружены парно + должны быть реализованы интерфейсы IComparable и IComparable<T>
            Note n = (Note)555.4f;// вызывается explicit operator Note(int x)
            int x = n;// вызывается implicit operator int (Note x)

            // перегрузка операций true и false очень редкая и нужна для тех тиков что схожи с булевскими но не имеют преобразования в bool
            Debug.Log("test".IsCapitalized());// мой метод расширения
            Debug.Log("Seatle".First());// S
            string test = "Zipp Storm";
            Debug.Log(test.First());

            Debug.Log("a".Test("b"));// можно расширять метод поля с параметрами
            }
        public struct Note {
            public int Value;
            public Note(int value) {
                Value = value;
                }
            public static Note operator +(Note x, int value) => new(x.Value + value);
            // rule 1 должно быть ключевое слово operator, за которым следует символ операции
            // rule 2 такая функция должна быть посечена public и static

            // перегрузка явных и не явных преобразований
            public static implicit operator int(Note x) => (int)(440 * Mathf.Pow(2, x.Value / 12));
            public static explicit operator Note(int x) => new((int)(.5f + 12 * (Mathf.Log(x / 440) / Mathf.Log(2))));
            }
        }

    public class Vector {
        public int X, Y;

        public Vector(int x, int y) {
            X = x;
            Y = y;
            }

        public static Vector operator +(Vector a, Vector b) => new(a.X + b.X, a.Y + b.Y);

        public override string ToString() => $"X = {X}, Y = {Y}";
        }

    public static class StringHelper { // методы расширения полей

        public static bool IsCapitalized(this string s) { // метод расширения для строковых полей, должен быть определен в статическом классе
            if (string.IsNullOrEmpty(s))
                return false;

            return char.IsUpper(s[0]);
            }

        public static string Test(this string s, string b) => s + b;

        public static T First<T>(this IEnumerable<T> sequence) { // работает с любым полем реализующим интерфейс IEnumerable
            foreach (T element in sequence)
                return element;

            throw new InvalidOperationException("No Elements!");
            }

        }
    }