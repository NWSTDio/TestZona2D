using UnityEngine;

namespace SimpleCode.SimpleAnonumousThype {
    public class AnonimousThype {

        public AnonimousThype() {
            Debug.Log("!!! пример анонимных типов!");

            string Sex = "Male";

            var dude = new { Name = "Bob", Age = 23, Sex, L = Sex.Length };// анонимный тип

            var a = new { Test = "test" };
            var b = new { Test = "test" };

            Debug.Log(a.GetType() == b.GetType());// true

            var c = new { Test = "test", X = 0 };

            Debug.Log(a.GetType() == c.GetType());// false

            // анонимные поля обьявленные одинаково, будут иметь одинаковый тип на протяжении компиляции

            Debug.Log(a == b);// false
            Debug.Log(a.Equals(b));// true

            // можно создавать массив анонимных типов

            var dudes = new[] {
                new { Test = "test_1" },
                new { Test = "test_2" },
                new { Test = "test_3" },
                new { Test = "test_4" }
                };

            foreach (var d in dudes)
                Debug.Log(d.Test);

            // анонимные типы нужны в LINQ
            }

        }
    }