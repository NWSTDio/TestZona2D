using UnityEngine;

namespace ExampleWorkEditor.TestAnimations {
    [RequireComponent(typeof(Animator))]
    public class TriggerTest : MonoBehaviour {

        private Animator _animator;

        private void Awake() {
            _animator = GetComponent<Animator>();
            }

        private void Update() {
            if (Input.GetKeyUp(KeyCode.Q))
                _animator.SetTrigger("TestTriggerZipp");
            }

        }
    }