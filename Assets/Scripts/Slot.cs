using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler {
    public int index;
    private Inventory inventory;

    private void Start() {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    public void OnDrop(PointerEventData eventData) {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
        Item itemForOldSlot;

        if (ContainsItem()) {
            // If this slot already contains an item, swap it into the dropped
            // item's old slot.
            itemForOldSlot = GetCurrentItem();

            Transform currentItemTransform = transform.GetChild(0);
            Transform oldDroppedItemSlotTransform = inventory.slots[droppedItem.slotIndex].transform;

            currentItemTransform.GetComponent<ItemData>().slotIndex = droppedItem.slotIndex;
            currentItemTransform.SetParent(oldDroppedItemSlotTransform);
            currentItemTransform.position = oldDroppedItemSlotTransform.position;
            inventory.items[droppedItem.slotIndex] = GetCurrentItem();
            
            droppedItem.transform.SetParent(transform);
            droppedItem.transform.position = transform.position;
        } else {
            // If this slot is empty, drop the item in and empty out the item's
            // old slot.
            itemForOldSlot = new Item();
        }

        inventory.items[droppedItem.slotIndex] = itemForOldSlot;
        inventory.items[index] = droppedItem.item;
        droppedItem.slotIndex = index;
    }

    public Item GetCurrentItem() {
        return inventory.items[index];
    }

    public bool ContainsItem() {
        return GetCurrentItem().ID > -1;
    }
}
