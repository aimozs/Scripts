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
				
	//mental
		academicsMS,
		computerMS,
		craftsMS,
		investigationMS,
		medicineMS,
		occultMS,
		politicsMS,
		scienceMS,
				
	//physic
		athleticsPS,
		brawlPS,
		drivePS,
		firearmsPS,
		larcenyPS,
		stealthPS,
		survivalPS,
		weaponryPS,
				
	//social
		animalKenSS,
		empathySS,
		expressionSS,
		intimidationSS,
		persuasionSS,
		socializeSS,
		streetwiseSS,
		subterfugeSS


}