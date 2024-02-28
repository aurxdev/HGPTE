using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    private float interactionDistance;

    [SerializeField]
    private float timeToTeleport = 0.5f;

    private float time = 0f;

    private GameObject player;

    private CircleCollider2D circleCollider2D;

    private GameObject playerTeleporting;

    void Awake()
    {
        player = null;
        circleCollider2D = GetComponent<CircleCollider2D>();
        circleCollider2D.radius = interactionDistance;
    }

    private static Vector2 GetVectorTeleportation(Rigidbody2D rb, GameObject otherPortal, float timeToTeleport)
    {
        switch (otherPortal.GetComponent<Portal>().direction)
        {
            case Direction.E:
                return new Vector2((otherPortal.transform.position.x + 1 - rb.position.x) * (1/timeToTeleport) , (otherPortal.transform.position.y - rb.position.y) * (1/timeToTeleport));

            case Direction.W:
                return new Vector2((otherPortal.transform.position.x - 1 - rb.position.x) * (1/timeToTeleport), (otherPortal.transform.position.y - rb.position.y) * (1/timeToTeleport));
            
            case Direction.N:
                return new Vector2((otherPortal.transform.position.x - rb.position.x) * (1/timeToTeleport), (otherPortal.transform.position.y + 1 - rb.position.y) * (1/timeToTeleport));
            
            case Direction.S:
                return new Vector2((otherPortal.transform.position.x - rb.position.x) * (1/timeToTeleport), (otherPortal.transform.position.y - 1 - rb.position.y) * (1/timeToTeleport));

            default:
                return new Vector2(0, 0);
        }
    }


    private void Teleport() {
        if (state == PortalState.Open)
        {
            if (player)
            {
                playerTeleporting = player;
                playerTeleporting.GetComponent<MovementManager>().IsTeleporting = true;
                Rigidbody2D rb = playerTeleporting.GetComponent<Rigidbody2D>();

                StartCoroutine(TeleportationEffect());
                if (rb)
                {
                    rb.bodyType = RigidbodyType2D.Kinematic;
                    rb.velocity = GetVectorTeleportation(rb, otherPortal, timeToTeleport);
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
    }



    IEnumerator TeleportationEffect()
    {
        // Save original scale and camera position
        Vector3 originalScale = playerTeleporting.transform.localScale;
        float originalCameraOrthographicSize = Camera.main.orthographicSize;

        int originalSortingOrder = playerTeleporting.GetComponentInChildren<SpriteRenderer>().sortingOrder;

        playerTeleporting.GetComponentInChildren<SpriteRenderer>().sortingOrder = 10;

        // Get the MovementManager and SpriteRenderer of the player
        MovementManager movementManager = playerTeleporting.GetComponent<MovementManager>();

        // Increase scale and move camera up
        for (float t = 0; t < 1; t += Time.deltaTime / timeToTeleport)
        {
            if (t < 0.5)
            {
                playerTeleporting.transform.localScale = Vector3.Lerp(originalScale, originalScale * 3.5f, t);
                Camera.main.orthographicSize = Mathf.Lerp(originalCameraOrthographicSize, originalCameraOrthographicSize * 3.5f, t);

                // Rotate the player and change the sprite
                movementManager.LastDirection = 'E'; // Change to the desired direction
                movementManager.setSprite(MovementManager.idSpriteRight);
            }
            else
            {
                playerTeleporting.transform.localScale = Vector3.Lerp(originalScale * 3.5f, originalScale, t);
                Camera.main.orthographicSize = Mathf.Lerp(originalCameraOrthographicSize * 3.5f, originalCameraOrthographicSize, t);

                movementManager.LastDirection = 'W';
                movementManager.setSprite(MovementManager.idSpriteLeft);
            }
            yield return null;
        }

        // Reset scale and camera position
        playerTeleporting.transform.localScale = originalScale;
        Camera.main.orthographicSize = originalCameraOrthographicSize;
        playerTeleporting.GetComponentInChildren<SpriteRenderer>().sortingOrder = originalSortingOrder;

        playerTeleporting.GetComponent<MovementManager>().IsTeleporting = false;
        playerTeleporting.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        playerTeleporting = null;
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        player = other.gameObject;
    }

    private void OnTriggerExit2D()
    {
        player = null;
    }






    void Update()
    {

        if (interactionDistance != circleCollider2D.radius)
        {
            circleCollider2D.radius = interactionDistance;
        }

        if (state == PortalState.Cooldown)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                state = PortalState.Open;
            }
        }

        if (player != null && Input.GetKeyDown(KeyCode.E) && !player.GetComponent<MovementManager>().IsOnAnimation())
        {
            Teleport();
        }


        if (state == PortalState.Open)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = OpenSprite;
        }

    }

    
}
