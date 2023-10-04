using UnityEngine;

namespace ExampleWorkEditor.TestAnimations.version_1 {
    [RequireComponent(typeof(Animator))]
    public class TestAnimation : MonoBehaviour {

        private Animator _animator;

        private bool _test = false;

        private void Awake() {
            _animator = GetComponent<Animator>();
            }

        private void Update() {
            if (Input.GetKeyUp(KeyCode.Space)) {
                _test = _test == false;

                _animator.SetBool("test", _test);
                }
            }

        }
    }