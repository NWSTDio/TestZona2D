using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DeleteButtonUI : MonoBehaviour {

	private Inventory _inventory;

	private void Awake() {
		GetComponent<Button>().onClick.AddListener(() => {
			if (_inventory.IsMovingModeItem)
				return;

			_inventory.ChangeDeleteMode();
		});
		}

	private void Start() => _inventory = Inventory.Instance;

	}