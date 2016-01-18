using UnityEngine;
using System.Collections;

public class AsteroidExplosion : MonoBehaviour {

	public Transform explosionPrefab;
	public AudioClip audioClip;
	public int points;
	public int damage;

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Ground") {
			AddScore ();
			Explode (collision);
		} else if (collision.gameObject.tag == "Structure") {
			AddScore ();
			AddDamage (collision);
			Explode (collision);
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
			playerScore.AddPoints (points);
		}
	}

	void AddDamage(Collision collision) {
		StructureHealth structureHealth = collision.gameObject.GetComponent<StructureHealth> ();
		structureHealth.TakeDamage (damage);
	}
}
