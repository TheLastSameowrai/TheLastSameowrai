using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour {


    public Text timer;
	public GameObject proceedButton;

    //float startTime;
    //public bool gameOver;

    // Use this for initialization
    void Start () {
		print ("In UISCRIPT start");
		DontDestroyOnLoad (transform.gameObject);
		proceedButton.SetActive (false);

		if (LevelConfigManager.FirstTime) {
			LevelConfigManager.StartTime = Time.time; //startTime = Time.time;
			LevelConfigManager.Timer = timer;
			LevelConfigManager.Timer.text = "00:00"; // Timer.text = "00:00";
			LevelConfigManager.FirstTime = false;
		}
	}

	// Update is called once per frame
	void Update () {
        if (!LevelConfigManager.GameOver)
        {
			float elapsedTime = Time.time - LevelConfigManager.StartTime;
            int minutes = ((int)elapsedTime) / 60;
			LevelConfigManager.Timer.text = minutes.ToString("D2") + ":" + ((int)(elapsedTime) - 60 * minutes).ToString("D2");
        }

		if (LevelConfigManager.EnemiesDefeated >= LevelConfigManager.EnemiesToDefeat && !LevelConfigManager.GameOver) {
			proceedButton.SetActive (true);
			LevelConfigManager.Level = LevelConfigManager.Level + 1;
		}
    }

	public void ToNextLevel() {
		SceneManager.LoadScene ("GameScene");
	}
}
