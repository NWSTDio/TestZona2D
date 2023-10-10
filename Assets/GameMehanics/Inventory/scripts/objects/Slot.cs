public class Slot {

	public Item Item { get; private set; } // предмет который в слоте
	public int Quantity { get; private set; } // количество

	public Slot(Item item, int quantity) {
		Item = item;
		Quantity = quantity;
		}

	public void Add(int count) => Quantity += count;
	public void Remove(int count) => Quantity -= count;

	}