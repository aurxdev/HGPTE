using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour, IPointerClickHandler
{
    private Player player;
    void Start(){
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if(gameObject.name == "PlayButton"){
                transform.parent.parent.parent.gameObject.SetActive(false);
                player.IsPausing = false;
            }
            else if(gameObject.name == "QuitButton")Application.Quit();
        }
    }
}
