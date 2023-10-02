using UnityEngine;

namespace SimpleCode.SimplePointers {
    public class Pointers {
        public Pointers() {
            Debug.Log("!!! пример работы указателей");

            // rule 1 указатели работают с памятью
            // rule 2 работа с указателями помечается оператором unsafe
            // rule 3 экземпляр указателя хранит в себе адресс переменной
            // небезопасный код должен быть разрешен и Visual Studio и Unity

            var t = new Test() { X = 100 };
            Debug.Log(t.X);// 100

            //unsafe
            //    {
            //    fixed (int* p = &t.X)
            //        *p = 9;
            //    Debug.Log(t.X);// 9
            //    }

            /*
                
                операции работы над указателями
                    &   - операция взятия адреса, возвращает указатель на адресс переменной
                    *   - операция разименования, возвращает переменную, находящуюся по адресу, который задан указателем
                    ->  - операция указателя на член, является синтатическим сокразением, т.е. x -> y эквивалентно (*x).y
             
             */
            }
        //unsafe void BlueFilter(int[,] bitmap)
        //    {
        //    int length = bitmap.Length;
        //    fixed (int* b = bitmap)
        //        {
        //        int* p = b;
        //        for (int i = 0; i < length; i++)
        //            *p++ &= 0xFF;
        //        }
        //    }
        }
    public class Test {

        public int X = 100;

        }
    }