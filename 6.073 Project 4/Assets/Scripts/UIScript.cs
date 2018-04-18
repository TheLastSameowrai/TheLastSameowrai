using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {


    public Text Timer;
    float start_time;

    // Use this for initialization
    void Start () {
        start_time = Time.time;
        Timer.text = "00:00";
	}
	
	// Update is called once per frame
	void Update () {
        float elapsedTime = Time.time - start_time;
        int minutes = ((int)elapsedTime) / 60;
        Timer.text = minutes.ToString("D2") + ":" + ((int)(elapsedTime) - 60 * minutes).ToString("D2");
    }
}
