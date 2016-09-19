using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

public class MailManager : MonoBehaviour {

	public Text header;
	public Text content;

	public List<Quest> questsAnger = new List<Quest>();
	public List<Quest> questsEnvy = new List<Quest>();
	public List<Quest> questsGluttony = new List<Quest>();
	public List<Quest> questsGreed= new List<Quest>();
	public List<Quest> questsLust = new List<Quest>();
	public List<Quest> questsPride = new List<Quest>();
	public List<Quest> questsSloth = new List<Quest>();

	private Dropdown _filter;
	int totalStep;

	private static MailManager instance;
	public static MailManager Instance {
		get { if(instance == null) {
				instance = GameObject.FindObjectOfType<MailManager>();
			}
			return instance;
		}

		set { instance = value; }
	}

	void Start(){
		_filter = GetComponentInChildren<Dropdown>();
		InitQuests();
	}

	void InitQuests(){
		Quest[] quests = FindObjectsOfType<Quest>();

		foreach(Quest quest in quests){
			switch(quest.conviction){
			case Globals.Conviction.anger:
				questsAnger.Add(quest);
				break;
			case Globals.Conviction.envy:
				questsEnvy.Add(quest);
				break;
			case Globals.Conviction.gluttony:
				questsGluttony.Add(quest);
				break;
			case Globals.Conviction.greed:
				questsGreed.Add(quest);
				break;
			case Globals.Conviction.lust:
				questsLust.Add(quest);
				break;
			case Globals.Conviction.pride:
				questsPride.Add(quest);
				break;
			default:
				questsSloth.Add(quest);
				break;
			}
		}
	}

	public void DisplayEmail(){
		Globals.Conviction conviction = (Globals.Conviction)_filter.value;
		Quest lastestClue = GetLatestConvictionClue(conviction);

		if(lastestClue != null){
			_filter.captionText.text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(conviction.ToString()) + " (" + lastestClue.step +  "/" + totalStep + ")";
			header.text = lastestClue.header;
			content.text = lastestClue.content;
		} else {
			_filter.captionText.text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(conviction.ToString()) + " (0/" + totalStep + ")";
			header.text = "No quest";
			content.text = "No quest";
		}
	}

	public void DebugIncrementCurrProg(){
		Globals.Conviction conviction = (Globals.Conviction)_filter.value;
		Quest lastestClue = GetLatestConvictionClue(conviction);
		Globals.IncrementProgress(conviction);
		if(Globals.GetConvictionProgress(conviction) > totalStep)
			Globals.SetConvictionProgress(conviction);
		DisplayEmail();
	}

	Quest GetLatestConvictionClue(Globals.Conviction conviction){
		Quest latestClue = null;
		int step = Globals.GetConvictionProgress(conviction);
		switch(conviction){
		case Globals.Conviction.anger:
			latestClue = questsAnger.Find(obj => obj.step == step);
			totalStep = questsAnger.Count;
			break;
		case Globals.Conviction.envy:
			latestClue = questsEnvy.Find(obj => obj.step == step);
			totalStep = questsEnvy.Count;
			break;
		case Globals.Conviction.gluttony:
			latestClue = questsGluttony.Find(obj => obj.step == step);
			totalStep = questsGluttony.Count;
			break;
		case Globals.Conviction.greed:
			latestClue = questsGreed.Find(obj => obj.step == step);
			totalStep = questsGreed.Count;
			break;
		case Globals.Conviction.lust:
			latestClue = questsLust.Find(obj => obj.step == step);
			totalStep = questsLust.Count;
			break;
		case Globals.Conviction.pride:
			latestClue = questsPride.Find(obj => obj.step == step);
			totalStep = questsPride.Count;
			break;
		case Globals.Conviction.sloth:
			latestClue = questsSloth.Find(obj => obj.step == step);
			totalStep = questsSloth.Count;
			break;
		}
		return latestClue;
	}
}
