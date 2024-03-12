using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    public GameObject container;
    [SerializeField]
    public GameObject inventoryInformations;
    public int nb;
    public int id;
    public string description;
    public Sprite iconImage;
    public string nom;
    public int nbSlot;
    [HideInInspector]
    public Transform parentAfterDrag;

    // informations panel
    public void ShowInformations(){
        inventoryInformations.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = iconImage;
        inventoryInformations.transform.GetChild(1).gameObject.GetComponent<Text>().text = nom;
        inventoryInformations.transform.GetChild(2).GetChild(0).gameObject.GetComponent<Text>().text = description;
        inventoryInformations.transform.GetChild(3).gameObject.GetComponent<Text>().text = nb.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ShowInformations();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            Player player = FindObjectOfType<Player>();
            if(player.IsOpening){
                GameObject button = gameObject.transform.GetChild(0).gameObject;
                if(button.activeSelf)button.SetActive(false);
                else button.SetActive(true);
            }
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        ShowInformations();
        parentAfterDrag = transform.parent;
        transform.SetParent(container.transform);
        transform.SetAsLastSibling();
        icon.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        icon.raycastTarget = true;
    }

}