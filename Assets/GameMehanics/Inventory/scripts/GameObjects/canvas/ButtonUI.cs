using UnityEngine;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour {

	[SerializeField] private Item _item;

	private Inventory _inventory;

	private void Awake() {
		GetComponent<Button>().onClick.AddListener(() => {
			if (_inventory.IsDeleteModeItem)
				return;

			_inventory.AddQuantityItems(_item, 1);
		});
		}

	private void Start() => _inventory = Inventory.Instance;

	}