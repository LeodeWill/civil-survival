using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	public static GameManager Instance = null;                //Static instance of GameManager which allows it to be accessed by any other script.
	public GameObject p1,p2;
	public bool soloGame = false;
	public bool gameEnded = false;


	//Awake is always called before any Start functions
	void Awake()
	{
		//Check if instance already exists
		if (Instance == null){
			//if not, set instance to this
			Instance = this;
		}

		//If instance already exists and it's not this:
		else if (Instance != this){
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);
		}

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);
		SceneManager.sceneLoaded += OnSceneLoaded;

		//Call the InitGame function to initialize the first level
		InitGame();
	}

	void InitGame(){
		p1 = GameObject.Find("P1");
		p2 = GameObject.Find("P2");

		gameEnded = false;
		if(p2 == null){
			// TODO: SOLO GAME
			soloGame = true;
			UiManager.Instance.InitLife(0,false);
		}

	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Game"){
			InitGame();
		}
    }

	public void GameOver(bool isP1){
		gameEnded = true;
		UiManager.Instance.GameOver(isP1);
	}

}