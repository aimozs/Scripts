using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OriginManager : MonoBehaviour {

	public enum OriginEnum {None, Egyptian, Greek, Hebrew, Indian, Romanian};

	public Origin Human;
	public Origin Egyptian;
	public Origin Greek;
	public Origin Hebrew;
	public Origin Indian;
	public Origin Romanian;

	private static OriginManager instance;
	public static OriginManager Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<OriginManager>();
			}
			return instance;
		}
	}

	public Origin GetOrigin(OriginEnum newOrigin){
		switch(newOrigin){
		case OriginEnum.Egyptian:
			return Egyptian;
		case OriginEnum.Greek:
			return Greek;
		case OriginEnum.Hebrew:
			return Hebrew;
		case OriginEnum.Indian:
			return Indian;
		case OriginEnum.Romanian:
			return Romanian;
		default:
			return Human;
		}
	}

}
