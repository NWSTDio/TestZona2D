using System;
using UnityEngine;

namespace SimpleCode.SimpleEnumeration {
    public class Enumeration {

        // можно указывать откуда начинать инициализацию
        public enum Day { MONDAY = 4, TUESDAY, WEDNESDAY = 100, THURSDAY, FRIDAY, SATURDAY, SUNDAY } // MONDAY = 4, TUESDAY = 5, WEDNESDAY = 100 THURSDAY = 101
        public enum Bytes : byte { A, B, C, D, E, F }
        public enum HORIZONTAL { LEFT = -1, CENTER, RIGHT }
        public enum VERTICAL { LEFT = HORIZONTAL.LEFT, CENTER, RIGHT = HORIZONTAL.RIGHT }
        [Flags]
        public enum FLAG {// комбинированное перечисление, желательно помечать [Flags] и все значения должны быть обьявлены степенями двойки
            NONE = 0,
            LEFT = 1, RIGHT = 2, TOP = 4, BOTTOM = 8,
            LEFTRIGHT = LEFT | RIGHT,
            TOPBOTTOM = TOP | BOTTOM,
            ALL = LEFTRIGHT | TOPBOTTOM
            }
        public Enumeration() {
            Day days = Day.TUESDAY;
            if (days == Day.FRIDAY)
                Debug.Log("Пятница!");

            // проблемы с перечислениями:
            Day day = (Day)1000;// можно присовить число 1000, но такого в перечислении нету
            Debug.Log((int)day); // 1000

            if (Enum.IsDefined(typeof(Day), day) == false)
                Debug.Log("Ошибка!");

            var test = Bytes.D;
            Debug.Log(test.ToString());// D
            Debug.Log(test);// D
            Debug.Log(test.GetType());// BookLessons.SimpleEnumeration+Test
            Debug.Log(test.GetType().Name);// Test
            Debug.Log(typeof(Bytes));// BookLessons.SimpleEnumeration+Test
            Debug.Log(typeof(Bytes).Name);// Test
            Debug.Log((byte)test);// 3

            Bytes test2 = 0;// так можно, но только для 0
            Debug.Log((int)test2);

            HORIZONTAL horizontal = HORIZONTAL.RIGHT;
            Debug.Log((int)horizontal);// 0

            VERTICAL vertical = (VERTICAL)horizontal;// тоже самое что и (VERTICAL) (int) horizontal;
            Debug.Log((int)vertical);

            // к комбинированным перечислениям применяют побитовые операции
            FLAG flag = FLAG.LEFT | FLAG.RIGHT;
            if ((flag & FLAG.LEFT) != 0)
                Debug.Log("LEFT");

            string formatted = flag.ToString();
            Debug.Log(formatted);

            FLAG left = FLAG.LEFT;
            left |= FLAG.RIGHT;
            Debug.Log(left == flag);// true

            left ^= FLAG.RIGHT;
            Debug.Log(left);// LEFT



            // но для перечислений с флагом такой фокус не пройдет, но можно так
            FLAG flag2 = (FLAG)777;
            Debug.Log(IsFlagDefined(flag2));
            }

        private bool IsFlagDefined(Enum e) => !decimal.TryParse(e.ToString(), out _);
        }
    }