using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	
	
	public Transform meshEnemy;
	public float distanceToWalk = 6;
	public float speedWalk = 1;
	public int totalLife = 2;
	public ParticleSystem explosionPower;
	public float DistanceToAttack;
	public PowerBehaviour powerPrefab;
	private PlayerBehaviour player;
	private float currentRateToPower;
	public float powerRate;
	public bool canAttack;
	public Vector3 offsetPower;
	
	public AnimationClip walkAnimation;
	public AnimationClip attackAnimation;
	public Animation animationEnemy;
	
	
	
	private Vector3 rightDirection;
	private Vector3 leftDirection;
	private Vector3 startPosition;
	private Vector3 finalPosition;
	private bool inLeftDirection = false;
	private bool isBack = true;

	// Use this for initialization
	void Start () {
		
		startPosition = transform.position;
		finalPosition = startPosition;
		finalPosition.x -= distanceToWalk;
		
		leftDirection = transform.localScale;
		rightDirection = leftDirection;
		rightDirection.x *= -1;
		
		player = FindObjectOfType(typeof(PlayerBehaviour)) as PlayerBehaviour;
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(player == null)
			return;
		
		currentRateToPower += Time.deltaTime;
		
		if(inLeftDirection && isBack){
			transform.Translate(Vector3.left * Time.deltaTime * speedWalk);
			transform.localScale = leftDirection;
		}
		else{
			transform.Translate(Vector3.right * Time.deltaTime * speedWalk);
			transform.localScale = rightDirection;
		}
		
		if(transform.position.x > finalPosition.x){
			inLeftDirection = true;
		}
		else {
			inLeftDirection = false;
			isBack = false;
		}
		
		if(transform.position.x > startPosition.x)
			isBack = true;
		
		float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
		
		if(distanceToPlayer <= DistanceToAttack && canAttack)
			Attack();
		
		animationEnemy.CrossFade(walkAnimation.name);
		
	
	}
	
	void OnTriggerEnter(Collider hit){
		if(hit.transform.tag == "Power"){
			totalLife--;
			if(totalLife <= 0){
				Destroy(gameObject);
			}
			Instantiate(explosionPower.gameObject, hit.transform.position, hit.transform.rotation);
			Destroy(hit.gameObject);
		}
	}
	
	void Attack(){
		if(currentRateToPower >= powerRate){
			currentRateToPower = 0;
			GameObject projectil = Instantiate(powerPrefab.gameObject, transform.position+offsetPower, transform.rotation) as GameObject;
			projectil.GetComponent<PowerBehaviour>().right = !isBack;
			animationEnemy.Play(attackAnimation.name);
		}
	}
}
