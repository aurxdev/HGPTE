using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItemChest : MonoBehaviour, IPointerClickHandler
{
    public int nbSlot;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Player player = FindObjectOfType<Player>();
            if(player.IsOpening){
                GameObject button = gameObject.transform.GetChild(3).gameObject;
                if(button.activeSelf)button.SetActive(false);
                else button.SetActive(true);
            }
        }
    }

}