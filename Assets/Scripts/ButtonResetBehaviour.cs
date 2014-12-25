using UnityEngine;
using System.Collections;

public class ButtonResetBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnClick(){
		ApplicationController.Reset();
	}
}
