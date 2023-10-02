using System.Runtime.Serialization;
using UnityEngine;

namespace GameScripts.BinarySerialization {
    public class RectSerialization : ISerializationSurrogate {

        private readonly static string X = "X", Y = "Y";
        private readonly static string WIDTH = "W", HEIGHT = "H";

        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context) {
            var rect = (Rect)obj;

            info.AddValue(X, rect.x);
            info.AddValue(Y, rect.y);
            info.AddValue(WIDTH, rect.width);
            info.AddValue(HEIGHT, rect.height);
            }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector) {
            var rect = (Rect)obj;

            rect.x = (float)info.GetValue(X, typeof(float));
            rect.y = (float)info.GetValue(Y, typeof(float));
            rect.width = (float)info.GetValue(WIDTH, typeof(float));
            rect.height = (float)info.GetValue(HEIGHT, typeof(float));

            obj = rect;

            return obj;
            }

        }
    }