using UnityEngine;
using System.Collections;

public class NewBehaviourScript1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
