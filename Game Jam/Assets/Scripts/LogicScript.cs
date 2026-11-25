using UnityEngine;

public class LogicScript : MonoBehaviour
{
    [SerializeField] private GameObject startGame;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemies;
    [SerializeField] private GameObject endGame;
    private PlayerScript playerScript;
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
        }
    }

    public void OnStartClick()
    {
        startGame.SetActive(false);
        player.SetActive(true);
        enemies.SetActive(true);
    }


}
