using UnityEngine;
using System.Collections;

public class Human : MonoBehaviour, IPlayer {
	private string myName = "Human Player";
	public Airplane p;
	private bool isMain = true;
	private Menu m;
	bool paused = false;


	void Start()
	{
		m = (Menu)GameObject.Find ("SettingsMenu").GetComponent<Menu>();
	}

	bool togglePause()
	{
		if(Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
			return(false);
		}
		else
		{
			Time.timeScale = 0f;
			return(true);    
		}
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.Q))
		{
			
			if (isMain){
				p.camout ();
			}
			p.setSpeed (1);
		}
		if (Input.GetKey(KeyCode.Z))
		{
			
			if (isMain){
				p.camin ();
			}
			p.setSpeed (-1);
		}

		if (Input.GetKeyUp (KeyCode.Q) || getSpeed() >= flightPhysics.getMaxSpeed()|| Input.GetKeyUp(KeyCode.Z) || getSpeed() <= flightPhysics.getMinSpeed()) {
			p.camstab ();
		}

		if (Input.GetKey(KeyCode.Space))
		{
			fireMissile ();
		}
		/*
		if (Input.GetKey (KeyCode.M)) {
			
			m.IsOpen = true;

			//StartCoroutine(waitforsec());
			//paused = togglePause();		
		}
		if (Input.GetKey (KeyCode.N)) {

			m.IsOpen = false;
			GameObject.Find("AI").GetComponent<AISmart>().enabled = false;
			//GameObject.Find("AI").GetComponent<AI>().;
			//paused = togglePause();		
		}*/

	}

	void FixedUpdate()
	{
		//Simulate arcade style airplane controls
		float[] commands = new float[2];
		commands[0] = Input.GetAxis("Horizontal");
		commands[1] = Input.GetAxis("Vertical");

		p.steerPlane (commands);
	}

	public string getName(){
		return myName;
	}
	public void setName(string n){
		myName =n;
	}
	public void fireMissile(){
		p.releaseMissile();
	}
	public Vector3 getPos(){
		
		return p.getPosition ();
	}
	public Vector3 getDir(){
		return p.getDirection();
	}
	public Vector3 getFor(){
		return p.getForward();
	}
	public float getSpeed(){
		return p.getSpeed();
	}
	public int getHealth(){
		return p.getHealth ();
	}

	IEnumerator waitforsec() {
		yield return new WaitForSeconds(1);
	}

	public void setStateFromPlayerState (playerState pstate){
		setPosition (pstate.getPosition ());
		setDirection (pstate.getDirection ());
		setSpeed (pstate.getSpeed ());
		setHealth (pstate.getHealth ());
	}

	public void setPosition(Vector3 pos){
		p.setPos (pos);
	}

	public void setDirection(Vector3 dir){
		p.setDir (dir);
	}

	public void setSpeed(float spd){
		p.setSpd (spd);
	}

	public void setHealth(int hlt){
		p.setHlt (hlt);
	}
}