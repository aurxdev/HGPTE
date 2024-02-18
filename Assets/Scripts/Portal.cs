using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public enum Direction // votre énumération personnalisée
{
    S, N, W, E
};


class Portal : MonoBehaviour
{
    public GameObject otherPortal;
    public Direction direction = Direction.S;

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject player = other.gameObject;
        if (player) {
            if (direction == Direction.E) {
                player.transform.position = new Vector2(otherPortal.transform.position.x - 1.75f, otherPortal.transform.position.y);
            } else if (direction == Direction.W) {
                player.transform.position = new Vector2(otherPortal.transform.position.x + 1.75f, otherPortal.transform.position.y);
            } else if (direction == Direction.N) {
                player.transform.position = new Vector2(otherPortal.transform.position.x, otherPortal.transform.position.y + 1.75f);
            } else if (direction == Direction.S) {
                player.transform.position = new Vector2(otherPortal.transform.position.x, otherPortal.transform.position.y - 1.75f);
            }
        }
    }
}
