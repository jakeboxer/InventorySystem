using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public Item item;
    public int amount;

    public void OnBeginDrag(PointerEventData eventData) {
        if (item != null) {
            transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        throw new NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData) {
        throw new NotImplementedException();
    }
}
