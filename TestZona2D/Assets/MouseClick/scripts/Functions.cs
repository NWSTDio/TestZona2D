using UnityEngine;
using UnityEngine.EventSystems;

namespace MouseClick {
    public static class Functions {

        public static bool IsClickUIObject() { // проверяет есть ли над касанием канвас
            if (Application.platform == RuntimePlatform.Android) { // если на телефоне
                if (Input.touchCount > 0) { // если есть касание
                    Touch touch = Input.GetTouch(0);// касание первого пальца

                    if (touch.phase == TouchPhase.Began) { // фаза касания: начало
                        if (touch.position.IsPointerOverGameObject()) // проверим есть ли обьекты на 
                            return true;
                        }
                    }

                return false;
                }

            if (EventSystem.current.IsPointerOverGameObject()) // если сработало UI событие
                return true;

            return false;
            }

        public static Color GetRandomColor(float alpha) {
            return new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), alpha);
            }

        }
    }