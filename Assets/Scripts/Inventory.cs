using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    int slotCount;
    GameObject inventoryPanel;
    GameObject slotPanel;
    ItemDatabase itemDatabase;

    public GameObject inventorySlot;
    public GameObject inventoryItem;

    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    void Start() {
        slotCount = 16;
        inventoryPanel = GameObject.Find("Inventory Panel");
        slotPanel = inventoryPanel.transform.FindChild("Slot Panel").gameObject;
        itemDatabase = GetComponent<ItemDatabase>();

        for (int i = 0; i < slotCount; i++) {
            GameObject slot = Instantiate(inventorySlot);
            slot.GetComponent<Slot>().index = i;
            slot.transform.SetParent(slotPanel.transform);
            slots.Add(slot);

            items.Add(new Item());
        }

        AddItem(0);
        AddItem(1);
        AddItem(1);
        AddItem(1);
        AddItem(1);
    }

    public void AddItem(int id) {
        Item itemToAdd = itemDatabase.FindItemById(id);

        if (itemToAdd.Stackable && IsInInventory(itemToAdd)) {
            for (int i = 0; i < items.Count; i++) {
                Item item = items[i];

                if (item.ID == id) {
                    GameObject itemGameObject = slots[i].transform.GetChild(0).gameObject;
                    ItemData itemData = itemGameObject.GetComponent<ItemData>();

                    itemData.amount++;
                    itemGameObject.transform.FindChild("Stack Amount").GetComponent<Text>().text = itemData.amount.ToString();

                    break;
                }
            }
        } else {
            for (int i = 0; i < items.Count; i++) {
                if (items[i].ID == -1) {
                    items[i] = itemToAdd;

                    GameObject itemGameObject = Instantiate(inventoryItem);
                    itemGameObject.transform.SetParent(slots[i].transform);
                    itemGameObject.transform.position = Vector2.zero;
                    itemGameObject.GetComponent<Image>().sprite = itemToAdd.Sprite;
                    itemGameObject.name = itemToAdd.Title;

                    ItemData itemData = itemGameObject.GetComponent<ItemData>();
                    itemData.item = itemToAdd;
                    itemData.amount = 1;
                    itemData.slotIndex = i;

                    break;
                }
            }
        }
    }

    bool IsInInventory(Item itemToCheck) {
        foreach (Item item in items) {
            if (itemToCheck.ID == item.ID) {
                return true;
            }
        }

        return false;
    }
}
