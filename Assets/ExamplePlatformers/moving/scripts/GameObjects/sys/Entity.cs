using UnityEngine;

namespace ExamplePlatformer.moving {
    public class Entity : MonoBehaviour {

        public virtual void Die() { // убить сущность
            Destroy(gameObject);
            }

        }
    }