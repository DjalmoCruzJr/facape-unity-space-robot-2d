using UnityEngine;
using System.Collections;

public class SelectLevelButtonBehaviour : MonoBehaviour {
	
	public TextMesh numberLevel;
	private bool isUnlocked;
	public int levelToGo;
	public Texture boxUnlocked;
	public Texture boxLocked;
	// Use this for initialization
	void Start () {
		
		isUnlocked = ApplicationController.isUnlocked(levelToGo);
		
		if(isUnlocked || levelToGo==1){
			renderer.material.mainTexture = boxUnlocked;		
		}
		else{
			renderer.material.mainTexture = boxLocked;
			numberLevel.renderer.enabled = false;
		}
		
		numberLevel.text = levelToGo.ToString();
		
	
	}
	
	void OnClick(){
		if(isUnlocked || levelToGo==1){
			ApplicationController.SetCurrentLevel(levelToGo);
			Application.LoadLevel("Level"+levelToGo);
		}
	}

}
