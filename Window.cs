using UnityEngine;
using System.Collections;

public class Window : MonoBehaviour {

	public bool debug = false;
	public MeshRenderer windowRend;
	public Light lamp;

	// Use this for initialization
	void Start () {
		if(windowRend != null){
			windowRend.material = GameModel.Instance.GetRandomMaterial("window");
//			if(debug)
//				Debug.Log("Changed window texture");
		}

		/*if(lamp != null){
			if(Random.Range(0, 4) == 0){
				
				Color color = GameModel.Instance.GetRandomColor();
				if(color != null){
					lamp.gameObject.SetActive(true);
					lamp.color = color;
					if(debug)
						Debug.Log("------------> Lamp at pos " + gameObject.transform.position + " color set to " + color.ToString());
				} else {
					lamp.gameObject.SetActive(false);
				}
			}
		}*/
	}

}
