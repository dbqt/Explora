using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public int nbOfScenes = 1;

	public bool readyToLoad;

	private AsyncOperation currentOperation;
	private int currentLevel;
	

	// Undestroyable singleton !
	void Awake () {
		if(instance != null) {
			Destroy(this.gameObject);
		}
		else {
			instance = this;
			DontDestroyOnLoad(this.gameObject);
			currentLevel = 0;
		}
		readyToLoad = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(currentOperation != null) {
			if(currentOperation.isDone) {
				readyToLoad = true;
			}
		}

		if(Input.GetButtonDown("Jump")) {
			LoadNextLevel();
		}
	}

	public void LoadNextLevel(){
		if(!readyToLoad) return;
		readyToLoad = false;
		// clean up level
		var level = GameObject.Find("Level");
		float delay = level.GetComponent<LevelManager>().CleanLevel();

		//start actual load after clean up
		Invoke("StartLoad", delay);
	}

	private void StartLoad()
	{
		// increment level count to load
		currentLevel = (currentLevel+1) % nbOfScenes;
		LoadNewLevel(currentLevel);
	}

	private void LoadNewLevel(int index){
		currentOperation = SceneManager.LoadSceneAsync(index);
	}

	private void LoadNewLevel(string name){
		currentOperation = SceneManager.LoadSceneAsync(name);
	}
}
