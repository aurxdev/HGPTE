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
    public void TestAddItemWhenInventoryIsFull()
    {
    // Arrange
    Inventory inventory = new Inventory(1);
    Item item1 = CreateItemData(1, "TestItem1", ItemType.MATERIAL);
    Item item2 = CreateItemData(2, "TestItem2", ItemType.WEAPON);
    inventory.Add(item1);

    // Act
    inventory.Add(item2);

    // Assert
    // Vérifiez que le nombre d'éléments dans l'inventaire est toujours 1
    Assert.AreEqual(1, inventory.NumberOfItems());
    // Vérifiez que l'élément dans l'inventaire est toujours item1
    Assert.AreEqual(item1.data.id, inventory.slots[0].id);
    }

    [Test]
    public void TestAddSameItemMultipleTimes()
    {
    // Arrange
    Inventory inventory = new Inventory(2);
    Item item = CreateItemData(1, "TestItem", ItemType.MATERIAL);

    // Act
    inventory.Add(item);
    inventory.Add(item);

    // Assert
    // Vérifiez que le nombre d'éléments dans l'inventaire est 2
    Assert.AreEqual(2, inventory.slots[0].count);
    }

    [Test]
    public void TestAddDifferentItems()
    {
    // Arrange
    Inventory inventory = new Inventory(2);
    Item item1 = CreateItemData(1, "TestItem1", ItemType.MATERIAL);
    Item item2 = CreateItemData(2, "TestItem2", ItemType.WEAPON);

    // Act
    inventory.Add(item1);
    inventory.Add(item2);

    // Assert
    // Vérifiez que le nombre d'éléments dans l'inventaire est 2
    Assert.AreEqual(2, inventory.NumberOfItems());
    // Vérifiez que les éléments dans l'inventaire sont item1 et item2
    Assert.AreEqual(item1.data.id, inventory.slots[0].id);
    Assert.AreEqual(item2.data.id, inventory.slots[1].id);
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
    public void TestRemoveItemWhenInventoryIsEmpty()
    {
    // Arrange
    Inventory inventory = new Inventory(1);
    Item item = CreateItemData(1, "TestItem", ItemType.MATERIAL);

    // Act
    inventory.Remove(item);

    // Assert
    // Vérifiez que le nombre d'éléments dans l'inventaire est toujours 0
    Assert.AreEqual(0, inventory.NumberOfItems());
    }

    [Test]
    public void TestRemoveItemNotInInventory()
    {
    // Arrange
    Inventory inventory = new Inventory(1);
    Item item1 = CreateItemData(1, "TestItem1", ItemType.MATERIAL);
    Item item2 = CreateItemData(2, "TestItem2", ItemType.WEAPON);
    inventory.Add(item1);

    // Act
    inventory.Remove(item2);

    // Assert
    // Vérifiez que le nombre d'éléments dans l'inventaire est toujours 1
    Assert.AreEqual(1, inventory.NumberOfItems());
    // Vérifiez que l'élément dans l'inventaire est toujours item1
    Assert.AreEqual(item1.data.id, inventory.slots[0].id);
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

    [Test]
    public void TestSwapItemWithItself()
    {
        // Arrange
        Inventory inventory = new Inventory(1);
        Item item = CreateItemData(1, "TestItem", ItemType.MATERIAL);
        inventory.Add(item);

        // Act
        inventory.SwapItems(0, 0);

        // Assert
        // Vérifiez que l'élément est toujours dans le même slot
        Assert.AreEqual(item.data.id, inventory.slots[0].id);
    }

    [Test]
    public void TestSwapItemsWithOneEmptySlot()
    {
        // Arrange
        Inventory inventory = new Inventory(2);
        Item item = CreateItemData(1, "TestItem", ItemType.MATERIAL);
        inventory.Add(item);

        // Act
        inventory.SwapItems(0, 1);

        // Assert
        // Vérifiez que l'élément a été déplacé au deuxième slot
        Assert.AreEqual(item.data.id, inventory.slots[1].id);
        // Vérifiez que le premier slot est maintenant vide
        Assert.AreEqual(ItemType.NONE, inventory.slots[0].type);
    }

    [Test]
    public void TestContainsItem()
    {
    // Arrange
    Inventory inventory = new Inventory(1);
    Item item = CreateItemData(1, "TestItem", ItemType.MATERIAL);
    inventory.Add(item);

    // Act
    bool result = inventory.Contains(item.data.id, 1);

    // Assert
    // Vérifiez que l'inventaire contient l'élément
    Assert.IsTrue(result);
    }

    [Test]
    public void TestCraftItem()
    {
    // Arrange
    Inventory inventory = new Inventory(3);
    Item item1 = CreateItemData(1, "TestItem1", ItemType.MATERIAL);
    Item item2 = CreateItemData(2, "TestItem2", ItemType.WEAPON);
    Item itemData = CreateItemData(3, "TestItem3", ItemType.WEAPON);
    inventory.Add(item1);
    inventory.Add(item2);

    // Act
    bool result = inventory.Craft(item1.data.id, 1, item2.data.id, 1, itemData.data);

    // Assert
    // Vérifiez que l'artisanat a réussi
    Assert.IsTrue(result);
    // Vérifiez que l'inventaire contient le nouvel élément
    Assert.AreEqual(itemData.data.id, inventory.slots[0].id);
    }

    [Test]
    public void TestRemoveWithID()
    {
    // Arrange
    Inventory inventory = new Inventory(1);
    Item item = CreateItemData(1, "TestItem", ItemType.MATERIAL);
    inventory.Add(item);

    // Act
    inventory.RemoveWithID(item.data.id, 1);

    // Assert
    // Vérifiez que l'inventaire est vide
    Assert.AreEqual(0, inventory.NumberOfItems());
    }

    [Test]
    public void TestRemoveAll()
    {
    // Arrange
    Inventory inventory = new Inventory(2);
    Item item1 = CreateItemData(1, "TestItem1", ItemType.MATERIAL);
    Item item2 = CreateItemData(2, "TestItem2", ItemType.WEAPON);
    inventory.Add(item1);
    inventory.Add(item2);

    // Act
    inventory.RemoveAll();

    // Assert
    // Vérifiez que l'inventaire est vide
    Assert.AreEqual(0, inventory.NumberOfItems());
    }

    }
