using System;
using UnityEngine;

namespace BookLessons
    {
    internal class SimpleDateTime
        {
        public SimpleDateTime()
            {
            Debug.Log("!!! Пример работы даты и времени");
            /*
                для работы с датой и временем, существует три структуры
                    - DateTime
                    - DateTimeOffset
                    - TimeSpan
            */

            // TimeSpan (временной интервал, время суток), который получают при помощи: конструктора, вызовом статического метода From... или вычитом из лдного DateTime другого

            var span_1 = new TimeSpan(14, 25, 44);
            Debug.Log($"дней: {span_1.Days} часов: {span_1.Hours} минут: {span_1.Minutes} секунд: {span_1.Seconds} миллисекунд: {span_1.Milliseconds}");
            Debug.Log($"дней: {span_1.TotalDays} часов: {span_1.TotalHours} минут: {span_1.TotalMinutes} секунд: {span_1.TotalSeconds} миллисекунд: {span_1.TotalMilliseconds}");

            span_1 = new TimeSpan(767, 567, 345);
            // вывод ниже укажет сколько дней часов минут и пр. начиная с полуночи в пересчете от указанных значений в конструкторе не учитывая летнее время
            Debug.Log($"дней: {span_1.Days} часов: {span_1.Hours} минут: {span_1.Minutes} секунд: {span_1.Seconds} миллисекунд: {span_1.Milliseconds}");

            // статические методы генерируют таймспан из указанных частей времени
            Debug.Log(TimeSpan.FromDays(4567).ToString());
            Debug.Log(TimeSpan.FromHours(4567).ToString());
            Debug.Log(TimeSpan.FromMinutes(4567).ToString());
            Debug.Log(TimeSpan.FromSeconds(4567).ToString());
            Debug.Log(TimeSpan.FromMilliseconds(4567).ToString());

            // для TimeSpan перегружены операторы + - < и >
            TimeSpan span_2 = TimeSpan.FromDays(10) - TimeSpan.FromSeconds(1);// от 10 дней отнимаем секунду
            Debug.Log(span_2.ToString());// 9 дней 23 часа 59 минут 59 секунд

            // DateTime
            var dt = new DateTime(2005, 4, 21);// 21.04.2005 0:00:00
            Debug.Log(dt.ToString());
            dt = new DateTime(2006, 7, 12, 6, 34, 45);// 12.07.2006 6:34:45
            Debug.Log(dt.ToString());

            // DateTimeOffset
            var dto = new DateTimeOffset(2005, 4, 21, 22, 34, 45, new TimeSpan(6, 24, 0));
            Debug.Log(dto.ToString());
            // new TimeSpan(6, 24, 0) должен принимать целые минуты))

            // текущее значение DateTime/DateTimeOffset
            Debug.Log(DateTime.Now);
            Debug.Log(DateTimeOffset.Now);

            Debug.Log(DateTime.Today);

            Debug.Log(DateTime.UtcNow);
            Debug.Log(DateTimeOffset.UtcNow);

            // DateTime предоставляет возможность выводить составляющие даты и времени по частям
            Debug.Log($"{dt.Year} год. {dt.Month} месяц {dt.Day} день");
            Debug.Log($"{dt.DayOfWeek} день недели {dt.DayOfYear} день года");
            Debug.Log($"{dt.Hour} час. {dt.Minute} минут {dt.Second} секунд {dt.Millisecond} миллис.");
            Debug.Log($"{dt.Ticks} тиков {dt.TimeOfDay} времени в таймпспане");

            var dt_1 = new DateTime(2006, 7, 12, 6, 34, 45);
            dt_1.Add(TimeSpan.FromDays(10));// добавить 10 дней
            dt_1.AddMinutes(100);// добавить минут
            // обеими способами можно добавить лет, дней, месяцев, минут, часов и пр.

            // также можно и +
            dt_1 += TimeSpan.FromDays(10);

            // или -
            dt_1 -= TimeSpan.FromHours(100);
            }
        }
    }