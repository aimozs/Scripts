using UnityEngine;
using UnityEngine.UI;

//those two seems to be mandatory to use the Lists
using System.Collections;
using System.Collections.Generic;

public class Clan : MonoBehaviour
{
	private string _nameClan;
	private Image _imageClan;

}


public enum clanName
{
	Daeva,
	Gangrel,
	Mehket,
	Ventrue,
	Nosferatu
}