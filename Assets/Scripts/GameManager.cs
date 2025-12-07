using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform spawnPoint;

    [Range(0.1f, 3f)]
    public float playerScale = 1f;

    public TMP_Text deathCounterText;
    public TMP_Text timerText;

    private GameObject currentPlayer;

    public static int deathCount = 0;
    public static float totalTime = 0f;

    void Start()
    {
        UpdateDeathUI();
    }

    void Update()
    {
        totalTime += Time.deltaTime;

        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(totalTime / 60f);
            int seconds = Mathf.FloorToInt(totalTime % 60f);
            int ms = Mathf.FloorToInt((totalTime * 1000f) % 1000f);

            timerText.text =
                minutes.ToString("00") + ":" +
                seconds.ToString("00") + ":" +
                ms.ToString("000");
        }

        if (currentPlayer == null && Input.GetKeyDown(KeyCode.Space))
        {
            RespawnPlayer();
        }
    }

    private void RespawnPlayer()
    {
        currentPlayer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);

        Movement movement = currentPlayer.GetComponent<Movement>();
        if (movement != null)
        {
            movement.SetScale(playerScale);
        }
    }

    public void UpdateDeathUI()
    {
        if (deathCounterText != null)
            deathCounterText.text = "Deaths: " + deathCount;
    }
}
