using System.Collections.Generic

public class ModifiedStat : BaseStat{
	private List<ModifyingAttribute> _mods;		//a list of attribute that modify the stat
	private int _modValue;						// the amount added the baseValue from the modifiers
	
	public ModifiedStat(){
		_mods = new List<ModifyingAttribute>();
		_modValue = 0;
	}
	
	public void AddModifier( ModifyingAttribute mod) {
		_mods.Add(ModifiedStat)
	}
	
	private void CalculateModValue(){
		_modValue = 0;
		
		if(_mods.Count >0)
			foreach(ModifyingAttribute att in _mods)
				_modValue += (int)(att.attribute.AdjustedValue * att.ratio);
	}
}


public struct ModifyingAttribute {
	public Attribute attribute;
	public float ratio;
}