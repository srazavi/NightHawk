using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class controller : MonoBehaviour {
	private Load loader;
	private GameState currentGame;
	private static IPlayer humanPlayer;
	private static IPlayer AIPlayer;
	public Menu m;
	public Menu win;
	public Menu lost;

	// Use this for initialization
	void Start () {
		saveLoad.setSavePath ("/Users/shahabrazavi/nightHawk.sqlite");
		AudioListener.volume = PlayerPrefs.GetFloat("volume");
		loader = GameObject.Find ("gameToLoad").GetComponent<Load> ();
		currentGame = loader.getGameState ();

		humanPlayer = GameObject.Find ("Human").GetComponent<Human>();
		AIPlayer = GameObject.Find("AI").GetComponent<AI>();

		humanPlayer.setStateFromPlayerState (currentGame.getHumanPlayerState());
		AIPlayer.setStateFromPlayerState (currentGame.getAIPlayerState());


		//m = (Menu)GameObject.Find ("Canvas").GetComponent<Menu>();
		//win = (Menu)GameObject.Find ("WIN").GetComponent<Menu>();
		//lost = (Menu)GameObject.Find ("LOST").GetComponent<Menu>();
	}
	
	// Update is called once per frame
	void Update () {
		/*if (Input.GetKeyDown ("y")) {
			saveLoad.save ();
		}
		if (Input.GetKeyDown ("u")) {
			List<GameState> loadedGames = saveLoad.load ();

		
		}*/
		if (Input.GetKey (KeyCode.M)) {

			m.IsOpen = true;
			GameObject.Find ("Human").GetComponent<Human> ().enabled = false;
			GameObject.Find ("AI").GetComponent<AISmart> ().enabled = false;
		
		}
		if (Input.GetKey (KeyCode.N)) {

			m.IsOpen = false;
			GameObject.Find ("Human").GetComponent<Human> ().enabled = true;
			GameObject.Find ("AI").GetComponent<AISmart> ().enabled = true;

		}
	}

	public void saveGame(){
		saveLoad.save ();
	}

	public void backToMainMenu(){
		Destroy(GameObject.Find("gameToLoad"));
		Application.LoadLevel ("NightHawk-GUI");
	}

	public void iLost(Airplane a)
	{
		//disengage planes
		GameObject.Find ("Human").GetComponent<Human> ().enabled = false;
		GameObject.Find ("AI").GetComponent<AISmart> ().enabled = false;

		print (a.GetComponent<Transform> ().parent.name);
		if (a.GetComponent<Transform> ().parent.name.Equals("Human")) {
			lost.IsOpen = true;
		} else if (a.GetComponent<Transform> ().parent.name.Equals("AI")) {
			win.IsOpen = true;
		}
	}
}
