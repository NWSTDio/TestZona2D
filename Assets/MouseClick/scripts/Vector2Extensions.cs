using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace MouseClick {
    public static class Vector2Extensions { // расширения для структуры Vector2

        public static bool IsPointerOverGameObject(this Vector2 pos) { // проверка есть ли под кликом UI
            if (EventSystem.current.IsPointerOverGameObject()) // если сработало UI событие
                return false;

            var eventPosition = new PointerEventData(EventSystem.current) {
                position = new Vector2(pos.x, pos.y)
                };

            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventPosition, results);

            return results.Count > 0;
            }

        }
    }