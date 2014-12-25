using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

	public float speed = 6.2F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
	public float positionDead = -12;
	public int powerTotal = 10;
	private int currentPower;
    private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;
	public int totalLife = 3;
	
	public AnimationClip walk;
	public AnimationClip jump;
	public AnimationClip attack;
	public AnimationClip damage;
	public AnimationClip idle;
	public Animation playerAnimation;
	public float speedWalkAnimation = 1;
	
	private Vector3 rightDirection;
	private Vector3 leftDirection;
	public Transform mesh;
	
	public Transform powerPrefab;
	
	public int totalCoins = 0;
	public int totalCoinsToCatch = 30;
	
	public float powerRate = 0.5f;
	private float currentRateToPower = 0;
	private bool directionPlayerRight = true;
	
	public float timeToInvencibility = 2;
	private float currentTimeToInvencibility = 0;
	public float timeToBlink = 0.05f;
	private float currentTimeToBlink = 0;
	private bool invencibility = false;
	public Renderer rendererPlayer;
	
	public int powerRecharge = 5;
	
	//HUD//
	public Renderer[] hearts;
	public TextMesh currentGoldText;
	public TextMesh goldToCathText;
	public Transform powerBar;
	private Vector3 startSize;
	//HUD//
	

	
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		rightDirection = mesh.localScale;
		leftDirection = mesh.localScale;
		leftDirection.z *= -1;
		
		currentRateToPower = powerRate;
		currentPower = powerTotal;
		
		startSize = powerBar.transform.localScale;
	
	}
	
	void RefreshHUD(){
		for(int i = 0; i<hearts.Length; i++){
			if(totalLife >= i+1){
				hearts[i].enabled = true;
			}
			else{
				hearts[i].enabled = false;
			}
		}
		
		currentGoldText.text = totalCoins.ToString();
		goldToCathText.text = "/ "+totalCoinsToCatch;
		
		Vector3 newSize = startSize;
		newSize.x = startSize.x*currentPower/powerTotal;
		powerBar.localScale = newSize;
	
				
	}
	
    void Update() {
		
		
		//HUD//
		RefreshHUD();	
		//HUD//
		
		
        moveDirection.x = Input.GetAxis("Horizontal")*speed;
        if (controller.isGrounded) {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump")){
                moveDirection.y = jumpSpeed;
				playerAnimation.clip = jump;
				
			}

        }
		else{
			if(playerAnimation.animation != jump){
				
				playerAnimation.Play();
			}
		}
		
		if(moveDirection == Vector3.zero){
			playerAnimation.CrossFade(idle.name);
		}
		
		playerAnimation[walk.name].speed = speedWalkAnimation;
		
		if(moveDirection.x > 0){
			mesh.localScale = rightDirection;
			playerAnimation.CrossFade(walk.name);
			directionPlayerRight = true;
			
		}
		else if(moveDirection.x < 0){
			mesh.localScale = leftDirection;
			playerAnimation.CrossFade(walk.name);
			directionPlayerRight = false;
		}
		

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
		
		if(transform.position.y <= positionDead){
			CallGameOver();
		}
		
		currentRateToPower += Time.deltaTime;
		
		if(Input.GetKeyDown(KeyCode.Z)){
			if(currentRateToPower >= powerRate && currentPower > 0){
				currentRateToPower = 0;
				GameObject projectil = Instantiate(powerPrefab.gameObject, transform.position, transform.rotation) as GameObject;
				projectil.GetComponent<PowerBehaviour>().right = directionPlayerRight;
				playerAnimation.Play(attack.name);
				currentPower--;
			}
		}
		
		if(invencibility){
			
			currentTimeToBlink += Time.deltaTime;
			
			if(currentTimeToBlink > timeToBlink){
				currentTimeToBlink = 0;
				rendererPlayer.enabled = !rendererPlayer.enabled;
			}
			
			
			currentTimeToInvencibility += Time.deltaTime;
			if(currentTimeToInvencibility > timeToInvencibility){
				invencibility = false;
				rendererPlayer.enabled = true;
				currentTimeToInvencibility = 0;
			}
		}
		
		
		
	
    }
	

	
	void OnTriggerEnter(Collider hit){
		if(hit.transform.tag == "Gold"){
			totalCoins++;
			Destroy(hit.gameObject);
		}
		
		if(hit.transform.tag == "Enemy"){
			ApplyDamage();
		}
		
		if(hit.transform.tag == "PowerUp" && currentPower < powerTotal){
			currentPower += powerRecharge;
			if(currentPower > powerTotal)
				currentPower = 10;
			Destroy(hit.gameObject);
		}
		
		if(hit.transform.tag == "FinalLevel"){
			if(totalCoins >= totalCoinsToCatch){
				ApplicationController.UnlockLevel(ApplicationController.GetCurrentLevel()+1);
				Application.LoadLevel("SelectLevel");
			}
		}
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit) {
       if(hit.transform.tag == "Enemy"){
			ApplyDamage();
		}
    }
	
	void ApplyDamage(){
		if(!invencibility){
			totalLife--;
			invencibility = true;
			if(totalLife == 0){
				CallGameOver();
			}
		}
	}
	

	
	void CallGameOver(){
		Application.LoadLevel("GameOver");
	}

	
}
