using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCopper : MonoBehaviour
{
    [SerializeField]
    float distance;
    [SerializeField]
    private Sprite openSprite;
    [SerializeField]
    private GameObject collectiblePrefab;
    [SerializeField]
    private List<ScriptableObject> collectible;

    bool isTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            isTrigger = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isTrigger)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = openSprite;

        }
    }
}
