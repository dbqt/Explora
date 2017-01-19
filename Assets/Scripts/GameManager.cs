using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public ElevatorLogic elevator;
	public int nbOfScenes = 1;

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

	}
	
	// Update is called once per frame
	void Update () {
		if(currentOperation != null) {
			if(currentOperation.isDone) {
				currentOperation = null;
				elevator.OpenDoors();
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log("collide");
		if(other.gameObject.tag == "Player") {
			Debug.Log("Loading next level");
			LoadNextLevel();
		}
	}

	private void LoadNextLevel(){
		currentLevel = (currentLevel+1) % nbOfScenes;
		LoadNewLevel(currentLevel);
	}

	private void LoadNewLevel(int index){
		elevator.CloseDoors();
		currentOperation = SceneManager.LoadSceneAsync(index);
	}

	private void LoadNewLevel(string name){
		elevator.CloseDoors();
		currentOperation = SceneManager.LoadSceneAsync(name);
	}
}
