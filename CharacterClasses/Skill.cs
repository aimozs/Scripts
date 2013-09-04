public class Skill : ModifiedStat {
	private bool _known;
	
	public Skill() {
		_known = false;
		ExpToLevel = 25;
		LevelModifier = 1.1f;
	}
	
	public bool Known{
		get { return _known; }
		set { _known = value; }
	}
}


public enum SkillName {

	//physic
		athleticsPS,
		brawlPS,
		drivePS,
		firearmsPS,
		larcenyPS,
		stealthPS,
		survivalPS,
		weaponryPS,
	
	//mental
		academicsMS,
		computerMS,
		craftsMS,
		investigationMS,
		medicineMS,
		occultMS,
		politicsMS,
		scienceMS,

				
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