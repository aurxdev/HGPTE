using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

class Portal : MonoBehaviour
{
    public GameObject otherPortal;
    public char direction;

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject player = other.gameObject;
        if (player) {
            if (direction == 'E') {
                player.transform.position = new Vector2(otherPortal.transform.position.x - 1.75f, otherPortal.transform.position.y);
            } else if (direction == 'W') {
                player.transform.position = new Vector2(otherPortal.transform.position.x + 1.75f, otherPortal.transform.position.y);
            } else if (direction == 'N') {
                player.transform.position = new Vector2(otherPortal.transform.position.x, otherPortal.transform.position.y + 1.75f);
            } else if (direction == 'S') {
                player.transform.position = new Vector2(otherPortal.transform.position.x, otherPortal.transform.position.y - 1.75f);
            }
        }
    }
}
