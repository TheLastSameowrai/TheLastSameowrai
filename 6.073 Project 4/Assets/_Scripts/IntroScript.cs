using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour {


	public GameObject canvas;
	public float speed;
	Vector3 start_position;
	Vector3 target_position;
	float t;
	float total_time;

	float start_time;
	// Use this for initialization
	void Start () {
		start_time = Time.time;
		start_position = new Vector3(0f, -7.5f, 90f);
		target_position = new Vector3(0f, 7.5f, 90f);
		t = 0;
		total_time = 15.0f;
	}

	// Update is called once per frame
	void Update () {
		if (Time.time - start_time > 2.0f) {
			t += Time.deltaTime / total_time;
			canvas.transform.position = Vector3.Lerp(start_position, target_position, t);
		}
	}

	public void ToInstructionsScene() {
		SceneManager.LoadScene ("InstructionsScene");
	}

	public void toStartScene() {
		SceneManager.LoadScene ("TitleScene");
	}
}
