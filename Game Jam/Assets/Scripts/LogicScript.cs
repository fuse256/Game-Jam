using TMPro;
using UnityEngine;
using System;
using System.Collections.Generic;

public class LogicScript : MonoBehaviour
{
    [SerializeField] private GameObject startGame;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemies;
    [SerializeField] private GameObject projectiles;
    [SerializeField] private GameObject endGame;
    [SerializeField] private GameObject playerHealthUI;
    [SerializeField] private TextMeshProUGUI playerHealthText;
    [SerializeField] private GameObject enemySpawners;
    [SerializeField] private GameObject waveUI;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI gameOverWaveText;
    [SerializeField] private GameObject background;
    [SerializeField] private TextMeshProUGUI slideText;
    [SerializeField] private GameObject slideUI;

    [SerializeField] private GameObject selectionPanel;
    [SerializeField] private PowerUpCard[] cards;
    [SerializeField] private List<PowerUp> availablePowerUps;

    private float timer = 0f;
    private PlayerScript playerScript;
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
            projectiles.SetActive(false);
            enemySpawners.SetActive(false);
            slideUI.SetActive(false);
        }
        float currentPlayerHealth = playerScript.GetPlayerHealth();
        playerHealthText.text = "Health: " + currentPlayerHealth.ToString();
        if (enemies.transform.childCount == 0 && playerScript.GetDead() == false)
        {
            waveCount += 1;
            waveText.text = "Wave: " + waveCount.ToString();
            gameOverWaveText.text = "You made it to wave " + waveCount.ToString() + "!";
        }

        if (playerScript.GetIsSliding() == false && startGame.activeSelf == false)
        {
            float slideCooldownTimer = playerScript.GetSlideCooldownTimer();
            float slideCooldown = playerScript.GetSlideCooldown();
            if (slideCooldownTimer < slideCooldown)
            {
                slideUI.SetActive(true);
                slideText.text = (((int) (slideCooldown - slideCooldownTimer) + 1)).ToString();
            }
            else
            {
                slideUI.SetActive(false);
            }
        }
        else
        {
            slideUI.SetActive(false);
        }

        timer += Time.deltaTime;
        if (timer >= 10)
        {
            ShowSelection();
            timer = 0f;
        }

    }

    public void OnStartClick()
    {
        startGame.SetActive(false);
        player.SetActive(true);
        enemies.SetActive(true);
        projectiles.SetActive(true);
        playerHealthUI.SetActive(true);
        enemySpawners.SetActive(true);
        waveUI.SetActive(true);
        background.SetActive(true);
    }

 private Action onChoiceMade;

public void ShowSelection(Action completed = null)
{
    if (availablePowerUps == null || availablePowerUps.Count < cards.Length)
    {
        Debug.LogError("Add at least as many power-ups as there are cards.");
        return;
    }

    onChoiceMade = completed;
    selectionPanel.SetActive(true);
    Time.timeScale = 0f;

    var remaining = new List<PowerUp>(availablePowerUps);

    foreach (PowerUpCard card in cards)
    {
        PowerUp choice = GetRandomPowerUp(remaining);
        card.SetUp(choice, Choose);
    }
}

    private void Choose(PowerUp selected)
    {
        ApplyPowerUp(selected);

        selectionPanel.SetActive(false);
        Time.timeScale = 1f;

        onChoiceMade?.Invoke();
        onChoiceMade = null;
    }

    private void ApplyPowerUp(PowerUp selected)
    {
        //Change player stats
    }
    private PowerUp GetRandomPowerUp(List<PowerUp> remaining)
{
    float roll = UnityEngine.Random.Range(0f, 100f);

    Rarity chosenRarity =
        roll < 50f ? Rarity.Common :
        roll < 75f ? Rarity.Rare :
        roll < 90f ? Rarity.Epic :
                     Rarity.Legendary;

    List<PowerUp> matching = remaining.FindAll(
        powerUp => powerUp.rarity == chosenRarity
    );

    List<PowerUp> pool = matching.Count > 0 ? matching : remaining;

    int index = UnityEngine.Random.Range(0, pool.Count);
    PowerUp choice = pool[index];

    remaining.Remove(choice);
    return choice;
}

}
