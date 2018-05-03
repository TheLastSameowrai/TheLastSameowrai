using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class LevelConfigManager {

	// Timer UI variables
	public static Text Timer;
	public static float StartTime;
	public static bool FirstTime = true;

	// Game state variables
	public static bool GameOver = false;
	public static bool Paused = false;
	public static int Level = 1;

	// Enemy variables
	public static int EnemiesToDefeat;
	public static int EnemiesDefeated;
	public static int EnemiesSpawned;
	public static float SpawnRate;

	// Dog variables
    public static float doguraiWindupSpeed;
    public static float doguraiAttackSpeed;
    public static float doguraiCoolDownSpeed;

	// Cat variables
    public static float sameowraiWindupSpeed;
    public static float sameowraiAttackSpeed;
    public static float sameowraiCoolDownSpeed;

    public static Data dataManager;

}
