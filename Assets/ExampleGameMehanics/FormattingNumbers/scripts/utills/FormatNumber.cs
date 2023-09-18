using System;
using System.Collections.Generic;

namespace ExampleGameMehanics.FormattingNumbers {
    public static class FormatNumber {

        private static readonly int charA = Convert.ToInt32('a');
        private static readonly List<string> units = new() { "", "K", "M", "B", "T" };

        public static string Format(double value) {
            if (value < 1d)
                return "0";

            int n = (int)Math.Log(value, 1000);
            double m = value / Math.Pow(1000, n);
            string unit;

            if (n < units.Count)
                unit = units[n];
            else {
                var unitInt = n - units.Count;
                var secondUnit = unitInt % 26;
                var firstUnit = unitInt / 26;

                unit = Convert.ToChar(firstUnit + charA).ToString() + Convert.ToChar(secondUnit + charA).ToString();
                }

            return (Math.Floor(m * 100) / 100).ToString("0.##") + unit;
            }

        }
    }