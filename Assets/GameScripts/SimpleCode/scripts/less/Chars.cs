using System.Globalization;
using UnityEngine;

namespace SimpleCode.SimpleChars {
    public class Chars {

        public Chars() {
            char c = 'a';

            Debug.Log(char.ToUpper(c));// верхний регистр
            Debug.Log(char.ToUpperInvariant('i'));// для всех локалей будет правильным, ниже более сложный варинат
            Debug.Log(char.ToUpper('i', CultureInfo.InvariantCulture));// но тут нужен класс CultureInfo
            Debug.Log(char.ToLower('T'));// t

            Debug.Log(char.IsLetter('A'));// true
            Debug.Log(char.IsLetter('0'));// false

            Debug.Log(char.IsUpper('A'));// true
            Debug.Log(char.IsUpper('a'));// false

            Debug.Log(char.IsLower('A'));// false
            Debug.Log(char.IsLower('a'));// true

            Debug.Log(char.IsDigit('1'));// true
            Debug.Log(char.IsDigit('a'));// false

            Debug.Log(char.IsLetterOrDigit('1'));// true
            Debug.Log(char.IsLetterOrDigit('a'));// true
            Debug.Log(char.IsLetterOrDigit('-'));// false

            Debug.Log(char.IsNumber('1'));// true

            Debug.Log(char.IsSeparator(' '));// true
            Debug.Log(char.IsSeparator('a'));// false

            Debug.Log(char.IsWhiteSpace('\n'));// true

            Debug.Log(char.IsPunctuation(','));// true

            Debug.Log(char.IsSymbol('$'));// true

            Debug.Log(char.IsControl('\t'));// true
            }
        }
    }