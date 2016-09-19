using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityStandardAssets.Characters.ThirdPerson.AICharacterControl))]
[RequireComponent(typeof(BaseCharacter))]
public class NPC : MonoBehaviour {

	[Header("Work")]
	public string WorkSceneName;
	public Vector3 Wposition;
	public Vector3 Wrotation;
	[Range(0.0f, 23.0f)]
	public float WpresentFrom;
	[Range(0.0f, 23.0f)]
	public float WpresentTo;


	[Header("Home")]
	public string HomeSceneName;
	public Vector3 Hposition;
	public Vector3 Hrotation;
	[Range(0.0f, 23.0f)]
	public float HpresentFrom;
	[Range(0.0f, 23.0f)]
	public float HpresentTo;

	public Globals.Mood mood = Globals.Mood.confused;

	public UnityStandardAssets.Characters.ThirdPerson.AICharacterControl _AIController;

	private bool init = false;

	#region 0.Basics
//	void Awake(){}

	void Start(){
		SetupNPC();
	}

//	void OnEnable(){}

//	void OnDisable(){}

//	void Update(){}

	void FixedUpdate(){
		if(_AIController.target != null){
			if(Vector3.Distance(_AIController.target.position, transform.position) < 2f){
				UseInteractable(_AIController.target.GetComponent<Interactable>());
//				SetRandomDestination();
			}
		}
	}

	#endregion

	#region 1.Statics
	#endregion

	#region 2.Publics
	#endregion

	#region 3.Privates

	void SetupNPC(){
		SetupAura();
		SetupAI(true);
		SetupSpeed();

	}

	void UseInteractable(Interactable _interactable){
		_interactable.OnInteract();
	}

	void SetupLocation(float timeFrom, float timeTo, Vector3 position, Vector3 rotation){
		float time = DayNightCycle.Instance.CurrentTime;

		if(timeFrom < timeTo){
			if(timeFrom < time && time < timeTo){
				gameObject.transform.position = position;
				Vector3 rot = gameObject.transform.rotation.eulerAngles;
				rot = rotation;
			}
		} else {
			if(timeFrom < time || time < timeTo){
				gameObject.transform.position = position;
				Vector3 rot = gameObject.transform.rotation.eulerAngles;
				rot = rotation;
			}
		}
		
	}

	void SetupAI(bool on){
		GetComponent<NavMeshAgent>().enabled = on;

		if(_AIController == null) {
			if(GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>() == null)
				gameObject.AddComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();

			_AIController = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
		}

		_AIController.enabled = on;

//		if(GameManager.CurrentSceneName == WorkSceneName){
//			SetDestination(Wdefault);
//		} else {
//
//			if(GameManager.CurrentSceneName == HomeSceneName){
//				SetDestination(Wdefault);
//			} else {
				SetRandomDestination();
//			}
//		}
	}

//	public void SetTimeN(float currentTime){
//		if(!init){
//			if(WpresentFrom < WpresentTo){
//				if(WpresentFrom < currentTime){
//					if(currentTime < WpresentTo){
//					} else {
//						gameObject.SetActive(false);
//					}
//				} else {
//					gameObject.SetActive(false);
//				}
//			} else {
//				if(currentTime < WpresentTo){
//				} else {
//					gameObject.SetActive(false);
//				}
//			}
//			init = true;
//		}
//	}
		
	public void SetRandomDestination(){
		if(_AIController != null){
			GameObject newTarget = GameSettings.GetRandomTarget();

			if(newTarget != null && _AIController.isActiveAndEnabled){
//				Debug.Log(gameObject.name + " is now moving toward " + newTarget.gameObject.name);
				_AIController.SetTarget(newTarget.transform);
			}
		}
	}

	void SetupAura(){
		GameObject aura = Instantiate(Resources.Load<GameObject>("aura")) as GameObject;
		aura.transform.SetParent(transform, false);
		aura.GetComponent<ParticleSystem>().startColor = GameModel.GetMoodColor(mood);
	}

	void SetupSpeed(){
		switch(mood){
		case Globals.Mood.afraid:
		case Globals.Mood.psychotic:
			_AIController.moveSpeed = 1f;
			break;
		case Globals.Mood.angry:
		case Globals.Mood.agressive:
		case Globals.Mood.excited:
		case Globals.Mood.frenzied:
			_AIController.moveSpeed = .7f;
			break;
		case Globals.Mood.calm:
		case Globals.Mood.confused:
		case Globals.Mood.daydreaming:
		case Globals.Mood.depressed:
		case Globals.Mood.sad:
			_AIController.moveSpeed = .3f;
			break;
		default:
			_AIController.moveSpeed = .5f;
			break;
		}
	}

	#endregion
}
