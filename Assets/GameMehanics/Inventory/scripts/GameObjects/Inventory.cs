using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public static Inventory Instance { get; private set; }
	public static Action OnChangeDeleteMode;// событие включения и выключения режима удаления

	[SerializeField] private SlotUI _slotPrefab;// UI слот
	[SerializeField] private Transform _slotsContainer;// контейнер слотов

	private readonly List<SlotUI> _slots = new();// список всех UI слотов
	private readonly int _capacity = 20;// емкость инвентаря
	private SlotUI _movingSlot;// перемещаемый слот
	private bool _deleteModeItems;// режим удаления предметов

	public bool IsMovingModeItem => _movingSlot != null;// включен ли режим перемещения
	public bool IsDeleteModeItem => _deleteModeItems;// состояние режима удаления предметов

	#region MonoBehaviour
	private void Awake() => Instance = this;
	private void Start() {
		for (int i = 0; i < _capacity; i++) // заполним инвентарь пустыми слотами
			_slots.Add(Instantiate(_slotPrefab, _slotsContainer));
		}
	#endregion

	public void AddQuantityItems(Item item, int quantity) { // добавить количество предметов
		while (quantity > 0) { // пока есть предметы для добавления
			if (TryGetSlot(item, out SlotUI slot)) { // если есть подходящий слот
				if (slot.IsEmpty) { // если слот пуст
					int count = quantity > item.MaxQuantity ? item.MaxQuantity : quantity;// предметы для перемещения

					slot.Init(new Slot(item, count));// поместим предметы

					quantity -= count;
					}
				else {
					int count = quantity > slot.Remainder ? slot.Remainder : quantity;// предметы для перемещения

					slot.AddQuantityItems(count);// поместим предметы

					quantity -= count;
					}
				}
			else {
				Debug.Log("Инвентарь переполнен!");

				break;
				}
			}
		}
	public void MoveItem(SlotUI slot) { // перемещение предметов в ячейку
		if (IsMovingModeItem == false) // если еще не включен режим перемещения
			return;

		if (slot.IsEmpty) { // если слот в который пернемещаем пуст
			slot.Init(_movingSlot.Slot);// перемещаем в новый слот

			_movingSlot.Clear();// очищаем перемещаемый слот
			}
		else { // если слот не пуст
			if (slot.IsStackable && slot.IsFill == false && slot.ContainsItem(_movingSlot.Slot.Item)) { // если предмет стакабельный и есть место для новых предметов и предметы совпали
				int count = _movingSlot.Slot.Quantity <= slot.Remainder ? _movingSlot.Slot.Quantity : slot.Remainder;// колл-во перемещаемых предметов

				_movingSlot.RemoveQuantityItems(count);// заберем предметы
				slot.AddQuantityItems(count);// поместим предметы

				if (_movingSlot.IsEmptyItems) // если предметов больше не соталось
					_movingSlot.Clear();// очищаем перемещаемый слот
				}
			else { // иначе просто поменяем местами слоты
				Slot tmp = _movingSlot.Slot;
				_movingSlot.Init(slot.Slot);
				slot.Init(tmp);
				}
			}

		_movingSlot.ShowMovingVisualMode(false);// деактивираем визульно выбираемый слот
		_movingSlot = null;// сбросим перемещаемый слот

		CursorUI.Instance.Hide();// спрячем курсор
		}
	public void AddMoveItem(SlotUI slot) { // добавить предмет для перемещения
		if (IsMovingModeItem) // если уже включен режим перемещения
			return;

		_movingSlot = slot;// запомним предмет
		_movingSlot.ShowMovingVisualMode();// покажем визуально выбираемый слот

		CursorUI.Instance.Show(_movingSlot.Slot.Item.Icon);// отобразим куроср с перемещаемым предметом
		}
	public void ChangeDeleteMode() {
		_deleteModeItems = _deleteModeItems == false;

		OnChangeDeleteMode?.Invoke();
		}

	private bool TryGetSlot(Item item, out SlotUI slotUI) { // получить свободный слот
		SlotUI tmp = null;// первый попавшийся пустой слот

		foreach (var slot in _slots) { // проходим по всем слотам
			if (slot.IsEmpty) { // если слот пуст
				if (tmp == null)  // если не сохранен пустой слот
					tmp = slot;// запомним слот

				if (item.IsStackable == false)  // если предмет не стакабельный
					break;

				continue;
				}

			if (slot.IsStackable == false || slot.IsFill) // если предмет не стакабельный или слот не имеет свободного места
				continue;

			if (slot.ContainsItem(item)) { // если предметы одинаковы
				tmp = slot;

				break;
				}
			}

		slotUI = tmp;

		if (slotUI == null) // если не нашли подходящий слот
			return false;

		return true;
		}

	}