using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OpenInventory : MonoBehaviour
{
    [SerializeField]
    public GameObject inventory;
    public void showUI()
    {
        inventory.SetActive(!inventory.activeSelf); // on active ou desactive
    }


}
