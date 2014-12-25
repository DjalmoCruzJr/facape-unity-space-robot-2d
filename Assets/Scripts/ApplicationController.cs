using UnityEngine;
using System.Collections;

public class ApplicationController : MonoBehaviour {
	
	private static int currentLevel = 1;
	public static int totalLevels = 10;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public static void UnlockLevel(int numberLevel){
		if(numberLevel <= totalLevels){
			PlayerPrefs.SetInt("Level"+numberLevel, 1);
		}
	}
	
	public static bool isUnlocked(int numberLevel){
		if(PlayerPrefs.GetInt("Level"+numberLevel) == 1)
			return true;
		
		return false;
	
	}
	
	public static void SetCurrentLevel(int levelToSet){
	 	currentLevel = levelToSet;
	}
	
	public static void Reset(){
		Application.LoadLevel("Level"+currentLevel);
		
	}
	
	public static int GetCurrentLevel(){
		return currentLevel;
	}
}
