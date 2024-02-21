using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player : MonoBehaviour
{
    [SerializeField]
    public Inventory inventory;

    public const int TAILLE_INVENTAIRE = 10;

    private void Awake()
    {
        inventory = new Inventory(TAILLE_INVENTAIRE);
    }
}
