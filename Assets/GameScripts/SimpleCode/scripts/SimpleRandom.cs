using System;
using System.Security.Cryptography;
using UnityEngine;

namespace BookLessons
    {
    internal class SimpleRandom
        {
        public SimpleRandom()
            {
            Debug.Log("!!! работа с Random");

            // в конструкторе принимает число,  указывающее на старт последовательности, т.е. если числа одинаковы

            var r1 = new System.Random(1);
            var r2 = new System.Random(1);
            Debug.Log($"{r1.Next(100)} : {r1.Next(100)}");// каждый раз будет 24, 11
            Debug.Log($"{r2.Next(100)} : {r2.Next(100)}");// каждый раз будет 24, 11
            Debug.Log($"{r2.Next(100)} : {r2.Next(100)}");

            var r3 = new System.Random();// в качестве стартового числа будет текущее время
            Debug.Log($"{r3.Next(100)} : {r3.Next(100)}");// всегда будут отличатся
            Debug.Log($"{r3.Next(100)} : {r3.Next(100)}");// всегда будут отличатся

            // если дварандом создадутся без параметра, за 10 милисекунд, то они получат одно и тоже число и всегда будут выдавать одну и ту же последовательность

            // вызов метода Next(n) выдаст значение между 0 и n -1

            Debug.Log(r3.NextDouble());// число от 0 до 1

            // если приложение строго зависит от рандома, то лучьше пользоватся:
            var rand = RandomNumberGenerator.Create();
            byte[] bytes = new byte[32];
            rand.GetBytes(bytes);
            int i = BitConverter.ToInt32(bytes, 0);// получим число
            }
        }
    }