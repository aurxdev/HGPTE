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
    private List<ItemData> collectible;

    private bool isTrigger;

    private bool isOpen=false;

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

            int randomIndex = Random.Range(0, collectible.Count-1);

            Vector3 randomOffset = Random.insideUnitSphere * distance;
            Vector3 randomPosition = new Vector3(randomOffset.x, randomOffset.y, 0);
            GameObject c = Instantiate(collectiblePrefab, randomPosition, gameObject.transform.rotation);

            // on set l'item dans le collectable
            c.GetComponent<Collectable>().item = collectible[randomIndex];


            c.transform.SetParent(gameObject.transform, false);
        }
    }
}
