using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	public List<GameObject> enemies;
	public List<GameObject> defenseEntities;

	public delegate void GameStatus(int time, int frogs);
	public static GameStatus OnGameOver;

	public GameObject gameOverPanel;

	int totalTime;
	bool gameOver;

	// Use before initialization
	void Awake(){

		Instance = this;
		enemies = new List<GameObject> ();
		defenseEntities = new List<GameObject> ();
	}

	// Use this for initialization
	void Start () {
		totalTime = 0;
		gameOver = false;
		StartCoroutine (CheckGameOver());
		StartCoroutine (SaveTime());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator SaveTime(){
		while (!gameOver) {
			yield return new WaitForSeconds (1.0f);
			totalTime++;
		}
	}

	IEnumerator CheckGameOver(){
		while (EnemyController.aliveEntity == null || EnemyController.aliveEntity.Length == 0) {
			yield return null;
		}

		for (int i = 0; i < EnemyController.aliveEntity.Length; i++) {
			if (EnemyController.aliveEntity [i]) {
				yield return null;
				i = -1;
			} 
		}

		SetGameOver ();
	}

	void SetGameOver(){

		gameOver = true;
		gameOverPanel.SetActive (true);
		if (OnGameOver!=null)
			OnGameOver(totalTime,Power1Controller.killedFrogs);
	}
		
}
