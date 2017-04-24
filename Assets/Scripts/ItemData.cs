using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler {
    public Item item;
    public int amount;
    public int slotIndex;
    
    private Vector2 mouseOffset;
    private Inventory inventory;

    private void Start() {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (item != null) {
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            
            mouseOffset = eventData.position - new Vector2(transform.position.x, transform.position.y);

            transform.SetParent(transform.parent.parent);
            transform.position = eventData.position - mouseOffset;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        if (item != null) {
            transform.position = eventData.position - mouseOffset;
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.SetParent(GetCurrentSlotGameObject().transform);
        transform.position = GetCurrentSlotGameObject().transform.position;
    }

    private GameObject GetCurrentSlotGameObject() {
        return inventory.slots[slotIndex];
    }
}
