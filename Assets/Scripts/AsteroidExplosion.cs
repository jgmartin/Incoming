using UnityEngine;
using System.Collections;

public class AsteroidExplosion : MonoBehaviour {

	public Transform explosionPrefab;
	public AudioClip audioClip;

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag != "Asteroid") {
			Explode (collision);
			AddScore ();
		}
	}

	void Explode(Collision collision) {
		ContactPoint contact = collision.contacts [0];
		Quaternion rot = Quaternion.FromToRotation (Vector3.up, contact.normal);
		Vector3 pos = contact.point;
		Instantiate (explosionPrefab, pos, rot);
		Destroy (gameObject);
		PlayAudio ();
	}

	void PlayAudio() {
		AudioSource.PlayClipAtPoint (audioClip, transform.position);
	}

	void AddScore() {
		if (GameObject.FindWithTag ("Orb") != null) {
			PlayerScore playerScore = GameObject.FindWithTag("Orb").GetComponent<PlayerScore> ();
			playerScore.AddPoints (10);
		}
	}
}
