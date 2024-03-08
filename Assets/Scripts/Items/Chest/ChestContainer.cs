using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestContainer : MonoBehaviour
{
    [SerializeField]
    private int capacity;
    [SerializeField]
    private List<Slot> slots;
    [SerializeField]
    private int distance;
    [SerializeField]
    private GameObject chestUI;
    [SerializeField]
    private GameObject canvas;
    private bool isTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTrigger=false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isTrigger)
        {
            GameObject chestPrefab = Instantiate(chestUI);


        }
    }
}
