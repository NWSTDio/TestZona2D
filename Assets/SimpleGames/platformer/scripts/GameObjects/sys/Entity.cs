using UnityEngine;

namespace SimpleGames.Platformer {
    public class Entity : MonoBehaviour {

        public virtual void Die() { // убить сущность
            Destroy(gameObject);
            }

        }
    }