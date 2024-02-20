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

            int randomIndex = Random.Range(0, 2);

            Vector3 randomOffset = Random.insideUnitSphere * distance;
            Vector3 randomPosition = gameObject.transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);
            GameObject c = Instantiate(collectiblePrefab, randomPosition, gameObject.transform.rotation);
            c.GetComponent<Collectable>().item = collectible[randomIndex];
            /*c.transform.SetParent(gameObject.transform, false);*/
        }
    }
}
