using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {

    //private float spawnRate;
    private float startTime;

    public GameObject WhiteDogurai;
	public GameObject YellowDogurai;
	public GameObject OrangeDogurai;
	public GameObject GreenDogurai;
	public GameObject BlueDogurai;
	public GameObject PurpleDogurai;
	public GameObject BrownDogurai;
	public GameObject RedDogurai;
	public GameObject BlackDogurai;

    public GameObject Player;

    private int[] totalEnemies = { 3, 5, 8, 10, 10, 12, 15, 15, 25, 1 };
    private float[] spawnRates = { 3f, 2.5f, 2.5f, 2.5f, 2.5f, 2f, 2f, 2f, 2f, 0f };
	private int[] spawnProbabilities = { 100, 0, 0, 0, 0, 0, 0, 0, 0 };

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

        LevelConfigManager.doguraiWindupSpeed = 0.1f;
        LevelConfigManager.doguraiCoolDownSpeed = 0.5f;
        LevelConfigManager.doguraiParryWindupSpeed = 1f;
        LevelConfigManager.doguraiParrySpeed = 1f;
        LevelConfigManager.doguraiParryCooldownSpeed = 1f;
        LevelConfigManager.doguraiStunSpeed = 0.1f;

		LevelConfigManager.EnemiesToDefeat = totalEnemies[LevelConfigManager.Level-1];
		LevelConfigManager.SpawnRate = spawnRates[LevelConfigManager.Level-1];

		switch (LevelConfigManager.Level)
		{
		case 1:
			//WHITE
			spawnProbabilities = new int[]{ 100, 0, 0, 0, 0, 0, 0, 0, 0 };
			break;
		case 2:
			//YELLOW
			spawnProbabilities = new int[]{ 30, 70, 0, 0, 0, 0, 0, 0, 0 };
			break;
		case 3:
			//ORANGE
			spawnProbabilities = new int[]{ 10, 20, 70, 0, 0, 0, 0, 0, 0 };
			break;
		case 4:
			//GREEN
			spawnProbabilities = new int[]{ 5, 10, 15, 70, 0, 0, 0, 0, 0 };
			break;
		case 5:
			//BLUE
			spawnProbabilities = new int[]{ 0, 5, 10, 15, 70, 0, 0, 0, 0 };
			break;
		case 6:
			//PURPLE
			spawnProbabilities = new int[]{ 0, 10, 10, 15, 15, 50, 0, 0, 0 };
			//spawnProbabilities = new int[]{ 0, 0, 5, 10, 15, 70, 0, 0, 0, 0 };
			break;
		case 7:
			//BROWN
			spawnProbabilities = new int[]{ 0, 0, 10, 10, 15, 15, 50, 0, 0 };
			//spawnProbabilities = new int[]{ 0, 0, 0, 5, 10, 15, 70, 0, 0, 0 };
			break;
		case 8:
			//RED
			spawnProbabilities = new int[]{ 0, 0, 0, 10, 10, 15, 15, 50, 0 };
			break;
		case 9:
			//ALL
			spawnProbabilities = new int[]{ 2, 2, 5, 5, 15, 15, 18, 38, 0 };
			break;
		case 10:
			//BLACK
			spawnProbabilities = new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 100 };
			break;
		default:
			spawnProbabilities = new int[]{ 100, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
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


	void spawnEnemyPrefab(int index){
		Vector3 spawnRightLocation = new Vector3(13.0f, -1.15f, 0);
		Vector3 spawnLeftLocation = new Vector3 (-13.0f, -1.15f, 0);
		Vector3 spawnLocation = Random.Range (0, 2) < 1 ? spawnRightLocation : spawnLeftLocation;
		switch (index) {
			case 0:
				GameObject enemy = (GameObject)Instantiate(WhiteDogurai, spawnLocation, new Quaternion());
				DemoEnemyController enemyController = enemy.GetComponent("DemoEnemyController") as DemoEnemyController;
				enemyController.target = Player;
				break;
			case 1:
			 	enemy = (GameObject)Instantiate(YellowDogurai, spawnLocation, new Quaternion());
			 	enemyController = enemy.GetComponent("DemoEnemyController") as DemoEnemyController;
			    enemyController.target = Player;
				break;
			case 2:
				enemy = (GameObject)Instantiate(OrangeDogurai, spawnLocation, new Quaternion());
			    enemyController = enemy.GetComponent("DemoEnemyController") as DemoEnemyController;
				enemyController.target = Player;
				break;
		case 3:
				GameObject player = GameObject.FindGameObjectWithTag ("Player");
				float xPosition = player.transform.position.x - 2 * LevelConfigManager.looking;
				Vector3 newSpawn = new Vector3 (xPosition, -1.15f, 0);
				enemy = (GameObject)Instantiate(GreenDogurai, newSpawn, new Quaternion());
			    enemyController = enemy.GetComponent("DemoEnemyController") as DemoEnemyController;
			    enemyController.target = Player;
				break;
			case 4:
				enemy = (GameObject)Instantiate(BlueDogurai, spawnLocation, new Quaternion());
				enemyController = enemy.GetComponent("DemoEnemyController") as DemoEnemyController;
				enemyController.target = Player;
				break;
			case 5:
				enemy = (GameObject)Instantiate(PurpleDogurai, spawnLocation, new Quaternion());
				enemyController = enemy.GetComponent("DemoEnemyController") as DemoEnemyController;
				enemyController.target = Player;
				break;
			case 6:
				enemy = (GameObject)Instantiate(BrownDogurai, spawnLocation, new Quaternion());
				enemyController = enemy.GetComponent("DemoEnemyController") as DemoEnemyController;
				enemyController.target = Player;
				break;
			case 7:
				player = GameObject.FindGameObjectWithTag ("Player");
				xPosition = player.transform.position.x - 2 * LevelConfigManager.looking;
				newSpawn = new Vector3 (xPosition, -1.15f, 0);
				enemy = (GameObject)Instantiate(RedDogurai, spawnLocation, new Quaternion());
				enemyController = enemy.GetComponent("DemoEnemyController") as DemoEnemyController;
				enemyController.target = Player;
				break;
			case 8:
				enemy = (GameObject)Instantiate(BlackDogurai, spawnLocation, new Quaternion());
				enemyController = enemy.GetComponent("DemoEnemyController") as DemoEnemyController;
				enemyController.target = Player;
				break;
			default:
				enemy = (GameObject)Instantiate(WhiteDogurai, spawnLocation, new Quaternion());
				enemyController = enemy.GetComponent("DemoEnemyController") as DemoEnemyController;
				enemyController.target = Player;
				break;
		}
		LevelConfigManager.EnemiesSpawned += 1;
	}


    void spawnEnemy()
    {
		LoadedDie loadedDie = new LoadedDie (spawnProbabilities);
		spawnEnemyPrefab (loadedDie.NextValue ());
    }
}











public class LoadedDie {
	// Initializes a new loaded die.  Probs
	// is an array of numbers indicating the relative
	// probability of each choice relative to all the
	// others.  For example, if probs is [3,4,2], then
	// the chances are 3/9, 4/9, and 2/9, since the probabilities
	// add up to 9.
	public LoadedDie(int probs){
		this.prob=new List<long>();
		this.alias=new List<int>();
		this.total=0;
		this.n=probs;
		this.even=true;
	}

	Random random=new Random();

	List<long> prob;
	List<int> alias;
	long total;
	int n;
	bool even;

	public LoadedDie(IEnumerable<int> probs){
		// Raise an error if nil
		this.prob=new List<long>();
		this.alias=new List<int>();
		this.total=0;
		this.even=false;
		var small=new List<int>();
		var large=new List<int>();
		var tmpprobs=new List<long>();
		foreach(var p in probs){
			tmpprobs.Add(p);
		}
		this.n=tmpprobs.Count;
		// Get the max and min choice and calculate total
		long mx=-1, mn=-1;
		foreach(var p in tmpprobs){			
			mx=(mx<0 || p>mx) ? p : mx;
			mn=(mn<0 || p<mn) ? p : mn;
			this.total+=p;
		}
		// We use a shortcut if all probabilities are equal
		if(mx==mn){
			this.even=true;
			return;
		}
		// Clone the probabilities and scale them by
		// the number of probabilities
		for(var i=0;i<tmpprobs.Count;i++){
			tmpprobs[i]*=this.n;
			this.alias.Add(0);
			this.prob.Add(0);
		}
		// Use Michael Vose's alias method
		for(var i=0;i<tmpprobs.Count;i++){
			if(tmpprobs[i]<this.total)
				small.Add(i); // Smaller than probability sum
			else
				large.Add(i); // Probability sum or greater
		}
		// Calculate probabilities and aliases
		while(small.Count>0 && large.Count>0){
			var l=small[small.Count-1];small.RemoveAt(small.Count-1);
			var g=large[large.Count-1];large.RemoveAt(large.Count-1);
			this.prob[l]=tmpprobs[l];
			this.alias[l]=g;
			var newprob=(tmpprobs[g]+tmpprobs[l])-this.total;
			tmpprobs[g]=newprob;
			if(newprob<this.total)
				small.Add(g);
			else
				large.Add(g);
		}
		foreach(var g in large)
			this.prob[g]=this.total;
		foreach(var l in small)
			this.prob[l]=this.total;
	}

	// Returns the number of choices.
	public int Count {
		get {
			return this.n;
		}
	}
	// Chooses a choice at random, ranging from 0 to the number of choices
	// minus 1.
	public int NextValue(){
		var i=Random.Range(0, this.n);
		return (this.even || Random.Range(0, (int)this.total)<this.prob[i]) ? i : this.alias[i];
	}
}