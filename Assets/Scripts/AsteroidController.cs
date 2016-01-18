using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AsteroidController : MonoBehaviour {

	public GameObject[] asteroids;
	public Text asteroidText;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public float waves;
	public float force;

	float waveCount;

	public void Spawn ()
	{
		StartCoroutine (SpawnWaves ());
	}

	IEnumerator SpawnWaves ()
	{
		LevelController levelController = GameObject.FindWithTag ("GameController").GetComponent<LevelController> ();
		levelController.controlDisabled = false;
		waveCount = 0f;
		for (int i = 0; i < startWait; i++) {
			int timeRemaining = (int)startWait - i;
			asteroidText.text = timeRemaining.ToString();
			yield return new WaitForSeconds (1f);
		}
		asteroidText.text = "";
		levelController.controlDisabled = true;
		while (waveCount < waves)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				SpawnAsteroid ();
				float randomWait = Random.Range (0.1f, spawnWait);
				yield return new WaitForSeconds (randomWait);
			}
			waveCount++;
			yield return new WaitForSeconds (waveWait);
		}
	}

	void SpawnAsteroid() {
		GameObject asteroid;
		int randomType = Random.Range (0, 10);
		if (asteroids.Length > 1 && randomType == 0) {
			asteroid = asteroids [1];
		} else {
			asteroid = asteroids [0];
		}
		Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
		Quaternion spawnRotation = RandomRotation();
		GameObject asteroidInstance = Instantiate (asteroid, spawnPosition, spawnRotation) as GameObject;
		RandomForce (asteroidInstance);
	}

	Quaternion RandomRotation() {
		float xRotation = Random.Range(0,360);
		float yRotation = Random.Range(0,360);
		float zRotation = Random.Range(0,360);
		return Quaternion.Euler(xRotation, yRotation, zRotation);
	}

	void RandomForce(GameObject asteroidInstance) {
		asteroidInstance.GetComponent<Rigidbody>().AddForce (asteroidInstance.transform.forward * force);
	}

	public bool Finished(){
		return waveCount >= waves && AllAsteroidsDestroyed();
	}

	private bool AllAsteroidsDestroyed() {
		return GameObject.FindWithTag ("Asteroid") == null;
	}
}
