using UnityEngine;
using System.Collections;

public class Poster : MonoBehaviour {

	MeshRenderer _meshRend;

	// Use this for initialization
	void Start () {
		_meshRend = GetComponent<MeshRenderer>();

		if(_meshRend != null)
			_meshRend.material = GameModel.Instance.GetRandomMaterial("poster");
	}
	

}
