using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {

    //private float spawnRate;
    private float startTime;

    public GameObject EnemyPrefab;
    public GameObject Player;

    //public bool gameOver;

	// Use this for initialization
	void Start () {

		LevelConfigManager.EnemiesDefeated = 0;
		LevelConfigManager.EnemiesSpawned = 0;

		switch (LevelConfigManager.Level) {
		case 1:
			print ("------In case 1 SpawnScript------");
			LevelConfigManager.EnemiesToDefeat = 1;
			LevelConfigManager.SpawnRate = 3f;
			break;
		case 2:
			LevelConfigManager.EnemiesToDefeat = 2;
			LevelConfigManager.SpawnRate = 2f;
			break;
		default:
			LevelConfigManager.EnemiesToDefeat = 3;
			LevelConfigManager.SpawnRate = 1f;
			break;
		}

        //spawnRate = 2f;
        startTime = Time.time;
        //gameOver = false;
	}

	// Update is called once per frame
	void Update () {
		if (!LevelConfigManager.GameOver && Time.time - startTime > LevelConfigManager.SpawnRate
			&& LevelConfigManager.EnemiesSpawned < LevelConfigManager.EnemiesToDefeat)
        {
            spawnEnemy();
            startTime = Time.time;
        }
	}

    void spawnEnemy()
    {
        Vector3 spawnLocation = new Vector3(5.0f, 0, 0);
        GameObject enemy = (GameObject)Instantiate(EnemyPrefab, spawnLocation, new Quaternion());
        DemoEnemyController enemyController = enemy.GetComponent("DemoEnemyController") as DemoEnemyController;
        enemyController.target = Player;
		LevelConfigManager.EnemiesSpawned = LevelConfigManager.EnemiesSpawned + 1;
    }
}
