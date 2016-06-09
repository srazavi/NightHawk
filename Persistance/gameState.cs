using UnityEngine;
using System.Collections;
using System;

public class GameState {
	public playerState humanPlayerState;
	public playerState AIPlayerState;
	public string timeStamp;
	public float difficulty;

	public GameState(){}

	public GameState(playerState human, playerState AI){
		humanPlayerState = human;
		AIPlayerState = AI;
		difficulty = 1;
		timeStamp = DateTime.Now.ToString("G");
	}

	public GameState(playerState human, playerState AI, float dif){
		humanPlayerState = human;
		AIPlayerState = AI;
		difficulty = dif;
		timeStamp = DateTime.Now.ToString("G");
	}

	public playerState getAIPlayerState(){
		return AIPlayerState;
	}

	public playerState getHumanPlayerState(){
		return humanPlayerState;
	}

	public string getTimeStamp(){
		return timeStamp;
	}
}

public class playerState{
	public Vector3 playerPos;
	public Vector3 playerDir;
	public float playerSpeed;
	public int playerHealth;

	public playerState(){}

	public playerState(Vector3 pos, Vector3 dir, float speed, int health){
		playerPos = pos;
		playerDir = dir;
		playerSpeed = speed;
		playerHealth = health;
	} 

	public playerState(IPlayer p){
		playerPos = p.getPos ();
		playerDir = p.getDir ();
		playerSpeed = p.getSpeed ();
		playerHealth = p.getHealth ();
	}

	public Vector3 getPosition(){
		return playerPos;
	}

	public Vector3 getDirection(){
		return playerDir;
	}

	public float getSpeed(){
		return playerSpeed;
	}

	public int getHealth (){
		return playerHealth;
	}

}