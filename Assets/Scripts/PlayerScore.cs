using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScore : MonoBehaviour {

	public Text scoreText;

	public static int score = 0;
	
	// Update is called once per frame
	void Update () {
		scoreText.text = PlayerScore.score.ToString();
	}

	public void AddPoints(int points) {
		PlayerScore.score += points;
	}
}
