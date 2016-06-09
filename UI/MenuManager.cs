using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{
	public Menu CurrentMenu;
	public Button loadable;
	public GameState loadedGame;
	public Load loader;
	private string uName = "Default Username";
	private float gameVolume;
	private float difficulty = 1f;

	public void Start()
	{
		ShowMenu(CurrentMenu);

	}

	public void ShowMenu(Menu menu)
	{
		if (CurrentMenu != null)
		{
			CurrentMenu.IsOpen = false;
		}
		CurrentMenu = menu;
		CurrentMenu.IsOpen = true;
	}

	public void quit()
	{
		//Application.Quit();

	}

	public void setDifficulty(Scrollbar s){
		difficulty = s.value;
	}

	public void StartGame ()
	{
		loader.resetGameState ();
		Application.LoadLevel ("localGame");
	}

	public void loadGame(){
		Application.LoadLevel ("localGame");
	}

	public void setUsername(string name)
	{
		uName = name;
	}

	public void setGameVolume(float vol)
	{
		gameVolume = vol;
		PlayerPrefs.SetFloat("volume", vol);

	}

	public void populateLoadableGameList(){
		//saveLoad s = saveLoad ;
		List<GameState> listOfGames = DBConnector.loadFromDB<GameState> ();
		//Debug.Log (listOfGames[0].AIPlayerState.getSpeed());
		float yPos = 155;
		foreach(GameState game in listOfGames){
			//Debug.Log (game.AIPlayerState.getDirection());
			GameObject v = GameObject.Find ("LoadMenu");
			Button b = Instantiate (loadable);
			//b.transform.SetParent (v.GetComponent<Transform>());
			b.transform.SetParent (v.GetComponent<Transform>());
			b.transform.localScale = new Vector3 (1f, 1f, 1f);
			b.transform.localPosition = new Vector3 (0f, yPos, 0f);
			//b.transform.position = GetComponentInParent
			b.GetComponentInChildren<Text> ().text = game.getTimeStamp();


			yPos -= 30;
		}
	}

	public void setGameToLoad(string time){
		List<GameState> listOfGames = DBConnector.loadFromDB<GameState> ();

		//bool foundit = false;
		foreach (GameState game in listOfGames) {
			if (game.getTimeStamp ().Equals (time)) {
				loadedGame = game;
				//Debug.Log (game.AIPlayerState.getHealth ());
				loader.setGameState (game);
			}
		}


	}
				
}