using UnityEngine;
using UnityEngine.UI;
using System;					// added to access the enum class
using System.Collections.Generic;

public class BaseCharacter : MonoBehaviour
{
	public enum Gender {male, female};
	public enum Species {humanoid, animal};
	public enum Kind {human, vampire, ghoul};
	public enum Mood {neutral, hungry, frenetic, angry, lunatic, excited};
	public enum Clan {None, Daeva, Gangrel, Mekhet, Nosferatu, Ventrue};
	public enum Covenant {None, Carthian, Circle, Invictus, Lancea, Ordo};

	public bool FeralWhispers = false;
	public Light flashlight;
	public ParticleSystem bubble;
	public string name;
	public Gender gender = Gender.male;
	public float heigth = 1.5f;
	public Species species = Species.humanoid;
	public Kind kind = Kind.human;
	public Mood mood = Mood.neutral;
	public Clan clan = Clan.None;
	public Covenant covenant = Covenant.None;
	public GameObject activeWeapon;
	
	public List<Vital> vitals = new List<Vital>();
//	public List<Attribute> attributes = new List<Attribute>();
//	public List<Skill> skills = new List<Skill>();
	public int currentDiscipline = 0;
	public List<Power> disciplines = new List<Power>();

	public List<GameObject> inventory = new List<GameObject>();
	public int currentWeapon = 0;
	public List<GameObject> weapons = new List<GameObject>();

	public List<BaseCharacter> allies = new List<BaseCharacter>();
	public List<BaseCharacter> enemies = new List<BaseCharacter>();
	public List<Quest> quests = new List<Quest>();

	private float scroll;

	public Dialogue currentDialogue;
	public static GameObject fist;

	void Start() {
		if(gameObject.CompareTag("Player")){
			fist = GetComponentInChildren<Weapon>().gameObject;
			weapons.Add(fist);
			clanSelection.Instance.AddPowers(clan);
		}

		if(gameObject.CompareTag("NPC")) {
			GameObject aura = Instantiate(Resources.Load<GameObject>("aura"), transform.position, transform.rotation) as GameObject;
			aura.transform.parent = transform;
			aura.GetComponent<Light>().color = GetMoodColor();
			aura.GetComponent<Light>().enabled = false;
		}
	}

	Color GetMoodColor(){
		switch(mood){
		case Mood.angry:
			return Color.red;
		case Mood.excited:
			return Color.yellow;
		case Mood.frenetic:
			return Color.gray;
		case Mood.hungry:
			return Color.blue;
		case Mood.lunatic:
			return Color.magenta;
		case Mood.neutral:
			return Color.white;
		default:
			return Color.black;
		}
	}

	void Update() {
		if(weapons.Count > 1){
			scroll = Input.GetAxis("Mouse ScrollWheel");
			if(scroll > 0.01f) {
				if(currentWeapon < weapons.Count-1) {
					currentWeapon++;
				} else {
					currentWeapon = 0;
				}
				SwitchWeapon();
			} else if(scroll < -0.01f) {
				if(currentWeapon > 0) {
					currentWeapon--;
				} else {
					currentWeapon = weapons.Count-1;
				}
				SwitchWeapon();
			}
		}
	}

	public void Swimming(bool isSwimming) {
		GetComponent<Animator>().SetBool("swim", isSwimming);
	}

	public void ToggleFlashlight() {
		if(flashlight != null)
			flashlight.enabled = !flashlight.enabled;
	}


	public void SwitchWeapon() {
		Destroy(activeWeapon);
		activeWeapon = Instantiate(weapons[currentWeapon].gameObject);
		activeWeapon.transform.position = fist.transform.position;
		activeWeapon.transform.rotation = fist.transform.rotation;
		activeWeapon.transform.parent = fist.transform;
		activeWeapon.transform.localScale = Vector3.one;
		UIManager.Instance.UpdateUI();
	}

	public void AddToInventory(GameObject item) {
		inventory.Add(item);
		item.transform.parent = GameSettings.Instance.player.transform;
		item.transform.localPosition = Vector3.zero;
		Debug.Log (item.name + " is an item of type " + item.GetComponent<Item>().itemType + " and has been added to the inventory");
	}

	public void AddToWeapons(GameObject item) {
		weapons.Add(item);
		item.transform.parent = GameSettings.Instance.player.transform;
		item.transform.localPosition = Vector3.zero;
		item.transform.localScale = Vector3.zero;
	}

	public void NextDiscipline(){
		if(currentDiscipline > 0) {
			currentDiscipline--;
		} else {
			currentDiscipline = disciplines.Count-1;
		}
		UIManager.Instance.UpdateUI();
	}


//
//	public void CompleteQuest()
//	{
//		Debug.Log ("calling complete quest from " + currentDialogue.gameObject.name);
//		currentDialogue.CompleteQuest();
//	}
	
//	void OnControllerColliderHit(ControllerColliderHit collider)
//	{
//	 	Ammo ammo = collider.gameObject.GetComponent<Ammo>();
//		if(ammo != null)
//		{
//			health.value += ammo._damageAmmo;
//		}
//	}
	
