using UnityEngine;

public class KillOnTouch : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Die(other.gameObject);
        }
    }

    private void Die(GameObject player)
    {
        GameManager.deathCount++;

        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.UpdateDeathUI();
        }

        Destroy(player);
    }
}
