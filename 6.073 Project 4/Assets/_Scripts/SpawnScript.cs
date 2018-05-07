using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {

    //private float spawnRate;
    private float startTime;

    public GameObject EnemyPrefab;
    public GameObject Player;

    private int[] totalEnemies = { 3, 5, 8, 10, 10, 12, 15, 15 };
    private float[] spawnRates = { 3f, 2.5f, 2.5f, 2f, 3f, 3f, 2.5f, 2.5f, 2f };

    private Vector3 spawnLocation;
    private GameObject enemy;
    private DemoEnemyController enemyController;

    //public bool gameOver;

    // Use this for initialization
    void Start () {

		LevelConfigManager.EnemiesDefeated = 0;
		LevelConfigManager.EnemiesSpawned = 0;
        LevelConfigManager.sameowraiAttackSpeed = 1;
        LevelConfigManager.sameowraiWindupSpeed = 0.5f;
        LevelConfigManager.sameowraiCoolDownSpeed = 0.5f;
        LevelConfigManager.sameowraiParryWindupSpeed = 1f;
        LevelConfigManager.sameowraiParrySpeed = 0.1f;
        LevelConfigManager.sameowraiParryCooldownSpeed = 1f;
        LevelConfigManager.sameowraiStunSpeed = 0.5f;

        LevelConfigManager.doguraiAttackSpeed = 1;
        LevelConfigManager.doguraiWindupSpeed = 0.1f;
        LevelConfigManager.doguraiCoolDownSpeed = 0.5f;
        LevelConfigManager.doguraiParryWindupSpeed = 1f;
        LevelConfigManager.doguraiParrySpeed = 1f;
        LevelConfigManager.doguraiParryCooldownSpeed = 1f;
        LevelConfigManager.doguraiStunSpeed = 0.1f;


		switch (LevelConfigManager.Level) {
		    case 1:
			    print ("------In case 1 SpawnScript------");
			    LevelConfigManager.EnemiesToDefeat = totalEnemies[LevelConfigManager.Level-1];
			    LevelConfigManager.SpawnRate = spawnRates[LevelConfigManager.Level-1];
			    break;
		    case 2:
			    print ("------In case 2 SpawnScript------");
                LevelConfigManager.EnemiesToDefeat = totalEnemies[LevelConfigManager.Level - 1];
			    LevelConfigManager.SpawnRate = spawnRates[LevelConfigManager.Level - 1];
                break;
            case 3:
                print("------In case 3 SpawnScript------");
                LevelConfigManager.EnemiesToDefeat = totalEnemies[LevelConfigManager.Level - 1];
                LevelConfigManager.SpawnRate = spawnRates[LevelConfigManager.Level - 1];
                break;
            case 4:
                print("------In case 4 SpawnScript------");
                LevelConfigManager.EnemiesToDefeat = totalEnemies[LevelConfigManager.Level - 1];
                LevelConfigManager.SpawnRate = spawnRates[LevelConfigManager.Level - 1];
                break;
            case 5:
                LevelConfigManager.EnemiesToDefeat = totalEnemies[LevelConfigManager.Level - 1];
                LevelConfigManager.SpawnRate = spawnRates[LevelConfigManager.Level - 1];
                break;
            case 6:
                LevelConfigManager.EnemiesToDefeat = totalEnemies[LevelConfigManager.Level - 1];
                LevelConfigManager.SpawnRate = spawnRates[LevelConfigManager.Level - 1];
                break;
            case 7:
                LevelConfigManager.EnemiesToDefeat = totalEnemies[LevelConfigManager.Level - 1];
                LevelConfigManager.SpawnRate = spawnRates[LevelConfigManager.Level - 1];
                break;
            case 8:
                LevelConfigManager.EnemiesToDefeat = totalEnemies[LevelConfigManager.Level - 1];
                LevelConfigManager.SpawnRate = spawnRates[LevelConfigManager.Level - 1];
                break;
            case 9:
                LevelConfigManager.EnemiesToDefeat = totalEnemies[LevelConfigManager.Level - 1];
                LevelConfigManager.SpawnRate = spawnRates[LevelConfigManager.Level - 1];
                break;
            default:
			    print ("-----In case default SpawnScript---");
			    print ("LevelConfigManager.Level is" + LevelConfigManager.Level);
            
			    LevelConfigManager.EnemiesToDefeat = LevelConfigManager.Level * (int)(5/3.0);
			    LevelConfigManager.SpawnRate = (float)LevelConfigManager.Level/4f;
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
        
        switch (LevelConfigManager.Level)
        {
            case 1:
                print("------In Level 1 SpawnScript------");
                Vector3 spawnLocation = new Vector3(5.0f, -1.15f, 0);
                GameObject enemy = (GameObject)Instantiate(EnemyPrefab, spawnLocation, new Quaternion());
                DemoEnemyController enemyController = enemy.GetComponent("DemoEnemyController") as DemoEnemyController;
                enemyController.target = Player;
                EntityManager em = enemyController.GetComponent("EntityManager") as EntityManager;
                em.health = 1;
                em.damage = 1;
                float attackSpeed = LevelConfigManager.doguraiAttackSpeed;
                LevelConfigManager.EnemiesSpawned += 1;
                break;
            case 2:
                print("------In Level 2 SpawnScript------");
                spawnLocation = new Vector3(5.0f, -1.15f, 0);
                enemy = (GameObject)Instantiate(EnemyPrefab, spawnLocation, new Quaternion());
                enemyController = enemy.GetComponent("DemoEnemyController") as DemoEnemyController;
                enemyController.target = Player;
                em = enemyController.GetComponent("EntityManager") as EntityManager;
                em.health = 1;
                em.damage = 2;
                LevelConfigManager.EnemiesSpawned += 1;
                break;
            case 3:
                print("------In Level 3 SpawnScript------");
                spawnLocation = new Vector3(5.0f, -1.15f, 0);
                enemy = (GameObject)Instantiate(EnemyPrefab, spawnLocation, new Quaternion());
                enemyController = enemy.GetComponent("DemoEnemyController") as DemoEnemyController;
                enemyController.target = Player;
                em = enemyController.GetComponent("EntityManager") as EntityManager;
                em.health = 1;
                em.damage = 1;
                em.speed += em.speed / 2f;
                LevelConfigManager.doguraiAttackSpeed = LevelConfigManager.doguraiAttackSpeed / 2f;
                LevelConfigManager.EnemiesSpawned += 1;
                break;
            case 4:
                print("------In Level 4 SpawnScript------");
                spawnLocation = new Vector3(5.0f, -1.15f, 0);
                enemy = (GameObject)Instantiate(EnemyPrefab, spawnLocation, new Quaternion());
                enemyController = enemy.GetComponent("DemoEnemyController") as DemoEnemyController;
                enemyController.target = Player;
                em = enemyController.GetComponent("EntityManager") as EntityManager;
                em.health = 1;
                em.damage = 2;
                //LevelConfigManager.doguraiAttackSpeed = LevelConfigManager.doguraiAttackSpeed / 2f;
                LevelConfigManager.EnemiesSpawned += 1;
                break;
            case 5:
                print("------In Level 5 SpawnScript------");
                spawnLocation = new Vector3(5.0f, -1.15f, 0);
                enemy = (GameObject)Instantiate(EnemyPrefab, spawnLocation, new Quaternion());
                enemyController = enemy.GetComponent("DemoEnemyController") as DemoEnemyController;
                enemyController.target = Player;
                em = enemyController.GetComponent("EntityManager") as EntityManager;
                em.health = 1;
                em.damage = 100;
                em.speed += em.speed / 2f;
                attackSpeed = 1f;
                LevelConfigManager.doguraiAttackSpeed = attackSpeed * 0.75f;
                LevelConfigManager.EnemiesSpawned += 1;
                break;
            case 6:
                print("------In Level 6 SpawnScript------");
                spawnLocation = new Vector3(5.0f, -1.15f, 0);
                enemy = (GameObject)Instantiate(EnemyPrefab, spawnLocation, new Quaternion());
                enemyController = enemy.GetComponent("DemoEnemyController") as DemoEnemyController;
                enemyController.target = Player;
                em = enemyController.GetComponent("EntityManager") as EntityManager;
                em.health = 2;
                em.damage = 3;
                attackSpeed = 1f;
                LevelConfigManager.doguraiAttackSpeed = attackSpeed + attackSpeed / 2f;
                LevelConfigManager.EnemiesSpawned += 1;
                break;
            case 7:
                print("------In Level 7 SpawnScript------");
                spawnLocation = new Vector3(5.0f, -1.15f, 0);
                enemy = (GameObject)Instantiate(EnemyPrefab, spawnLocation, new Quaternion());
                enemyController = enemy.GetComponent("DemoEnemyController") as DemoEnemyController;
                enemyController.target = Player;
                em = enemyController.GetComponent("EntityManager") as EntityManager;
                em.health = 2;
                em.damage = 2;
                em.speed += em.speed * 0.75f;
                attackSpeed = 1f;
                LevelConfigManager.doguraiAttackSpeed = attackSpeed + attackSpeed*0.75f;
                LevelConfigManager.EnemiesSpawned += 1;
                break;
            case 8:
                print("------In Level 8 SpawnScript------");
                spawnLocation = new Vector3(-5.0f, -1.15f, 0);
                enemy = (GameObject)Instantiate(EnemyPrefab, spawnLocation, new Quaternion());
                enemyController = enemy.GetComponent("DemoEnemyController") as DemoEnemyController;
                enemyController.target = Player;
                em = enemyController.GetComponent("EntityManager") as EntityManager;
                em.health = 2;
                em.damage = 3;
                attackSpeed = 1f;
                LevelConfigManager.doguraiAttackSpeed = attackSpeed - attackSpeed / 4f;
                LevelConfigManager.EnemiesSpawned += 1;
                break;
            case 9:
                print("------In Level 9 SpawnScript------");
                spawnLocation = new Vector3(5.0f, -1.15f, 0);
                enemy = (GameObject)Instantiate(EnemyPrefab, spawnLocation, new Quaternion());
                enemyController = enemy.GetComponent("DemoEnemyController") as DemoEnemyController;
                enemyController.target = Player;
                em = enemyController.GetComponent("EntityManager") as EntityManager;
                em.health = 2;
                em.damage = 2;
                em.speed += em.speed * 0.75f;
                attackSpeed = 1f;
                LevelConfigManager.doguraiAttackSpeed = attackSpeed - attackSpeed / 2f;
                LevelConfigManager.EnemiesSpawned += 1;
                break;
            case 10:
                print("------In Level 10 SpawnScript------");
                spawnLocation = new Vector3(5.0f, -1.15f, 0);
                enemy = (GameObject)Instantiate(EnemyPrefab, spawnLocation, new Quaternion());
                enemyController = enemy.GetComponent("DemoEnemyController") as DemoEnemyController;
                enemyController.target = Player;
                em = enemyController.GetComponent("EntityManager") as EntityManager;
                em.health = 2;
                em.damage = 100;
                em.speed += em.speed * 0.75f;
                attackSpeed = 1f;
                LevelConfigManager.doguraiAttackSpeed = attackSpeed - attackSpeed / 2f;
                LevelConfigManager.EnemiesSpawned += 1;
                break;
            default:
                print("-----In case default SpawnScript---");
                spawnLocation = new Vector3(5.0f, -1.15f, 0);
                enemy = (GameObject)Instantiate(EnemyPrefab, spawnLocation, new Quaternion());
                enemyController = enemy.GetComponent("DemoEnemyController") as DemoEnemyController;
                enemyController.target = Player;
                LevelConfigManager.EnemiesSpawned = LevelConfigManager.EnemiesSpawned + 1;
                break;
        }
    }
}
