using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject enemy;
    private float spawnRate = 10f;
    private float timer = 0f;
    [SerializeField] private GameObject enemies;
    private Rigidbody2D enemyRb;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            GameObject newEnemy = Instantiate(enemy,gameObject.transform.position, Quaternion.identity, enemies.transform);
            timer = 0f;
        }
    }
}
