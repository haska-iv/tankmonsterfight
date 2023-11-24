using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public View view;
    public Player playerPrefab;
    public Enemy[] enemyprefabs;
    public int maxEnemy = 10;
    public float spawnRange = 20f;
    public TMPro.TextMeshProUGUI text;
    public TMPro.TextMeshProUGUI gameover;

    private int enemyCount;

    private Player player;
    private List<Enemy> enemies;

    
    private void Awake()
    {
        enemies = new List<Enemy>();
        LoadGame();
    }

    private void Update()
    {
        if (enemyCount < maxEnemy)
            SpawnEnemy();
        SetHealthLabel();

        if (gameover.enabled && Input.GetKeyDown(KeyCode.Space))
            Retry();
    }

    private void LoadGame()
    {
        gameover.enabled = false;
        player = Instantiate(playerPrefab);
        player.onDie += OnPlayerDie;
        view.target = player.transform;
        enemyCount = 0;
    }

    private void SetHealthLabel()
    {
        text.text = $"HEALTH {player.health}";
    }

    private void SpawnEnemy()
    {
        var randomSpawnPos = Random.onUnitSphere; 
        randomSpawnPos.y = 0;
        randomSpawnPos = randomSpawnPos.normalized;
        randomSpawnPos *= spawnRange;

        var spawnPosition = player.transform.position + randomSpawnPos; 

        var enemyPrefab = enemyprefabs[Random.Range(0, enemyprefabs.Length)];
        var enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.SetTarget(player.transform);
        enemies.Add(enemy);
        enemy.onDie += OnEnemyDie;
        enemyCount++;
    }

    private void OnEnemyDie(Entity enemy)
    {
        enemy.onDie -= OnEnemyDie;
        enemies.Remove(enemy as Enemy);
        enemyCount--;
        Destroy(enemy.gameObject);
    }

    private void OnPlayerDie(Entity player)
    {
        player.onDie -= OnPlayerDie;
        gameover.enabled = true;
        Destroy(player.gameObject);
    }

    public void Retry()
    {
        for (int i = 0; i < enemies.Count; i++)
            enemies[i].Kill();
        enemies.Clear();
        LoadGame();
    }
}