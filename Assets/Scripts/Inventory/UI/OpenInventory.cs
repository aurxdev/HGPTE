using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OpenInventory : MonoBehaviour
{
    [SerializeField]
    public GameObject inventory;
    public GameObject btn;
    void Update()
    {
        btn.transform.GetComponent<Button>().onClick.AddListener(() => activeInventory(inventory.activeSelf));
    }

    public void activeInventory(Boolean show)
    {
        inventory.SetActive(show);
    }
}
