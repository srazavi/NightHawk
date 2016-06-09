using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public static class saveLoad { //MUST SET SAVEPATH VARIABLE TO PROPERLY USE THIS CLASS
	private static IPlayer humanPlayer = GameObject.Find ("Human").GetComponent<Human>();
	private static IPlayer AIPlayer = GameObject.Find("AI").GetComponent<AI>();

	public static void save() {
		playerState humanState = new playerState (humanPlayer);
		playerState AIState = new playerState (AIPlayer);
		GameState currentGame = new GameState (humanState, AIState);

		DBConnector.saveToDB <GameState> (currentGame);
	}

	public static List<GameState> load(){
		List<GameState> loadedGames = DBConnector.loadFromDB<GameState> ();

		//AIPlayer.setStateFromPlayerState (loadedGames [loadedGames.Count-1].getAIPlayerState ());
		//humanPlayer.setStateFromPlayerState(loadedGames[loadedGames.Count-1].getHumanPlayerState());

		return loadedGames;
	}



	public static void setSavePath(string path){
		DBConnector.setDBFilePath(path);
	}
}
