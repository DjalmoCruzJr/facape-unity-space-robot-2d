using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class GameOverController : MonoBehaviour {
	
	public Vector2 offSet;
	public Vector2 sizeButton;
	
    void OnGUI() {

        if (GUI.Button(new Rect(offSet.x, offSet.y, sizeButton.x, sizeButton.y), "Reiniciar")){
            Application.LoadLevel("GamePlay");
		}
        
    }
}
