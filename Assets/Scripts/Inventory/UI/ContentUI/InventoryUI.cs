using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    public GameObject inventoryPanel;
    [SerializeField]
    public GameObject inventorySlotPrefab;
    [SerializeField]
    public GameObject content;

    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private GameObject inventoryInformations;

    // instance du joueur
    [SerializeField]
    public Player player;

    void Start()
    {
        player.inventory.onInventoryChanged += OnInventoryChanged;
    }

    void Destroy()
    {
        player.inventory.onInventoryChanged -= OnInventoryChanged;
    }
    
    void Update(){
        if (Input.GetKeyDown(KeyCode.Tab))showUI();
    }

    public void showUI(){
        inventoryPanel.SetActive(!inventoryPanel.activeSelf); // on active ou desactive
        if(inventoryPanel.activeSelf) {
            inventoryPanel.transform.LeanMoveLocalX(500, 0.3f).setEaseOutCubic(); // si on ouvre on slide
        } else {
            inventoryPanel.transform.LeanMoveLocalX(inventoryPanel.transform.localPosition.x + 500, 0.1f).setEaseOutCubic(); // si on ferme on slide
        }
        Utils.UpdateUI(!inventoryPanel.activeSelf, player, inventorySlotPrefab, content, canvas, inventoryInformations); // on update l'UI   
    }

    public void maskUI(){
        inventoryPanel.SetActive(false);
        Utils.UpdateUI(true, player, inventorySlotPrefab, content, canvas, inventoryInformations); // on update l'UI
    }

    public void OnInventoryChanged(){
        Utils.UpdateSlotsUI(player, inventorySlotPrefab, content, canvas, inventoryInformations); // on update l'UI
    }
}
