using UnityEngine;

namespace ExampleWorkEditor.TestAnimations {
    [RequireComponent(typeof(Animator))]
    public class AnimationTest : MonoBehaviour {

        private Animator _animator;

        private void Awake() {
            _animator = GetComponent<Animator>();
            }

        private void Update() {
            if (Input.GetKeyUp(KeyCode.Space))
                _animator.SetInteger("state", 1);
            }

        }
    }