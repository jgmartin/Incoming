using UnityEngine;
using System.Collections;

public class StructureHealth : MonoBehaviour {

	public int health;

	void FixedUpdate() {
		if (health <= 0) {
			Destroy (gameObject);
		}
	}

	public void TakeDamage(int damage) {
		health -= damage;
	}
}
