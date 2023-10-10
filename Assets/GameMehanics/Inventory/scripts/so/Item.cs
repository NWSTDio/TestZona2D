using UnityEngine;

public abstract class Item : ScriptableObject {

	[Header("Item")]
	[SerializeField] private Sprite _icon;
	[SerializeField] private string _name;
	[SerializeField] private int _maxQuantity;// количество

	public Sprite Icon => _icon;
	public string Name => _name;
	public int MaxQuantity => _maxQuantity;
	public bool IsStackable => MaxQuantity > 1;

	}