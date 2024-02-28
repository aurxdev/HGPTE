using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped != null && transform.childCount == 0)
        {
            DraggableItem draggedItem = dropped.GetComponent<DraggableItem>();
            if(draggedItem != null)draggedItem.parentAfterDrag = transform;
        }
        else if(transform.childCount == 1) {
            Transform existingItem = transform.GetChild(0);
            DraggableItem draggedItem = dropped.GetComponent<DraggableItem>();
            if(draggedItem != null){
                existingItem.SetParent(draggedItem.parentAfterDrag);
                draggedItem.parentAfterDrag = transform;
            }
        }
    }
}
