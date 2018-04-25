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
	public static int Level = 1;

	// Enemy variables
	public static int EnemiesToDefeat;
	public static int EnemiesDefeated;
	public static int EnemiesSpawned;
	public static float SpawnRate;
}
