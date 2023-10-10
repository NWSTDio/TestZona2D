using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(Image))]
public class SlotUI : MonoBehaviour {

	public Slot Slot { get; private set; }

	[SerializeField] private Image _icon;
	[SerializeField] private TextMeshProUGUI _quantityText;

	private Inventory _inventory;
	private Image _background;
	private Color _baseColor, _movingColor, _deletedColor;

	public int Remainder => Slot.Item.MaxQuantity - Slot.Quantity;
	public bool IsFill => Slot.Quantity >= Slot.Item.MaxQuantity;
	public bool IsEmpty => Slot == null;
	public bool IsEmptyItems => Slot.Quantity <= 0;
	public bool IsStackable => Slot.Item.IsStackable;

	#region MonoBehaviour
	private void OnEnable() => Inventory.OnChangeDeleteMode += UpdateVisual;
	private void Awake() {
		GetComponent<Button>().onClick.AddListener(() => {
			if (_inventory.IsMovingModeItem) { // если включен режим перемещения предметов
				_inventory.MoveItem(this);// переместить в текущий слот

				return;
				}

			if (IsEmpty) // если слот пуст
				return;

			if (_inventory.IsDeleteModeItem) { // если включен режим удаления предметов
				RemoveQuantityItems(1);// удалим предмет

				return;
				}

			_inventory.AddMoveItem(this);// выбрать предмет для перемещения
		});

		_background = GetComponent<Image>();

		_baseColor = _background.color;
		_movingColor = new Color(.58f, .25f, .25f, .4f);
		_deletedColor = new Color(.7f, 0, 0, .4f);
		}
	private void Start() {
		_inventory = Inventory.Instance;

		UpdateVisual();
		}
	private void OnDisable() => Inventory.OnChangeDeleteMode -= UpdateVisual;
	#endregion

	public void Init(Slot slot) {
		Slot = slot;

		UpdateVisual();
		}
	public void AddQuantityItems(int quantity) { // добавить определенное количество предметов
		if (quantity > Remainder) // если предметов больше чем свободного места в слоте
			return;

		Slot.Add(quantity);

		UpdateVisual();
		}
	public void RemoveQuantityItems(int quantity) { // удалить определенное количество предметов
		Slot.Remove(quantity);

		if (IsEmptyItems) {// если предметов не осталось
			Clear();

			return;
			}

		UpdateVisual();
		}
	public bool ContainsItem(Item item) { // содержит ли слот указанный предмет
		if (IsEmpty)
			return false;

		return Slot.Item == item;
		}
	public void Clear() {
		Slot = null;

		UpdateVisual();
		}
	public void ShowMovingVisualMode(bool moving = true) => _background.color = moving ? _movingColor : _baseColor;

	private void UpdateVisual() {
		if (IsEmpty) { // если слот пуст
			_icon.enabled = false;
			_icon.sprite = null;
			_quantityText.text = "";
			_background.color = _baseColor;

			return;
			}

		_icon.enabled = true;
		_icon.sprite = Slot.Item.Icon;
		_quantityText.text = IsStackable ? Slot.Quantity + "" : "";
		_background.color = _inventory.IsDeleteModeItem ? _deletedColor : _baseColor;
		}

	}