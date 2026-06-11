using UnityEngine;
using System.Collections.Generic;

public class DangerZone : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform[] spawnPoints;

    public int enemiesToSpawn = 2;

    private bool spawned;

    public float respawnTime = 20f;

    private float timer;

    public float enemySpeed = 3f;

    public float captureTime = 3f;

    public int escapePresses = 20;

    private bool playerInside;

    void SpawnEnemies()
{
    int amount = Mathf.Min(
        enemiesToSpawn,
        spawnPoints.Length
    );

    for(int i = 0; i < amount; i++)
    {
        GameObject enemy =
    Instantiate(
        enemyPrefab,
        spawnPoints[i].position,
        Quaternion.identity
    );

     EnemyChase chase =
     enemy.GetComponent<EnemyChase>();

        chase.speed = enemySpeed;

        chase.captureTime = captureTime;

        chase.escapePresses = escapePresses;

        chase.player =
        GameObject.FindGameObjectWithTag("Player").transform;

        DangerZoneEnemy zoneEnemy =
            enemy.GetComponent<DangerZoneEnemy>();

        zoneEnemy.homeZone = this;
    }
}

    private void Update()
{
    if (!playerInside)
        return;

    GameObject[] enemies =
        FindGameObjectsWithTagInThisZone();

    if (enemies.Length > 0)
        return;

    timer += Time.deltaTime;

    if (timer >= respawnTime)
    {
        timer = 0f;

        SpawnEnemies();
    }
}

private void OnTriggerEnter(Collider other)
{
    if (!other.CompareTag("Player"))
        return;

    playerInside = true;

    if (!spawned)
    {
        SpawnEnemies();
        spawned = true;
    }
}
private void OnTriggerExit(Collider other)
{
    if (!other.CompareTag("Player"))
        return;

    playerInside = false;

    DestroyAllEnemies();
}


void DestroyAllEnemies()
{
    DangerZoneEnemy[] enemies =
        FindObjectsByType<DangerZoneEnemy>(
            FindObjectsSortMode.None
        );

    foreach (DangerZoneEnemy enemy in enemies)
    {
        if (enemy.homeZone == this)
        {
            Destroy(enemy.gameObject);
        }
    }
}

GameObject[] FindGameObjectsWithTagInThisZone()
{
    DangerZoneEnemy[] allEnemies =
        FindObjectsByType<DangerZoneEnemy>(
            FindObjectsSortMode.None
        );

    List<GameObject> enemies =
        new List<GameObject>();

    foreach (DangerZoneEnemy enemy in allEnemies)
    {
        if (enemy.homeZone == this)
        {
            enemies.Add(enemy.gameObject);
        }
    }

    return enemies.ToArray();
}
}