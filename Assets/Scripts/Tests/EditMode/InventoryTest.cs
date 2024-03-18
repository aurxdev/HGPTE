using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class InventoryTest
{

    private Item CreateItemData(int id, string itemName, ItemType type)
    {
        ItemData itemData = ScriptableObject.CreateInstance<ItemData>();
        itemData.id = id;
        itemData.itemName = itemName;
        itemData.type = type;
        Item item = new Item(itemData);
        return item;
    }

    [Test]
    public void TestAddItem()
    {

        Inventory inventory = new Inventory(10);
        Item item = CreateItemData(1, "TestItem", ItemType.MATERIAL);

        inventory.Add(item);

        // Assert
        Assert.AreEqual(1, inventory.NumberOfItems());
    }

    [Test]
    public void TestRemoveItem()
    {

        Inventory inventory = new Inventory(10);
        Item item = CreateItemData(1, "TestItem", ItemType.MATERIAL);
        inventory.Add(item);


        inventory.Remove(item);

        // Assert
        Assert.AreEqual(0, inventory.NumberOfItems());
    }

    [Test]
    public void TestSwapItems()
    {
        Inventory inventory = new Inventory(2);
        Item item1 = CreateItemData(1, "TestItem1", ItemType.MATERIAL);
        Item item2 = CreateItemData(2, "TestItem2", ItemType.WEAPON);
        inventory.Add(item1);
        inventory.Add(item2);

        inventory.SwapItems(0, 1);

        // Assert
        Assert.AreEqual(item2.data.id, inventory.slots[0].id);
        Assert.AreEqual(item1.data.id, inventory.slots[1].id);
    }

    [Test]
    public void TestSwapItemsWithInvalidIndex()
    {
        // Arrange
        Inventory inventory = new Inventory(2);
        Item item1 = CreateItemData(1, "TestItem1", ItemType.MATERIAL);
        inventory.Add(item1);

        LogAssert.Expect(LogType.Error, "Erreur swap: Index invalide");
        inventory.SwapItems(0, 2);

        // Assert
        // Vérifiez que l'élément n'a pas été déplacé
        Assert.AreEqual(item1.data.id, inventory.slots[0].id);
        // Vérifiez que le deuxième slot est toujours vide
        Assert.AreEqual(ItemType.NONE, inventory.slots[1].type);
    }

}
