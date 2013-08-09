public class Attribute : BaseStat {

	public Attribute(){
		ExpToLevel = 50;
		LevelModifier = 1.05f;
		
	}
}


public enum AttributeName{
	//mental
		intelligenceMA,
		witsMA,
		resolveMA,
			
	//physic
		strengthPA,
		dexterityPA,
		staminaPA,
				
	//social
		apparenceSA,
		manipulationSA,
		composureSA,

}