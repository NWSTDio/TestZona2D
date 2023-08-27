using UnityEngine;

namespace SimplePlatformer {
    public class Entity : MonoBehaviour {

        public virtual void Die() { // убить сущность
            Destroy(gameObject);
            }

        }
    }