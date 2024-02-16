using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecoltedItemBarUI : MonoBehaviour
{
    [SerializeField]
    public GameObject content;
    public GameObject inventorySlotPrefab;
    public Player player;
    void Start()
    {
        player.inventory.onInventoryChanged += UpdateUI; // on ajoute l'event
        UpdateUI(); 
    }

    void OnDestroy()
    {
        player.inventory.onInventoryChanged -= UpdateUI;
    }

    public void UpdateUI(){
        List <Slot> slots = player.inventory.slots;
        Slot lastSlot = player.inventory.lastSlot;
        if(lastSlot != null){
            GameObject slotUi = Instantiate(inventorySlotPrefab);
            slotUi.transform.GetChild(0).gameObject.GetComponent<Text>().text = lastSlot.name;
            slotUi.transform.GetChild(1).gameObject.GetComponent<Text>().text = lastSlot.count.ToString();
            slotUi.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = lastSlot.icon;
            slotUi.transform.SetParent(content.transform, false);
            slotUi.transform.SetAsFirstSibling();

            
        // animation fade out
        StartCoroutine(FadeOutAndDestroy(slotUi, 3f));
        }
    }

    private IEnumerator FadeOutAndDestroy(GameObject slotUi, float duration)
    {
        // Attendez une seconde avant de commencer l'animation
        yield return new WaitForSeconds(1);


        CanvasGroup canvasGroup = slotUi.AddComponent<CanvasGroup>();
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            canvasGroup.alpha = 1 - t;
            yield return null; // attends jusqu'à la prochaine frame
        }

        Destroy(slotUi);
    }
}