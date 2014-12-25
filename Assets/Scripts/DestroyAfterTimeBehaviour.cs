using UnityEngine;
using System.Collections;

public class DestroyAfterTimeBehaviour : MonoBehaviour {
	
	public float timeToDestroy = 2;
	private float currentTimeToDestroy = 0;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		currentTimeToDestroy += Time.deltaTime;
		if(currentTimeToDestroy >= timeToDestroy){
			Destroy(gameObject);
		}
	
	}
}
