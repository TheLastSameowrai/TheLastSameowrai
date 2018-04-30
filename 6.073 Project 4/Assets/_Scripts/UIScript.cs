using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour {


    public Text timer;
	public GameObject proceedButton;
	public GameObject[] pauseObjects;

    //float startTime;
    //public bool gameOver;

	private static UIScript instance = null;

	public static UIScript Instance {
		get { return instance; }
	}

	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

    // Use this for initialization
    void Start () {
		print ("In UISCRIPT start");
		//DontDestroyOnLoad (transform.gameObject);
		proceedButton.SetActive (false);
		Time.timeScale = 1;
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		hidePaused();
		if (LevelConfigManager.FirstTime) {
			LevelConfigManager.StartTime = Time.time; //startTime = Time.time;
			LevelConfigManager.Timer = timer;
			LevelConfigManager.Timer.text = "00:00"; // Timer.text = "00:00";
			LevelConfigManager.FirstTime = false;
			LevelConfigManager.dataManager = new Data ();
			LevelConfigManager.dataManager.Start (); // initialize Data
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P))
		{
				if (Time.timeScale == 1)
				{
					Time.timeScale = 0;
					showPaused();
				}
				else if (Time.timeScale == 0)
				{
					Time.timeScale = 1;
					hidePaused();
				}
		}
        if (!LevelConfigManager.GameOver)
        {
			float elapsedTime = Time.time - LevelConfigManager.StartTime;
            int minutes = ((int)elapsedTime) / 60;
			LevelConfigManager.Timer.text = minutes.ToString("D2") + ":" + ((int)(elapsedTime) - 60 * minutes).ToString("D2");
        }

		if (LevelConfigManager.EnemiesDefeated >= LevelConfigManager.EnemiesToDefeat && !LevelConfigManager.GameOver) {
			//print ("---Setting button to true---");
			if (proceedButton != null) {
				proceedButton.SetActive (true);
			}
		}
    }

	public void showPaused() {
		foreach(GameObject g in pauseObjects){
			g.SetActive (true);
		}
	}
		
	public void hidePaused(){
		foreach (GameObject g in pauseObjects) {
			g.SetActive (false);
		}
	}
	
	public void ToNextLevel() {
        LevelConfigManager.dataManager.levelComplete("completed"); //Store Data for level
		LevelConfigManager.Level = LevelConfigManager.Level + 1;
		LevelConfigManager.EnemiesDefeated = 0;
        LevelConfigManager.dataManager.nextLevel(LevelConfigManager.Level.ToString());
		print ("LevelConfigManager.Level is now" + LevelConfigManager.Level);
		//Destroy (proceedButton);
		proceedButton.SetActive (false);
		print ("---Just set the button active to false----");
		SceneManager.LoadScene ("GameScene");
	}

	public void RestartGame() {
		LevelConfigManager.GameOver = false;
		LevelConfigManager.Level = 1;
		LevelConfigManager.Timer.text = "00:00";
		LevelConfigManager.StartTime = Time.time;
		proceedButton.SetActive (false);
		SceneManager.LoadScene ("GameScene");
	}
}
