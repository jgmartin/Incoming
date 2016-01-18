using UnityEngine;
using System.Collections;

public class OrbExplosion : MonoBehaviour {

	public Transform explosionPrefab;
	public AudioClip audioClip;

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag != "Ground" && collision.gameObject.tag != "Structure") {
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

}