using TMPro;
using UnityEngine;
using Zenject;

namespace ExampleAssets.ZenjectAsset {
    public class HpVisual : MonoBehaviour {

        private Player _player;
        private TextMeshProUGUI _hpText;

        [Inject]
        private void Construct(Player player) {
            _player = player;
            }

        #region Monobehaviour
        private void Awake() {
            _hpText = GetComponent<TextMeshProUGUI>();
            }
        private void Start() {
            UpdateVisual();
            }
        private void OnEnable() {
            _player.OnDamage += UpdateVisual;
            }
        private void OnDisable() {
            _player.OnDamage -= UpdateVisual;
            }
        #endregion

        private void UpdateVisual() {
            _hpText.text = _player.CurrentHp.ToString();
            }

        }
    }