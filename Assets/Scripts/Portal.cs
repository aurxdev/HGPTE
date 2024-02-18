using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public enum Direction // votre énumération personnalisée
{
    S, N, W, E
};

public enum PortalState
{
    Open, Cooldown,Close
}


class Portal : MonoBehaviour
{
    [SerializeField]
    private GameObject otherPortal;
    
    [SerializeField]
    private Direction direction = Direction.S;

    private PortalState state = PortalState.Open;

    [SerializeField]
    private Sprite CooldownSprite;

    [SerializeField]
    private Sprite OpenSprite; 

    [SerializeField]
    private float cooldown = 3f;

    private float time = 0f;

    private GameObject player;


    void Start()
    {
        player = null;
    }


    private void Teleport() {
        if (state == PortalState.Open)
        {
            if (player)
            {
                if (otherPortal.GetComponent<Portal>().direction == Direction.E)
                {
                    player.transform.position = new Vector2(otherPortal.transform.position.x + 3, otherPortal.transform.position.y);
                }
                else if (otherPortal.GetComponent<Portal>().direction == Direction.W)
                {
                    player.transform.position = new Vector2(otherPortal.transform.position.x - 3, otherPortal.transform.position.y);
                }
                else if (otherPortal.GetComponent<Portal>().direction == Direction.N)
                {
                    player.transform.position = new Vector2(otherPortal.transform.position.x, otherPortal.transform.position.y + 3);
                }
                else if (otherPortal.GetComponent<Portal>().direction == Direction.S)
                {
                    player.transform.position = new Vector2(otherPortal.transform.position.x, otherPortal.transform.position.y - 3);
                }

                gameObject.GetComponent<SpriteRenderer>().sprite = CooldownSprite;
                state = PortalState.Cooldown;
                time = cooldown;

                otherPortal.GetComponent<Portal>().time = otherPortal.GetComponent<Portal>().cooldown;
                otherPortal.GetComponent<Portal>().state = PortalState.Cooldown;
                otherPortal.gameObject.GetComponent<SpriteRenderer>().sprite = otherPortal.GetComponent<Portal>().CooldownSprite;
            }
        } else if (state == PortalState.Cooldown)
        {
            state = PortalState.Close;
            otherPortal.GetComponent<Portal>().state = PortalState.Close;
            Destroy(gameObject);
            Destroy(otherPortal);
        }
        player = null;
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        player = other.gameObject;
    }

    void OnTriggerExit2D()
    {
        player = null;
    }


    void Update()
    {
        if (state == PortalState.Cooldown)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                state = PortalState.Open;
            }
        }

        if (player != null && Input.GetKeyDown(KeyCode.E))
        {
            Teleport();
        }

        if (state == PortalState.Cooldown)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = CooldownSprite;
        }
        else if (state == PortalState.Open)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = OpenSprite;
        }

    }

    
}
