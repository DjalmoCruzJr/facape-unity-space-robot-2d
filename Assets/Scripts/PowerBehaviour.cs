using UnityEngine;
using System.Collections;

public class PowerBehaviour : MonoBehaviour {
	
	public bool right = true;
	public float speed = 1;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(right)
			transform.Translate(Vector3.right*speed*Time.deltaTime);
		else
			transform.Translate(Vector3.left*speed*Time.deltaTime);
	
	}
	
	 void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
