using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class IHarvest : MonoBehaviour
{
    public Player player;

    [SerializeField]
    public int minAmount = 1;

    [SerializeField]
    public int maxAmount = 5;

    [SerializeField]
    public GameObject collectiblePrefab;

    [SerializeField]
    public List<ItemData> collectible;

    [SerializeField]
    public float harvestTime = 2.0f;

    public bool isHarvesting = false;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject.GetComponent<Player>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = null;
        }
    }

    public abstract IEnumerator Harvest();

    public abstract void Update();
}
