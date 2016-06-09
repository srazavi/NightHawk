using UnityEngine;
using System.Collections;

public class Airplane : MonoBehaviour {
	
	Camera frontCamera;
	Camera rightCamera;
	Camera leftCamera;
	Camera mainCamera;

	bool accelerating = false;
	bool decelerating = false;

	private Rigidbody rigid;
	public Rigidbody Missile;
	public GameObject launchPoint;

	private Vector3 missileOffset;
	private Vector3 missilePos;
	public ParticleSystem exp;

	private float moveSpeed = 100f;
	public int health;

	private Rigidbody clone;
	//private Menu win;

	void Start()
	{
		frontCamera = (Camera)GameObject.Find("Front Camera").GetComponent<Camera>();
		rightCamera = (Camera)GameObject.Find("Right Camera").GetComponent<Camera>();
		leftCamera = (Camera)GameObject.Find("Left Camera").GetComponent<Camera>();
		mainCamera = (Camera)GameObject.Find("Main Camera").GetComponent<Camera>();
		health = 100;

		rigid = GetComponent<Rigidbody>();
		//win = (Menu)GameObject.Find ("WIN").GetComponent<Menu>();

		this.transform.Find ("CG").GetComponent<Transform> ().localScale = new Vector3 (0.7f, (float)getHealth()/100f*5f, 0.7f);
	}

	void Update()
	{
		mainCamera.enabled = true;
		frontCamera.enabled = false;
		leftCamera.enabled = false;
		rightCamera.enabled = false;
		/*Camera Control*/


		if (Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.Keypad5))
		{
			mainCamera.enabled = false;
			leftCamera.enabled = false;
			rightCamera.enabled = false;

			frontCamera.enabled = true;
		}
		if (Input.GetKey(KeyCode.Keypad4))
		{
			mainCamera.enabled = false;
			frontCamera.enabled = false;
			rightCamera.enabled = false;

			leftCamera.enabled = true;
		}
		if (Input.GetKey(KeyCode.Keypad6))
		{
			mainCamera.enabled = false;
			frontCamera.enabled = false;
			leftCamera.enabled = false;

			rightCamera.enabled = true;
		}
		this.transform.Find ("CG").GetComponent<Transform> ().localScale = new Vector3 (0.7f, (float)getHealth()/100f*5f, 0.7f);

	}


	public void releaseMissile (){
		clone = (Rigidbody) Instantiate(Missile, launchPoint.transform.position, transform.rotation);
		Physics.IgnoreCollision (clone.GetComponent<Collider>(), GetComponent<Collider>());
	}
	public void steerPlane (float[] commands ){
		rigid.rotation *= flightPhysics.getNewState(getDirection(), commands, Time.deltaTime);

		Vector3 forth = Vector3.forward;
		forth = rigid.rotation * forth;
		rigid.velocity = forth * (Time.deltaTime * moveSpeed);
	}
	public Vector3 getPosition (){
		return transform.position;
	}
	public Vector3 getForward(){
		return transform.forward;
	}
	public Vector3 getDirection (){
		return transform.eulerAngles;
	}
	public float getSpeed (){
		return moveSpeed;
	}
	public void setSpeed(float s){
		if (s > 0) {
			
			moveSpeed = flightPhysics.accelerate (moveSpeed);
			
		} else if (s < 0) {
			
			moveSpeed = flightPhysics.decelerate (moveSpeed);
		}
	}

	public int getHealth(){
		return health;
	}
	public void setHealth(int h){
		health = h;
	}

	public void camout(){
		if (!accelerating && mainCamera.fieldOfView != 80 && moveSpeed < flightPhysics.getMaxSpeed())
		{
			StartCoroutine(cameraFadeOut());
		}
	}
	public void camin(){
		if (!decelerating && mainCamera.fieldOfView != 40 && moveSpeed > flightPhysics.getMinSpeed())
		{
			StartCoroutine(cameraFadeIn());
		}
	}
	public void camstab(){
		StartCoroutine(cameraStabilize());
	}
	IEnumerator cameraStabilize()
	{
		while (mainCamera.fieldOfView != 60)
		{
			if (mainCamera.fieldOfView > 60)
			{
				mainCamera.fieldOfView -= 1;
			}
			else {
				mainCamera.fieldOfView += 1;
			}
			yield return new WaitForFixedUpdate();
		}

	}

	IEnumerator cameraFadeOut()
	{
		for (float i = 60f; i <= 80f; i += 2f)
		{
			accelerating = true;
			mainCamera.fieldOfView = i;
			yield return new WaitForFixedUpdate();
			accelerating = false;
		}
	}

	IEnumerator cameraFadeIn()
	{
		for (float i = 60f; i >= 40f; i -= 2f)
		{
			decelerating = true;
			mainCamera.fieldOfView = i;
			yield return new WaitForFixedUpdate();
			decelerating = false;
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		//print ("Collision occurs");
		if (other.gameObject.CompareTag("Missile"))
		{
			if (health <=5) {
				controller cnt;
				cnt = (controller)GameObject.Find ("TerrainGenerator").GetComponent<controller> ();
				exp.Play ();
				MeshRenderer rend = GetComponent<MeshRenderer> ();
				transform.Find ("CR").GetComponent<MeshRenderer> ().enabled = false;
				transform.Find ("CG").GetComponent<MeshRenderer> ().enabled = false;
				rend.enabled = false;

				//win.IsOpen = true;
				GetComponent<Airplane> ().enabled = false;
				cnt.iLost (this);

				//Destroy(gameObject, exp.duration);
			} else {
				//print ("aiee");
				setHealth (getHealth () - 5);

				this.transform.Find ("CG").GetComponent<Transform> ().localScale = new Vector3 (0.7f, (float)getHealth()/100f*5f, 0.7f);

			}

		}
		/*
		if (other.gameObject.CompareTag ("FirstAid")) {
			if (getHealth () <= 75) {
				setHealth (getHealth () + 25);
			} else {
				setHealth (100);
			}
		}
		*/
		if (other.gameObject.CompareTag ("Terrain")) {
			controller cnt;
			cnt = (controller)GameObject.Find ("TerrainGenerator").GetComponent<controller> ();
			exp.Play ();
			MeshRenderer rend = GetComponent<MeshRenderer> ();
			transform.Find ("CR").GetComponent<MeshRenderer> ().enabled = false;
			transform.Find ("CG").GetComponent<MeshRenderer> ().enabled = false;
			rend.enabled = false;
			setHealth (00);
			//win.IsOpen = true;
			GetComponent<Airplane> ().enabled = false;
			cnt.iLost (this);
		}

	}

	public void setPos(Vector3 pos){
		transform.position = pos;
	}

	public void setDir(Vector3 dir){
		transform.eulerAngles = dir;
	}

	public void setSpd(float spd){
		moveSpeed = spd;
	}

	public void setHlt(int hlt){
		health = hlt;
	}
}
