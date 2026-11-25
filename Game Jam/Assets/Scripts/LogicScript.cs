using TMPro;
using UnityEngine;

public class LogicScript : MonoBehaviour
{
    [SerializeField] private GameObject startGame;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemies;
    [SerializeField] private GameObject endGame;
    [SerializeField] private GameObject playerHealthUI;
    [SerializeField] private TextMeshProUGUI playerHealthText;
    [SerializeField] private GameObject enemySpawners;
    [SerializeField] private GameObject waveUI;
    [SerializeField] private TextMeshProUGUI waveText;
    private PlayerScript playerScript;
    private float waveTimer = 10f;
    private int waveCount = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript = player.gameObject.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.GetDead() == true)
        {
            endGame.SetActive(true);
            player.SetActive(false);
            enemies.SetActive(false);
            enemySpawners.SetActive(false);
        }
        float currentPlayerHealth = playerScript.GetPlayerHealth();
        playerHealthText.text = "Health: " + currentPlayerHealth.ToString();
        waveTimer -= Time.deltaTime;
        if (waveTimer <= 0 && playerScript.GetDead() == false)
        {
            waveCount += 1;
            waveText.text = "Wave: " + waveCount.ToString();
            waveTimer = 10;
        }

    }

    public void OnStartClick()
    {
        startGame.SetActive(false);
        player.SetActive(true);
        enemies.SetActive(true);
        playerHealthUI.SetActive(true);
        enemySpawners.SetActive(true);
        waveUI.SetActive(true);
    }


}
