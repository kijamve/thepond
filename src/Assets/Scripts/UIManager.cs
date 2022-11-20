using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Text totalFrogs;
	public Text finishTime;

	// Use this for initialization
	void Start () {
		GameManager.OnGameOver += OnGameOver;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGameOver(int time, int frogs){
        InputManager input = FindObjectOfType<InputManager>();
        input.enabled = false;

		totalFrogs.text = frogs.ToString ();
		finishTime.text = time.ToString ();
	}

	public void OnPlay(){
		SceneManager.LoadScene ("Pond");
	}

	public void OnExit(){
		Application.Quit ();
	}
}
