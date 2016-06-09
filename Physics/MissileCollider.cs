using UnityEngine;
using System.Collections;

public class MissileCollider : MonoBehaviour {
	
	//public ParticleSystem exp;

	void OnTriggerEnter(Collider other) 
	{
		
		Destroy (gameObject);

	}
}
