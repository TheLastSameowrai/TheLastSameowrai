﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour {


    public Text timer;
	public Slider healthBar;
	public GameObject transitionPopup;
	public Text transitionText;
	public GameObject proceedButton;
	public GameObject[] pauseObjects;
	public GameObject instructions;
	public GameObject pavilionDay;
	public GameObject pavilion;
	public GameObject dojo;
	public GameObject garden;
	public GameObject final;

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
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		hidePaused();
		if (LevelConfigManager.FirstTime) {
			LevelConfigManager.StartTime = Time.time; //startTime = Time.time;
			LevelConfigManager.Timer = timer;
			LevelConfigManager.Timer.text = "00:00"; // Timer.text = "00:00";
			LevelConfigManager.FirstTime = false;
			LevelConfigManager.dataManager = new Data ();
			LevelConfigManager.playerHealth = 5;
			LevelConfigManager.dataManager.Start (); // initialize Data
		}
		setLevelText ();
		setBackground ();
		transitionPopup.SetActive (true);
		Time.timeScale = 0;
		LevelConfigManager.Paused = true;
		instructions.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		if (transitionPopup.gameObject.active && Input.GetKeyDown (KeyCode.C)) {
			Time.timeScale = 1;
			transitionPopup.gameObject.SetActive (false);
			LevelConfigManager.Paused = false;
			instructions.SetActive (true);
		}
		if (!LevelConfigManager.GameOver && Input.GetKeyDown(KeyCode.P))
		{
			if (Time.timeScale == 1)
			{
				showPaused ();
			}
			else if (Time.timeScale == 0)
			{
				hidePaused();
			}
		}

		if (Input.GetKeyDown (KeyCode.R)) 
		{
			RestartGame ();
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

		healthBar.value = LevelConfigManager.playerHealth;
    }

	public void showPaused() {
		Time.timeScale = 0;
		foreach(GameObject g in pauseObjects){
			g.SetActive (true);
		}
		timer.gameObject.SetActive (false);
		LevelConfigManager.Paused = true;
	}
		
	public void hidePaused(){
		Time.timeScale = 1;
		foreach (GameObject g in pauseObjects) {
			g.SetActive (false);
		}
		timer.gameObject.SetActive (true);
		LevelConfigManager.Paused = false;
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
		setLevelText ();
		setBackground ();
		transitionPopup.SetActive (true);
		Time.timeScale = 0;
		LevelConfigManager.Paused = true;
		instructions.SetActive (false);
	}

	void RestartGame() {
		Time.timeScale = 1;
		hidePaused ();
		LevelConfigManager.GameOver = false;
		LevelConfigManager.Level = 1;
		LevelConfigManager.Timer.text = "00:00";
		LevelConfigManager.StartTime = Time.time;
		LevelConfigManager.EnemiesDefeated = 0;
		SceneManager.LoadScene ("GameScene");
		proceedButton.SetActive (false);
		setLevelText ();
		setBackground ();
		transitionPopup.SetActive (true);
		Time.timeScale = 0;
		LevelConfigManager.Paused = true;
		instructions.SetActive (false);
	}

	void setLevelText() {
		switch (LevelConfigManager.Level) {
		case 1:
			transitionText.text = "Hurry! I just saw Dokugawa Doggunate Bork go through that door, but his puppers are in your way. Defeat them with the appropriate stance to move on!";
			break;
		case 2:
			transitionText.text = "Great job, young one! Be careful, these guys are a little stronger than the pups you just defeated.";
			break;
		case 3:
			transitionText.text = "Old Man Meowza would be proud of you. But we can't celebrate yet. These fast puppers are running right at you!";
			break;
		case 4:
			transitionText.text = "Wooooweee! Look at you go! You're growing to be a fine young - oh no! Watch your back!";
			break;
		case 5:
			transitionText.text = "I've heard about these guys. These guys are strong and fast but have slow reflexes. Be careful!";
			break;
		case 6:
			transitionText.text = "You must be tired from all this slaying. But push through young one! These fellas are not just strong but have more endurance as well!";
			break;
		case 7:
			transitionText.text = "Meow meow meow~ Oh, sorry I was busy chasing a butterfly. Watch out for these doggos, they're pretty fast and they think you're their chew toy!";
			break;
		case 8:
			transitionText.text = "We got pretty far in the doge-o didn't we? And by we, I mean you. Wait hold on... why are those bulky guys so fast. Ahh! Behind you!"; 
			break;
		case 9:
			transitionText.text = "Phew, almost gave me cat-iac arrest, but you're almost there! Defeat these minions and bring honor to the clowder!";
			break;
		case 10:
			transitionText.text = "HERE HE IS! The fearful Dokugawa Doggunate Bork!";
			break;
		default:
			transitionText.text = "Level " + LevelConfigManager.Level.ToString();
			break;
		}
		transitionText.text = transitionText.text.ToUpper();
	}

	void setBackground(){
		pavilionDay.SetActive (false);
		pavilion.SetActive (false);
		dojo.SetActive (false);
		garden.SetActive (false);
		final.SetActive (false);

		switch (LevelConfigManager.Level) {
		case 1:
		case 2:
			pavilionDay.SetActive (true);
			break;
		case 3:
		case 4:
			pavilion.SetActive (true);
			break;
		case 5:
		case 6:
			dojo.SetActive (true);
			break;
		case 7:
		case 8:
		case 9:
			garden.SetActive (true);
			break;
		case 10:
			final.SetActive (true);
			break;
		default:
			pavilion.SetActive (true);
			break;
		}
	}
}
