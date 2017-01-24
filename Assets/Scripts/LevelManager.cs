using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public float CleanUpTime;
	public Animator anim;

	void Start(){
	}

	public float CleanLevel(){
		anim.SetTrigger("Clean");
		return CleanUpTime;
	}
}
