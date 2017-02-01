using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerLogic : MonoBehaviour {

	public GameObject ButtonUI;

	private bool inRange;

	// Use this for initialization
	void Start () {
		DesactivateUI();
		inRange = false;
	}
	
	// Update is called once per frame
	void Update () {
        inRange = true;

        if (!inRange || !GameManager.instance.readyToLoad) {
			DesactivateUI();
		} else if (inRange && GameManager.instance.readyToLoad && ButtonUI.activeSelf && Input.GetButtonDown("Activate")) {
			GameManager.instance.LoadNextLevel();
			DesactivateUI();
		} else if (inRange && GameManager.instance.readyToLoad){
			ActivateUI();
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player") {
			inRange = true;
		}
	}

	void OnTriggerExit(Collider other){
		if(other.gameObject.tag == "Player") {
			inRange = false;
		}
	}

	public void ActivateUI(){
		ButtonUI.SetActive(true);
	}

	public void DesactivateUI(){
		ButtonUI.SetActive(false);
	}
}
