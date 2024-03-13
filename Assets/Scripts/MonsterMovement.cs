using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{

    [SerializeField]
    private float range = 10;

    private BoxCollider2D boxCollider;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            player = collider.GetComponent<Player>();
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            player = null;
        }
    }


    // Update is called once per frame
    void Update()
    {
        boxCollider.size = new Vector2(range, range);
        //Faire un rigibody2D pour le monstre
        //Faire un script de mouvement pour le monstre
        //Faire l'animator pour le monstre
    }
}
