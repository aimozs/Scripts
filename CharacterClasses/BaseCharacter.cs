using UnityEngine;
using System.Collections;
using System;					// added to access the enum class

public class BaseCharacter : MonoBehaviour {
	
	
	private string _name;
	private int _level;
	private uint _freeExp;
	
	private Attribute[] _primaryAttribute;
	private Vital[] _vital;
	private Skill[] _skill;
	
	public void Awake() {
		_name = string.Empty;
		_level = 0;
		_freeExp = 0;
		
		_primaryAttribute = new Attribute[Enum.GetValues(typeof(AttributeName)).Length];
		_vital = new Vital[Enum.GetValues(typeof(VitalName)).Length];
		_skill = new Skill[Enum.GetValues(typeof(SkillName)).Length];
	
		SetupPrimaryAttribute();
		SetupVital();
		SetupSkill();
	}
	
	public string Name {
		get { return _name; }
		set { _name = value; }
	}
	
	public int Level {
		get { return _level; }
		set { _level = value; }
	}
	
	public uint FreeExp {
		get { return _freeExp; }
		set { _freeExp = value; }
	}
	
	public void AddExp(uint exp) {
		_freeExp += exp;
		
		CalculateLevel();
	}
	
	
	// take the average of the players skill and assign that as the player level
	
	public void CalculateLevel() {
		
	}
	
//setup the basic value	
	private void SetupPrimaryAttribute() {
		for(int cnt = 0; cnt < _primaryAttribute.Length; cnt++) {
			_primaryAttribute[cnt] = new Attribute(); }
	}
	
	private void SetupVital() {
		for(int cnt = 0; cnt < _vital.Length; cnt++) {
			_vital[cnt] = new Vital(); }
		
		SetupVitalModifiers();
	}
	
	private void SetupSkill() {
		for(int cnt = 0; cnt < _skill.Length; cnt++) {
			_skill[cnt] = new Skill(); }
		
		SetupSkillModifiers();
	}
	
//Getters for base values
	public Attribute GetPrimaryAttribute (int index) {
		 return _primaryAttribute[index];
	}
	
	public Vital GetVital (int index) {
		 return _vital[index];
	}
	
	public Skill GetSkill (int index) {
		 return _skill[index];
	}
	
//modify vitals, based on attributes
	private void SetupVitalModifiers() {
		
		/*ModifyingAttribute health = new ModifyingAttribute();
		health.attribute = GetPrimaryAttribute((int)AttributeName.staminaPA);
		health.ratio = .5f;
		GetVital ((int)VitalName.Health).AddModifier(health);
		*/
		
		//health
		GetVital((int)VitalName.Health).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.staminaPA), ratio = 1});
		//humanity
		GetVital((int)VitalName.Humanity).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.intelligenceMA), ratio = 1});
		//vitae
		GetVital((int)VitalName.Vitae).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.dexterityPA), ratio = 1});
		
		
		/*/Quick version but doesnt work\
		GetVital((int)VitalName.Health).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.staminaPA), .5f));
		GetVital((int)VitalName.Humanity).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)SkillName.empathySS), 1));
		GetVital((int)VitalName.Vitae).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)SkillName.occultMS), 1));*/
	}

//modify skills, based on attributes
	private void SetupSkillModifiers() {
		
		//physic
		GetSkill((int)SkillName.athleticsPS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.strengthPA), ratio = 1});
		GetSkill((int)SkillName.brawlPS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.staminaPA), ratio = 1});
		GetSkill((int)SkillName.drivePS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.dexterityPA), ratio = 1});
		GetSkill((int)SkillName.firearmsPS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.dexterityPA), ratio = 1});
		GetSkill((int)SkillName.larcenyPS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.dexterityPA), ratio = 1});
		GetSkill((int)SkillName.stealthPS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.dexterityPA), ratio = 1});
		GetSkill((int)SkillName.survivalPS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.staminaPA), ratio = 1});
		GetSkill((int)SkillName.weaponryPS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.strengthPA), ratio = 1});
		
		//mental
		GetSkill((int)SkillName.academicsMS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.witsMA), ratio = 1});
		GetSkill((int)SkillName.computerMS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.resolveMA), ratio = 1});
		GetSkill((int)SkillName.craftsMS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.intelligenceMA), ratio = 1});
		GetSkill((int)SkillName.investigationMS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.resolveMA), ratio = 1});
		GetSkill((int)SkillName.medicineMS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.intelligenceMA), ratio = 1});
		GetSkill((int)SkillName.occultMS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.witsMA), ratio = 1});
		GetSkill((int)SkillName.politicsMS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.witsMA), ratio = 1});
		GetSkill((int)SkillName.scienceMS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.resolveMA), ratio = 1});
		
		//social
		GetSkill((int)SkillName.animalKenSS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.composureSA), ratio = 1});
		GetSkill((int)SkillName.empathySS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.composureSA), ratio = 1});
		GetSkill((int)SkillName.expressionSS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.manipulationSA), ratio = 1});
		GetSkill((int)SkillName.intimidationSS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.apparenceSA), ratio = 1});
		GetSkill((int)SkillName.persuasionSS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.manipulationSA), ratio = 1});
		GetSkill((int)SkillName.socializeSS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.apparenceSA), ratio = 1});
		GetSkill((int)SkillName.streetwiseSS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.composureSA), ratio = 1});
		GetSkill((int)SkillName.subterfugeSS).AddModifier(new ModifyingAttribute { attribute = GetPrimaryAttribute((int)AttributeName.manipulationSA), ratio = 1});
		
	}
	
	public void StatUpdate(){
		for(int cnt = 0; cnt < _vital.Length; cnt ++)
			_vital[cnt].Update();
		
		for(int cnt = 0; cnt < _skill.Length; cnt ++)
			_skill[cnt].Update();
		
	}
}
