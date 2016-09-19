using UnityEngine;
using UnityEngine.UI;

public class Vital : MonoBehaviour
{
	public enum VitalName{Health, Humanity, Vitae};
	public VitalName vitalName;
	public Slider sliderUI;
	public float value;
}