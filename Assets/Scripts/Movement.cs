using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float jumpForce = 5f;
    private bool movingRight = true;
    private bool isGrounded = false;
    public AudioSource source;
    public AudioDetection detector;

    public float loudnessSensibility = 100;
    public float threshold = 0.1f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    [SerializeField] private Transform feetSensor;
    [SerializeField] private float groundCheckRadius = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Ground check
        isGrounded = false;
        Collider2D[] hits = Physics2D.OverlapCircleAll(feetSensor.position, groundCheckRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Floor"))
            {
                isGrounded = true;
                break;
            }
        }
        // Microphone Detector
        float loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;
        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        if (loudness > threshold && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }


        if (sr != null)
            sr.flipX = !movingRight;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2((movingRight ? 1f : -1f) * moveSpeed, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            FlipDirection();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goal"))
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels! Game complete.");
        }
    }

    public void FlipDirection()
    {
        movingRight = !movingRight;
    }

    // --- NEW METHOD ---
    public void SetScale(float scale)
    {
        transform.localScale = Vector3.one * scale;

        // Scale jump force with sqrt of size for realistic jump
        jumpForce *= Mathf.Sqrt(scale);

    }
}
