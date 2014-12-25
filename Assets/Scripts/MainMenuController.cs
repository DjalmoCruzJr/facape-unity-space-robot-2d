using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void GoToSelectLevel(){
		Application.LoadLevel("SelectLevel");
	}
	public void Exit(){
		Application.Quit();
	}
	public void PlayGame(){
		Application.LoadLevel("Level1");
	}
	
}
