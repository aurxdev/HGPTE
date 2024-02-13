using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotUI : MonoBehaviour
{
    public Image itemIcon;
    public Text quantityText;

    public void SetItem(Slot slot)
    {   
        if(slot != null)
        {
            itemIcon.sprite = slot.icon;
            quantityText.text = slot.count.ToString();
        }
    }

    public void SetEmpty()
    {
        itemIcon.sprite=null;
        quantityText.text = "";
    }
    
}
