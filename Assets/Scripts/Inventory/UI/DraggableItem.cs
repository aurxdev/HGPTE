using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField]
    private Image icon;
    [HideInInspector] 
    private Vector3 startPosition;
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        icon.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = startPosition;
        icon.raycastTarget = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
    DraggableItem droppedItem = eventData.pointerDrag.gameObject.GetComponent<DraggableItem>();
    if (droppedItem != null)
    {
        // Debug.Log(droppedItem.name + " was dropped on " + name);
        int droppedItemIndex = droppedItem.transform.GetSiblingIndex();
        int thisItemIndex = transform.GetSiblingIndex();
        Debug.Log("This item:"+thisItemIndex + " dropped:" + droppedItemIndex);
        droppedItem.transform.SetSiblingIndex(thisItemIndex);
        transform.SetSiblingIndex(droppedItemIndex);
    }
    }
}