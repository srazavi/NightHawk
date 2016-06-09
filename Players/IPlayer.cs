using UnityEngine;
using System.Collections;

public interface IPlayer{
	void fireMissile();
	Vector3 getPos();
	Vector3 getDir();
	Vector3 getFor();
	float getSpeed();
	int getHealth();
	void setStateFromPlayerState (playerState pstate);
	void setPosition (Vector3 pos);
	void setDirection (Vector3 dir);
	void setSpeed (float spd); 
	void setHealth (int hlt);

}