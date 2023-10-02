using System.Numerics;
using UnityEngine;

namespace BookLessons
    {
    internal class SimpleComplex
        {
        public SimpleComplex()
            {
            Debug.Log("!!! пример работы структуры Complex");

            var c1 = new Complex(2, 3.5);
            var c2 = new Complex(3, 0);

            Debug.Log(c1.Real);// вещественное
            Debug.Log(c1.Imaginary);// мнимое
            Debug.Log(c1.Phase);// фаза
            Debug.Log(c1.Magnitude);// амплитуда

            Complex c3 = Complex.FromPolarCoordinates(1.3, 5);

            // работают стандартные арифметические операции
            c3 += c2;
            }
        }
    }