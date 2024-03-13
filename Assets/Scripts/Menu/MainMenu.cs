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
    }
    void Update()
    {
        if(player.IsPausing || player.IsDead)ShowUI();
        else HideUI();
    }

    public void ShowUI()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void HideUI()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
    }
}

