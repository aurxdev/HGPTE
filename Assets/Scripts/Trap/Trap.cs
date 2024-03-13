using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    public abstract void Action(Collider2D collision);
    public abstract void Leave(Collider2D collision);
    public abstract void Update();

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Action(collision);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Leave(collision);
        }
    }
}