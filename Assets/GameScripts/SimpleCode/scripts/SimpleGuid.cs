using System;
using UnityEngine;

namespace BookLessons
    {
    internal class SimpleGuid
        {
        public SimpleGuid()
            {
            Debug.Log("!!! пример работы со структурой Guid");
            // Guid это уникальный 16-ти байтовый ключ

            Guid g = Guid.NewGuid();
            Debug.Log(g.ToString());
            }
        }
    }