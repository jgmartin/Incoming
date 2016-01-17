using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelController : MonoBehaviour {

	public Text uiText;

	void Start(){
		StartCoroutine (GameLoop ());
	}

	IEnumerator GameLoop() {
		yield return StartCoroutine (DisplayStarting ());
		yield return StartCoroutine (DisplayInstructions ());
		yield return StartCoroutine (LevelPlaying());
		yield return StartCoroutine (Restart());

		StartCoroutine (GameLoop ());
	}

	private IEnumerator DisplayStarting() {
		uiText.text = "Protect the orb!";
		yield return new WaitForSeconds(3f);
	}

	private IEnumerator DisplayInstructions() {
		uiText.text = "You have 30 seconds to construct defenses!";
		yield return new WaitForSeconds(3f);
	}

	private IEnumerator LevelPlaying() {
		uiText.text = "";
		AsteroidController asteroidController = GetComponent<AsteroidController> ();
		asteroidController.Spawn ();
		while (OrbIsAlive()) {
			if (asteroidController.Finished ()) {
				uiText.text = "The orb survived!";
				yield return new WaitForSeconds (5f);
				Application.LoadLevel ("Level02");
			}
			yield return null;
		}
	}

	private IEnumerator Restart() {
		uiText.text = "The orb was destroyed!";
		yield return new WaitForSeconds(5f);
		PlayerScore.score = 0;
		Application.LoadLevel ("Level01");
	}

	private bool OrbIsAlive() {
		return GameObject.FindWithTag ("Orb") != null;
	}
		
}
