using System.Numerics;
using System.Security.Cryptography;
using UnityEngine;

namespace BookLessons
    {
    internal class SimpleBigInteger
        {
        public SimpleBigInteger()
            {
            Debug.Log("!!! работа с BigInteger");

            BigInteger big = 25;

            BigInteger googol = BigInteger.Pow(10, 100);// число гугол, т.е. 10 в 100 степени
            Debug.Log(googol.ToString());

            // можно и так
            BigInteger googol_2 = BigInteger.Parse("1".PadRight(100, '0'));
            Debug.Log(googol_2.ToString());

            // если явно приводить кдругим более меньшим типам, то будет потеря точности
            double d_1 = (double) googol;
            BigInteger big_2 = (BigInteger)d_1;
            Debug.Log(big_2.ToString());

            RandomNumberGenerator rand = RandomNumberGenerator.Create();
            byte[] bytes = new byte[32];
            rand.GetBytes(bytes);
            var bigRandomNumЬer = new BigInteger(bytes);
            byte[] bytes_2 = bigRandomNumЬer.ToByteArray();

            googol += 100;// можно к ним арифметику применять
            }
        }
    }