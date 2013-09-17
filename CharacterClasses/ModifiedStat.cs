using System.Collections.Generic;

public class ModifiedStat : BaseStat{
	private List<ModifyingAttribute> _mods;		//a list of attribute that modify the stat
	private int _modValue;						// the amount added the baseValue from the modifiers
	
	public ModifiedStat(){
		_mods = new List<ModifyingAttribute>();
		_modValue = 0;
	}
	
	public void AddModifier(ModifyingAttribute mod) {
		_mods.Add(mod);
	}
	
	private void CalculateModValue(){
		_modValue = 0;
		
		if(_mods.Count >0)
			foreach(ModifyingAttribute att in _mods)
				_modValue += (int)(att.attribute.AdjustedBaseValue * att.ratio);
	}
	
	public new int AdjustedBaseValue{
		get{ return BaseValue + BuffValue + _modValue; }
	}
	
	public void Update() {
		CalculateModValue();
	
	}
	
	public string GetModifyingAttributes() {
		string temp = "";
		
//		UnityEngine.Debug.Log(_mods.Count);
		for (int cnt = 0; cnt < _mods.Count; cnt++) {
			temp += _mods[cnt].attribute.Name;
			temp += "_";
			temp += _mods[cnt].ratio;
			
			if(cnt < _mods.Count - 1)
				temp += "|";
			
			
		}
		UnityEngine.Debug.Log(temp);
		return temp;
	}
	
}


public struct ModifyingAttribute {
	public Attribute attribute;
	public float ratio;
	
	public ModifyingAttribute(Attribute att, float rat) {
		attribute = att;
		ratio = rat;
	}
}