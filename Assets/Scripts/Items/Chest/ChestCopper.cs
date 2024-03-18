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
    [SerializeField]
    public int maxItem;

    // lorsque le joueur entre dans la zone du coffre
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (collision.CompareTag("Player"))
        {
            isTrigger = true;
        }
    } // OnTriggerEnter2D(Collider2D)

    // lorsque le joueur sort de la zone du coffre
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))return;
        isTrigger=false;
    } // OnTriggerExit2D(Collider2D)

    // spawn un item dans le coffre
    private void SpawnItem(){
        gameObject.GetComponent<SpriteRenderer>().sprite = openSprite;

        int randomIndex = Random.Range(0, collectible.Count);

        Vector3 randomOffset = Random.insideUnitSphere * distance;
        Vector3 randomPosition = new Vector3(randomOffset.x, randomOffset.y, 0);
        GameObject c = Instantiate(collectiblePrefab, randomPosition, gameObject.transform.rotation);

        // on set l'item dans le collectable
        c.GetComponent<Collectable>().item = collectible[randomIndex];

        c.transform.SetParent(gameObject.transform, false);
    } // SpawnItem()

    // appelé à chaque frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isTrigger && !isOpen)
        {
            int nbItem = Random.Range(1, maxItem);
            for (int i = 0; i < nbItem; i++)
            {
                SpawnItem();
            }
        isOpen = true;
        }
    } // Update()


}
