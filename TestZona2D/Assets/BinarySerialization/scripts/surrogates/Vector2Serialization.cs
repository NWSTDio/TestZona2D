using System.Runtime.Serialization;
using UnityEngine;

namespace BinarySerialization {
    public class Vector2Serialization : ISerializationSurrogate {

        private readonly static string X = "X", Y = "Y";

        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context) {
            var vector = (Vector2)obj;

            info.AddValue(X, vector.x);
            info.AddValue(Y, vector.y);
            }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector) {
            var vector = (Vector2)obj;

            vector.x = (float)info.GetValue(X, typeof(float));
            vector.y = (float)info.GetValue(Y, typeof(float));

            obj = vector;

            return obj;
            }

        }
    }