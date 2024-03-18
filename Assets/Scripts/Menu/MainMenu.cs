using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [HideInInspector]
    public Player player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    } // Start()
    void Update()
    {
        if(player.IsPausing || player.IsDead)ShowUI();
        else HideUI();
    }// Update()


    public void ShowUI()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
    } // ShowUI()

    public void HideUI()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
    } // HideUI()
}

