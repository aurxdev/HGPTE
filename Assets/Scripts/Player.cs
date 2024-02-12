using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player : MonoBehaviour
{
    [SerializeField]
    public Inventory inventory;

    private void Awake()
    {
        inventory = new Inventory(10);
    }
}
