using UnityEngine;
using UnityEngine.UI;

public class Vital : MonoBehaviour
{
	public enum VitalName{Health, Humanity, Vitae};
	public VitalName name;
	public Slider sliderUI;
	public float value;
}