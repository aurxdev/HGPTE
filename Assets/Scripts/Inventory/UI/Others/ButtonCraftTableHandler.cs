using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonCraftTableHandler : MonoBehaviour, IPointerClickHandler
{

    [SerializeField]
    public ItemData itemData;
    [SerializeField]
    public ItemData itemData2;
    [SerializeField]
    public ItemData itemData3;
    [SerializeField]
    public ItemData itemData4;
    [SerializeField]
    public ItemData itemData5;
    [SerializeField]
    public ItemData itemData6;
    [SerializeField]
    public ItemData itemData7;
    [SerializeField]
    public Sprite errorSprite;
    [SerializeField]
    public Sprite defaultSprite;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            bool res = false;
            string name = gameObject.name;
            Player player = FindObjectOfType<Player>();
            if(player == null) return;
            if(name == "ButtonCraft"){
                res = player.inventory.Craft(itemData4.id, 1, itemData5.id, 2, itemData);
            }
            else if(name == "ButtonCraft (1)"){
                res = player.inventory.Craft(itemData4.id, 2, itemData5.id,2, itemData2);
            }
            else if(name == "ButtonCraft (2)"){
                res = player.inventory.Craft(itemData7.id, 2, itemData6.id, 1, itemData3);
            }

            if(!res){
                gameObject.GetComponent<Image>().sprite = errorSprite;
                gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
                StartCoroutine(ResetAfterDelay());
            }

        }
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<Image>().sprite = defaultSprite;
        gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Craft";
    }

}