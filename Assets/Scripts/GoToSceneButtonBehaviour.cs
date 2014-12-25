using UnityEngine;
using System.Collections;

public class GoToSceneButtonBehaviour : MonoBehaviour {
	
	public string nameScene;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnClick(){
		Application.LoadLevel(nameScene);
	}
}
