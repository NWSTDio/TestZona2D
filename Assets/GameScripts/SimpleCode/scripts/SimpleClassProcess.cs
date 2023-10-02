using System;
using System.Diagnostics;
using UnityEngine;

namespace BookLessons
    {
    internal class SimpleClassProcess
        {
        public SimpleClassProcess()
            {
            UnityEngine.Debug.Log("!!! Пример работы класса Process");

            // Process.Start("notepad.exe");// запускаем программу
            // Process.Start(Environment.CurrentDirectory + "\\file.txt");// запускаем файл в установленной программе
            }
        }
    }