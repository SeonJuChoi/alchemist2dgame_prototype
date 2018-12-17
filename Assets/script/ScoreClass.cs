using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreClass : MonoBehaviour {

	static int score = 0;
	static Text scoreText;
	private GUIStyle guiStyle = new GUIStyle();

	void Start () {
		scoreText = gameObject.GetComponent<Text>();
	}
	// <-- set Score Method
	public static void setScore(int value) {
		score += value;
	}

	// <-- add Score Method
	public static void addScore() {
		// scoreText.text = "Score - " + score.ToString();
	}

	void OnGUI() {
		GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));

		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();

		guiStyle.fontSize = 15;
		guiStyle.normal.textColor = Color.white;
		GUILayout.Label("Score - " + score.ToString(), guiStyle);
		
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		GUILayout.EndArea();
	}
}
