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
    }

    void OnDestroy()
    {
        player.inventory.onInventoryChanged -= UpdateUI;
    }

    public void UpdateUI(){
        List <Slot> slots = player.inventory.slots;
        Slot lastSlot = player.inventory.lastSlot;
        if(lastSlot != null){

            Transform existingSlot = null;

            // on parcours les slots pour voir si on a deja un slot avec le meme nom
            foreach (Transform child in content.transform)
            {
                Text childSlot = child.transform.GetChild(0).gameObject.GetComponent<Text>();
                // Debug.Log("childSlot: " + childSlot.text);
                if (childSlot != null && childSlot.text == lastSlot.name)
                {
                    existingSlot = child;
                    break;
                }
            }
            if (existingSlot)
            {
                Text quantityText = existingSlot.GetChild(1).gameObject.GetComponent<Text>();
                int quantity = int.Parse(quantityText.text);
                quantityText.text = (quantity + 1).ToString();
            }
            else{
                GameObject slotUi = Instantiate(inventorySlotPrefab);
                slotUi.transform.GetChild(0).gameObject.GetComponent<Text>().text = lastSlot.name;
                slotUi.transform.GetChild(1).gameObject.GetComponent<Text>().text = "1";
                slotUi.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = lastSlot.icon;
                slotUi.transform.SetParent(content.transform, false);
                slotUi.transform.SetAsFirstSibling();

                // animation fade out
                StartCoroutine(FadeOutAndDestroy(slotUi, 3f));
            }
        }
    }

    private IEnumerator FadeOutAndDestroy(GameObject slotUi, float duration)
    {
        // on attends 1sec
        yield return new WaitForSeconds(4);


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