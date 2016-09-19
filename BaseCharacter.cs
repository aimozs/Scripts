using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class BaseCharacter : MonoBehaviour {
	public enum Gender {male, female};
	public enum Species {humanoid, animal};
	public enum Kind {human, vampire, ghoul};
	public enum Attributes {strength, dexterity, stamina, intelligence, wit, resolve, presence, manipulation, composure}

	public bool debugBC = true;

	[Header("Char controls")]
	public bool _isCrouching;
	public bool _isClimbing = false;
	public bool _isSwimming = false;
	public bool sunProtected = false;


	[Header("Char stats")]
	public string firstname;
	public Gender gender = Gender.male;
	public Species species = Species.humanoid;
	public Kind kind = Kind.human;
	public OriginManager.OriginEnum _origin;

//	public UnityStandardAssets.Characters.FirstPerson FPC;
	public float health = 1f;
	public float vitae = 1f;

	public int strengthBonus = 1;
	public int dexterityBonus = 1;
	public int staminaBonus = 1;

	public int intelligenceBonus = 1;
	public int witBonus = 1;
	public int resolveBonus = 1;

	public int presenceBonus = 1;
	public int manipulationBonus = 1;
	public int composureBonus = 1;

	//CHAR BASICS
	public GameObject charModel;
	public Animator charAnimator;
	public AudioSource fxAudioSource;

	public CharacterController charControl;
	public float crouchFactor = 1.5f;
	public GameObject activeWeapon;

	public Animator cutScenes;

	[Header("Inventory")]
	public GameObject fist;
	public int currentWeapon = 0;
	public List<GameObject> inventory = new List<GameObject>();
	public List<GameObject> weapons = new List<GameObject>();
	public GameObject inspectionHolder;

	[Header("Powers")]
	public Discipline spectrum1;
	public Discipline spectrum2;
	public Discipline spectrum3;

	public Transform effects;
	public Animator powerVis;
	public int currentPower = 0;
	public List<Power> powers = new List<Power>();


	//RELATIONS
	public List<BaseCharacter> allies = new List<BaseCharacter>();
	public List<BaseCharacter> enemies = new List<BaseCharacter>();
	public List<Quest> quests = new List<Quest>();

	private UnityStandardAssets.Characters.FirstPerson.FirstPersonController playerCont;
	private bool _feralWhispers = false;
	private Vector3 sunDirection;

	private RaycastHit hit;

	#region 0.Basics
	void Start() {
		fxAudioSource = GetComponent<AudioSource>();

		if(gameObject.CompareTag("Player")){
			DontDestroyOnLoad(gameObject);


			playerCont = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
			charControl = GetComponent<CharacterController>();
			UpdateJumpSpeed();

			if(fist == null)
				fist = GetComponentInChildren<Weapon>().gameObject;
			weapons.Add(fist);
			activeWeapon = fist;
			InventoryManager.InspectionHolder = inspectionHolder;
		}

		GetCharAnimator();

		if(charAnimator != null)
			charAnimator.runtimeAnimatorController = NPCManager.Instance.basicController;

		InitBC(GameManager.CurrentScene);

	}

	void OnEnable(){
		SceneManager.sceneLoaded += InitBC;
	}

	void OnDisable(){
		GameSettings.updateGS -= ApplySunDamage;
		SceneManager.sceneLoaded -= InitBC;
	}

	#endregion

	#region 1.Statics
	public void PlayAnim(AnimationClip _anim){
		Animation animations = GetComponent<Animation>();

	}
	#endregion

	#region 2.Publics
	#endregion

	#region 3.Privates
	void InitBC(Scene scene, LoadSceneMode loadMode = LoadSceneMode.Single){
		Climb(false);
		if(gameObject.CompareTag("Player")){
			EnableControls();
			CameraManager.Instance.SetFirstPersonCamera(true);
		}
		ActivateRagDoll(false);
		if(vitae <= 0f)
			SetVitae(.5f);
		if(scene.name == "Montreal"){
			if(kind == Kind.vampire){
				GameSettings.updateGS += ApplySunDamage;
			}
		}

	}
	#endregion




	void ApplySunDamage(){
//		Debug.Log("Applying sun damage to " + gameObject.name);
		sunDirection = DayNightCycle.SunPosition-transform.position;
//		Debug.DrawRay(transform.position, sunDirection, Color.yellow, 1000);
		if (Physics.Raycast(transform.position, sunDirection, out hit, 1000)) {
//			Debug.Log(hit.collider.gameObject.name);

			if (hit.collider.CompareTag("Sun")) {
//				Debug.Log(gameObject.name + " is in light");
				AdjustVitae(-.05f);
			}


//		if(DayNightCycle.Instance.currentPeriod == DayNightCycle.DayPeriod.day){
//			AdjustVitae(-.1f / (Globals.Instance.playerData.stamina + staminaBonus));
		}
	}

	public void PlayFxSound(AudioClip sound) {
		if(fxAudioSource != null){
			fxAudioSource.PlayOneShot(sound);
		}
	}

	public void UpdateJumpSpeed(){
		GameSettings.Instance.player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().SetJumpSpeed(Globals.Instance.playerData.strength + strengthBonus * 2 );
	}

	public void AddSpectrum(Discipline newSpectrum, int level = 1){
		if(spectrum1 == null){
			spectrum1 = newSpectrum;
			Globals.Instance.playerData.spectrum1 = spectrum1.spectrumName.ToString();
			kind = Kind.vampire;
			Globals.Instance.playerData.charKind = kind.ToString();
			UIManager.EnableBloodUI(true);
		} else { 
			if(spectrum2 == null){
				if(newSpectrum != spectrum1)
					spectrum2 = newSpectrum;
			} else { 
				if(spectrum3 == null){
					if(newSpectrum != spectrum2 && newSpectrum != spectrum1)
						spectrum3 = newSpectrum;
				}
			}
		}


		for(int p = 1; p <= level; p++){
			if(newSpectrum.powers.Count >= p){
				if(newSpectrum.powers[p-1] != null)
					if(!powers.Contains(newSpectrum.powers[p-1]))
						powers.Add(newSpectrum.powers[p-1]);
			}
		}

		UIManager.PowerIcon = powers[currentPower].iconPower;
//		UIManager.Instance.UpdateUI();
	}

	public void ActivePowerEffect(GameObject effect, bool on, float time){
		if(on){
			GameObject newEffect = Instantiate(effect);
			newEffect.transform.SetParent(effects, false);
			Destroy(newEffect, time);
		}
	}

	public Kind charKind {
		get { return kind; }
		set{ kind = value; }
	}

	public bool FeralWhispers {
		get { return _feralWhispers; }
		set { _feralWhispers = value; }
	}

	public OriginManager.OriginEnum charOrigin {
		get { return _origin; }
		set{ _origin = value; }
	}

	void GetCharAnimator(){
		if(charModel != null){
			if(charAnimator == null)
				charAnimator = charModel.GetComponent<Animator>();

			ActivateCharModel(false);
		} else {
			charAnimator = GetComponent<Animator>();
		}
	}

	public void AdjustVitae(float adjustment, AudioClip clip = null){
		if(vitae > 0f && !GameManager.IsLoading){
//			Debug.Log("adjusting of " + adjustment);
			SetVitae(vitae + adjustment);
			if(clip != null)
				fxAudioSource.PlayOneShot(clip);
		}
	}

	public void SetVitae(float value){
		vitae = value;
		if(vitae <= 0f){
			Die();
		} else {
			if(gameObject.CompareTag("Player")){
				UIManager.VitaeSlider = Globals.Instance.playerData.vitae = vitae;
			}
		}
	}

	void Die(){
		if(gameObject.CompareTag("Player")) {
			StartCoroutine(ReloadAfterDeath());
			EnableControls(false);
			CameraManager.Instance.SetFirstPersonCamera(false);
			PlayFxSound(GameModel.Instance.deathMusicClip);

		} else {
			//TODO RESET NPC
		}
		ActivateRagDoll(true);
	}

	void ActivateRagDoll(bool on){
		if(charAnimator == null)
			GetCharAnimator();
		
		if(charAnimator != null)
			charAnimator.enabled = !on;
//		Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();
//		foreach(Rigidbody rb in rbs){
//			rb.isKinematic = false;
//		}
		
	}

	IEnumerator ReloadAfterDeath(){
		UIManager.Notify("You've suffered final death");

		yield return new WaitForSeconds(4f);
		GameManager.Instance.Reload();
	}


	public void SetMoveSpeed(Vector3 movement){

		if(charAnimator.isActiveAndEnabled){
			charAnimator.SetFloat("Forward", movement.x);
			charAnimator.SetFloat("Turn", movement.z);
		}
	}

	public void EnableControls(bool on = true){
		if(playerCont != null)
			playerCont.canMove = on;
//		UIManager.DisplayCursor(false);
	}

	#region character moves
	public void Climb(bool climb){
		_isClimbing = climb;

		if(playerCont == null)
			playerCont = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();

		if(playerCont != null)
			playerCont.Climb(_isClimbing);
	}

	public void Swim(bool isSwimming = false) {

		_isSwimming = isSwimming;

		if(charAnimator != null && charAnimator.isActiveAndEnabled)
			charAnimator.SetBool("Swim", _isSwimming);

		if(playerCont != null)
			playerCont.Swim(_isSwimming);
	}

	public void Crouch() {
		Debug.Log ("Toggle Crouching");
		_isCrouching = !_isCrouching;
		Crouch(_isCrouching);
	}

	public void Crouch(bool crouch) {
		Debug.Log ("Crouching " + crouch);

		if(gameObject.CompareTag("Player")){
			CameraManager.Instance.ToggleCrouch(crouch);
		}

		AdjustCharControl(crouch);

		if(charAnimator != null && charAnimator.isActiveAndEnabled)
			charAnimator.SetBool("Crouch", crouch);

		if(playerCont != null)
			playerCont.Crouch(_isCrouching);
	}

	#endregion

	void AdjustCharControl(bool crouch){
		if(charControl != null){
			if(crouch){
				charControl.height = charControl.height / crouchFactor;
				charControl.center = new Vector3(0f, charControl.center.y / crouchFactor, 0f);
			} else {
				charControl.center = new Vector3(0f, charControl.center.y * crouchFactor, 0f);
				charControl.height = charControl.height * crouchFactor;
			}
		}
	}

	public void SwitchWeapon() {
		if(activeWeapon != fist)
			Destroy(activeWeapon);

		if(currentWeapon >= 1){
			activeWeapon = Instantiate(weapons[currentWeapon].gameObject);
			activeWeapon.transform.position = fist.transform.position;
			activeWeapon.transform.rotation = fist.transform.rotation;
			activeWeapon.transform.parent = fist.transform;
			activeWeapon.transform.localScale = Vector3.one;	
		} else {
			activeWeapon = fist;
		}

//		UIManager.Instance.UpdateUI();
		UIManager.UpdateWeaponUI(weapons[currentWeapon].GetComponent<Weapon>().iconWeapon);
	}

	public void AnimateRange(){
		if(charAnimator.isActiveAndEnabled){
			charAnimator.SetLayerWeight(1, 1f);
			charAnimator.SetTrigger("Shoot");
			StartCoroutine(ResetLayerWeight());
		}
	}

	public void AnimateBrawl(){
		if(charAnimator.isActiveAndEnabled){
			charAnimator.SetLayerWeight(1, 1f);
			charAnimator.SetTrigger("Brawl");
			StartCoroutine(ResetLayerWeight());
		}
	}

	public void PlayEmbraceAnimation(){
		cutScenes.SetTrigger("embrace");
	}

	IEnumerator ResetLayerWeight(){
		yield return new WaitForSeconds(3f);
		charAnimator.SetLayerWeight(1, 0f);
	}


//	public void AddToInventory(GameObject item) {
//		inventory.Add(item);
//		item.transform.parent = GameSettings.Instance.player.transform;
//		item.transform.localPosition = Vector3.zero;
//		item.transform.localScale = Vector3.zero;
//		Debug.Log (item.name + " has been added to the inventory");
//		InventoryManager.RemoveFromCurrentContainer(item);
//	}


//	public void DepositItemInContainer(GameObject item){
//		inventory.Remove(item);
//		GameSettings.Instance.DepositItemInContainer(item);
//	}

	public void AddToWeapons(GameObject item) {
		weapons.Add(item);
		item.transform.parent = GameSettings.Instance.player.transform;
		item.transform.localPosition = Vector3.zero;
		item.transform.localScale = Vector3.zero;
	}

	public void NextPower(bool next){
		if(powers.Count > 0){
			if(next){
				if(currentPower > 0) {
					currentPower--;
				} else {
					currentPower = powers.Count-1;
				}
			} else {
				if(currentPower < powers.Count-1) {
					currentPower++;
				} else {
					currentPower = 0;
				}
			}
		}
		UIManager.PowerIcon = powers[currentPower].iconPower;
	}

	public void Feed(){
		GameObject interactable = GameSettings.Instance.GetInteractable();
		if(interactable != null && interactable != gameObject){
			BaseCharacter target = interactable.GetComponent<BaseCharacter>();
			if(target != null){
				if(charAnimator != null)
					charAnimator.SetTrigger("Feed");

				float bloodDrained = 0.05f;

				if(target.species == Species.humanoid){
					bloodDrained = .1f;
				}

				target.AdjustVitae(-bloodDrained);
				AdjustVitae(bloodDrained, GameModel.Instance.drinkClip);
			}
		}
	}

	public void UseCurrentPower(){
		if(!_isSwimming){
			Power curPower = null;

			if(currentPower < powers.Count)
				curPower = powers[currentPower];
			
			if(curPower != null){
				if(vitae - curPower.CostVitae > 0.25f){
					curPower.Attack();
					AdjustVitae(-curPower.CostVitae, curPower.audioFx);
				} else {
					UIManager.Notify("You can't use all your blood");
				}
			}
		} else {
			UIManager.Notify("You can't use your powers in water");
		}
	}

	public void ActivateCharModel(bool on){
		if(charModel != null)
			charModel.SetActive(on);
	}


//	public void AddPowers(OriginManager.OriginEnum newOrigin){
//		Globals.Instance.playerData.charOrigin = newOrigin.ToString();
//		if(newOrigin != OriginManager.OriginEnum.None){
//			kind = BaseCharacter.Kind.vampire;
//			Globals.Instance.playerData.charKind = kind.ToString();
//		}
//
//		powers.Clear();
//
//		foreach(Power power in OriginManager.Instance.GetOrigin(_origin).originSpectrum[0].powers){
//				if(power != null && !powers.Contains(power))
//					powers.Add(power);
//			}
//
//		if(gameObject.CompareTag("Player")) {
//			UIManager.Instance.SetHealthColor(OriginManager.Instance.GetOrigin(_origin).originColor);
//
//			UIManager.Instance.UpdateUI();
//		}
//	}

	public void AddPower(string spectrumName, int level = 1){
		Debug.Log("adding powers until " + level + " for " + spectrumName);
		if(Globals.Instance.playerData.spectrum1 == null || Globals.Instance.playerData.spectrum1 == "") {
			spectrum1 = PowerManager.Instance.GetSpectrum(spectrumName);
			Globals.Instance.playerData.spectrum1 = spectrumName;
		} else {
			if(Globals.Instance.playerData.spectrum2 == null || Globals.Instance.playerData.spectrum2 == "") {
				spectrum2 = PowerManager.Instance.GetSpectrum(spectrumName);
				Globals.Instance.playerData.spectrum2 = spectrumName;
			} else {
				if(Globals.Instance.playerData.spectrum3 == null || Globals.Instance.playerData.spectrum3 == ""){
					spectrum3 = PowerManager.Instance.GetSpectrum(spectrumName);
					Globals.Instance.playerData.spectrum3 = spectrumName;
				} else{
					UIManager.Notify("You cant have more than 3 spectrums");
				}
			}
		}

		for(int l = 0; l < level; l++){
			Debug.Log(PowerManager.Instance.GetSpectrum(spectrumName).powers[l].gameObject.name);
//			if(powers.Contains)
			powers.Add(PowerManager.Instance.GetSpectrum(spectrumName).powers[l]);
		}
	}


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
