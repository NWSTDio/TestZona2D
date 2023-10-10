using UnityEngine;

[CreateAssetMenu(fileName = "_tool", menuName = "Inventory/Items/Create Tool")]
public class Tool : Item {

	public enum Type { WAEPON, PICKAXE, HAMMER, AXE }

	[Header("Tool")]
	[SerializeField] private Type _toolType;

	public Type ToolType => _toolType;

	}