	//setup the basic value	
//	private void SetupPrimaryAttribute()
//	{
//		for(int cnt = 0; cnt < _primaryAttribute.Length; cnt++)
//		{
//			_primaryAttribute[cnt] = new Attribute();
//			_primaryAttribute[cnt].Name = ((AttributeName)cnt).ToString();
//		}
//	}
//	
//	private void SetupVital()
//	{
//		_health.BaseValue = 100f;
////		Debug.Log(_health.BaseValue);
//		_health.SliderUI = GameObject.Find("healthSlider").GetComponent<Slider>();
////		Debug.Log(_health.SliderUI.gameObject.name);
//
//		_vitae.BaseValue = 75f;
//		Debug.Log(_vitae.BaseValue);
//		_vitae.SliderUI = GameObject.Find("vitaeSlider").GetComponent<Slider>();
//		Debug.Log(_vitae.SliderUI.gameObject.name);
//
//
////		SetupVitalModifiers();
//
//	}
	
//	private void SetupSkill()
//	{
//		for(int cnt = 0; cnt < _skill.Length; cnt++)
//		{
//			_skill[cnt] = new Skill();
//		}
//		
//		SetupSkillModifiers();
//	}
	
	//Getters for base values
//	public Attribute GetPrimaryAttribute (int index)
//	{
//		return _primaryAttribute[index];
//	}
//	
//	public Vital GetVital (int index)
//	{
//		return _vital[index];
//	}
//	
//	public Skill GetSkill (int index)
//	{
//		return _skill[index];
//	}
//	
	//modify vitals, based on attributes
//	private void SetupVitalModifiers()
//	{
//		/*ModifyingAttribute health = new ModifyingAttribute();
//	health.attribute = GetPrimaryAttribute((int)AttributeName.staminaPA);
//	health.ratio = .5f;
//	GetVital ((int)VitalName.Health).AddModifier(health);
//	*/
//		
//		//health
//		GetVital((int)VitalName.Health).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.staminaPA), ratio = 1});
//		//humanity
//		GetVital((int)VitalName.Humanity).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.intelligenceMA), ratio = 1});
//		//vitae
//		GetVital((int)VitalName.Vitae).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.dexterityPA), ratio = 1});
//	}
	
	//modify skills, based on attributes
//	private void SetupSkillModifiers()
//	{
//		//physic
//		GetSkill((int)SkillName.athleticsPS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.strengthPA), ratio = 1});
//		GetSkill((int)SkillName.brawlPS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.staminaPA), ratio = 1});
//		GetSkill((int)SkillName.drivePS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.dexterityPA), ratio = 1});
//		GetSkill((int)SkillName.firearmsPS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.dexterityPA), ratio = 1});
//		GetSkill((int)SkillName.larcenyPS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.dexterityPA), ratio = 1});
//		GetSkill((int)SkillName.stealthPS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.dexterityPA), ratio = 1});
//		GetSkill((int)SkillName.survivalPS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.staminaPA), ratio = 1});
//		GetSkill((int)SkillName.weaponryPS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.strengthPA), ratio = 1});
//		
//		//mental
//		GetSkill((int)SkillName.academicsMS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.witsMA), ratio = 1});
//		GetSkill((int)SkillName.computerMS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.resolveMA), ratio = 1});
//		GetSkill((int)SkillName.craftsMS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.intelligenceMA), ratio = 1});
//		GetSkill((int)SkillName.investigationMS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.resolveMA), ratio = 1});
//		GetSkill((int)SkillName.medicineMS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.intelligenceMA), ratio = 1});
//		GetSkill((int)SkillName.occultMS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.witsMA), ratio = 1});
//		GetSkill((int)SkillName.politicsMS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.witsMA), ratio = 1});
//		GetSkill((int)SkillName.scienceMS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.resolveMA), ratio = 1});
//		
//		//social
//		GetSkill((int)SkillName.animalKenSS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.composureSA), ratio = 1});
//		GetSkill((int)SkillName.empathySS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.composureSA), ratio = 1});
//		GetSkill((int)SkillName.expressionSS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.manipulationSA), ratio = 1});
//		GetSkill((int)SkillName.intimidationSS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.apparenceSA), ratio = 1});
//		GetSkill((int)SkillName.persuasionSS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.manipulationSA), ratio = 1});
//		GetSkill((int)SkillName.socializeSS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.apparenceSA), ratio = 1});
//		GetSkill((int)SkillName.streetwiseSS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.composureSA), ratio = 1});
//		GetSkill((int)SkillName.subterfugeSS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.manipulationSA), ratio = 1});
//	}
	
//	public void StatUpdate()
//	{
//		for(int cnt = 0; cnt < _vital.Length; cnt ++)
//			_vital[cnt].Update();
//		
//		for(int cnt = 0; cnt < _skill.Length; cnt ++)
//			_skill[cnt].Update();
//	}
}