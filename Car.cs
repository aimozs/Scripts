using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {

	public bool debug = false;
	public MeshRenderer carRend;

	// Use this for initialization
	void Start () {
		if(carRend != null){
			carRend.material = GameModel.Instance.GetRandomMaterial("car");
			//			if(debug)
			//				Debug.Log("Changed window texture");
		}

	}
}
