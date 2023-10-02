using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace GameScripts.RegularExpression {
    public class Controller : MonoBehaviour {

        private void Start() {
            string data_port = "[<Порт Рояль;325450><Толедо;1500><Вирджиния;777><Роатан;888><Белиз;5687><Нью-Провиденс;435435><Тортуга;343545>]";

            MatchCollection countryList = Regex.Matches(data_port, @"\[([^\[\]]*)\]");

            foreach (Match country in countryList.Cast<Match>()) {
                MatchCollection portList = Regex.Matches(country.Value, @"<([^<>]*)>");

                foreach (Match port in portList.Cast<Match>()) {
                    string result = Regex.Replace(port.Value, @"<([^<>]*)>", "$1");

                    string[] data = result.Split(';');

                    for (int i = 0; i < data.Length; i++)
                        Debug.Log(data[i]);
                    }
                }
            }

        }
    }