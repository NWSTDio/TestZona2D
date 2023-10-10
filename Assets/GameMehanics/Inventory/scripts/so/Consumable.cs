using UnityEngine;

[CreateAssetMenu(fileName = "_consumable", menuName = "Inventory/Items/Create Consumable")]
public class Consumable : Item {

	[Header("Consumable")]
	[SerializeField] private float _healthAdded;

	public float HealthAdded => _healthAdded;

	}