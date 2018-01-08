using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

	public GameObject gameLoseUI;
	public GameObject gameWinUI;
	public Button winB;
	public Button loseB;

	private bool gameIsOver;
		
	void Start(){
		FindObjectOfType<Player> ().OnReachedEndOfLevel += ShowGameWinUI;
		Guard.OnGuardHasSpottedPlayer += ShowgameLoseUI;

		winB.onClick.AddListener (Restart);
		loseB.onClick.AddListener (Restart);
	}


	void Update () {
		if (gameIsOver) {
			if (Input.GetButtonDown ("Jump")) {
				Restart ();
			}
		}
		
	}

	void ShowGameWinUI(){
		OnGameOver (gameWinUI);
	}

	void ShowgameLoseUI(){
		OnGameOver (gameLoseUI);
	}
		
	void OnGameOver(GameObject gameUI){
		gameUI.SetActive (true);
		gameIsOver = true;
		Guard.OnGuardHasSpottedPlayer -= ShowgameLoseUI;
		FindObjectOfType<Player> ().OnReachedEndOfLevel -= ShowGameWinUI;

	}

	void Restart(){
		SceneManager.LoadScene (0);
	}


}
