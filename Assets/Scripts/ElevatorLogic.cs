using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorLogic : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player") {
			OpenDoors();
		}
	}

	void OnTriggerExit(Collider other) {
		if(other.gameObject.tag == "Player") {
			CloseDoors();
			GameManager.instance.readyToLoad = true;
		}
	}

	public void OpenDoors() {
		this.gameObject.GetComponent<Animator>().SetBool("isElevatorOpen", true);
	}

	public void CloseDoors() {
		this.gameObject.GetComponent<Animator>().SetBool("isElevatorOpen", false);
	}
}
