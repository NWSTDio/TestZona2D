using UnityEngine;

namespace ExampleWorkEditor.TestAnimations {
    [RequireComponent(typeof(Animator))]
    public class TestAnimationPlay : MonoBehaviour {

        private Animator _animator;

        private void Awake() {
            _animator = GetComponent<Animator>();
            }

        private void Update() {
            if (Input.GetKeyUp(KeyCode.Space))
                _animator.Play("rotate");

            if (Input.GetKeyUp(KeyCode.UpArrow))
                _animator.SetBool("rotate2", _animator.GetBool("rotate2") == false);
            }

        }
    }