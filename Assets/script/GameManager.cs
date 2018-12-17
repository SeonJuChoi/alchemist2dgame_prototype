using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject player;

	Vector3 startPos;
	Vector3 goalPos;
	Quaternion startRotate;
	bool isStart = false;
	static bool isEnd = false;

	// Use this for initialization
	void Start () {
		startPos = GameObject.FindGameObjectWithTag("start").transform.position;
		goalPos = GameObject.FindGameObjectWithTag("goal").transform.position;
		Debug.Log(startPos.ToString());
		Debug.Log(goalPos.ToString());
	}

	void OnGUI() {

		if(!isStart) {
		GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));

		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();

		GUILayout.BeginVertical();
		GUILayout.FlexibleSpace();

		GUILayout.Label("Are you Ready ? : D");
		if(GUILayout.Button("Game Start!")) {
			isStart = true;

			StartGame();
		}


		GUILayout.FlexibleSpace();
		GUILayout.EndVertical();
		
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		GUILayout.EndArea();
		}
		else if(isEnd) {
			GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));

		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();

		GUILayout.BeginVertical();
		GUILayout.FlexibleSpace();

		GUILayout.Label("End");
		if(GUILayout.Button("Re Start?")) {
			SceneManager.LoadScene("Main", LoadSceneMode.Single);
			isEnd = false;
		}


		GUILayout.FlexibleSpace();
		GUILayout.EndVertical();
		
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		GUILayout.EndArea();
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Awake()
	{
		Time.timeScale = 0f;
	}

	void StartGame() {
		Time.timeScale = 1f;

		GameObject standingCamera = GameObject.FindGameObjectWithTag("MainCamera");
		standingCamera.SetActive(false);

		startPos = new Vector3(startPos.x, startPos.y+2f, startPos.z);
		Instantiate(player, startPos, startRotate);

	}

	public static void EndGame(){
		//Stop Game
		Time.timeScale = 0f;

		// GUI Output
		isEnd = true;
	}

	public static void RestartStage() {
		Time.timeScale = 0f;

		SceneManager.LoadScene("Main", LoadSceneMode.Single);
	}
}
