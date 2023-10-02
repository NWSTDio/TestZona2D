using UnityEngine;

namespace BookLessons
    {
    internal class SimpleFormat
        {
        public SimpleFormat()
            {
            Debug.Log("!!! пример форматирования строк");

            // G или g
            float num = 1.2345f;
            Debug.Log(num.ToString("G"));
            Debug.Log(num.ToString("G3"));
            num = 0.00001f;
            Debug.Log(num.ToString("G"));
            Debug.Log(num.ToString("g"));
            num = 12345;
            Debug.Log(num.ToString("G3"));

            // F
            num = 123.34f;
            Debug.Log(num.ToString("F"));// ничего не сделает
            Debug.Log(num.ToString("F3"));
            num = 12.12345678f;
            Debug.Log(num.ToString("F3"));

            // N(формат с фиксирован. точкой) D(заполнение 0) E e(експонента) C(деньги) P(процент) X x(шестнадцатиричный формат) R(округление)

            // есть еще и Специальные форматные строки дпя чисел
            // Форматные строки для даты/времени, чувствительные к культуре
            }
        }
    }