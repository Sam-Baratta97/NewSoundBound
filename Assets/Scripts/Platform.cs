using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    public float topOffset = 0.05f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        Movement player = collision.gameObject.GetComponent<Movement>();
        if (player == null) return;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            Vector2 normal = contact.normal;

            // Side collision: flip direction
            if (Mathf.Abs(normal.x) > 0.5f && normal.y < 0.5f)
            {
                player.FlipDirection();
                break;
            }
        }
    }
}
