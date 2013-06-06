using UnityEngine;
using System.Collections;
using System.Collections.Generic;
	
public class Targeting : MonoBehaviour {
	public List<Transform> targets;
	public Transform selectedTarget;
	
	private Transform myTransform;

	// Use this for initialization
	void Start () {
		targets = new List<Transform>();
		selectedTarget = null;
		myTransform = transform;
		
		AddAllEnemies();
		
	
	}
	
	
	public void AddAllEnemies()
	{
		GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");
		
		foreach(GameObject enemy in go)
			AddTarget(enemy.transform);
	}
	
	//create a list of enemies
	public void AddTarget(Transform enemy)
	{
		targets.Add(enemy);
	}
	
	//sort the list "targets" from the closest one to the furthest
	private void SortTargetByDistance()
	{
		targets.Sort(delegate(Transform t1, Transform t2) {
			return Vector3.Distance(t1.position, myTransform.position).CompareTo(Vector3.Distance(t2.position, myTransform.position));
			});
	}
	
	//when pressing "keyCode.Tab" line Update+1 select the first item of list "targets"
	private void TargetEnemy()
	{
		if(selectedTarget == null)
		{
			SortTargetByDistance();
			selectedTarget = targets[0];
		}
		else
		{
			int index = targets.IndexOf(selectedTarget);
			
			if(index < targets.Count - 1)
			{
				index++;
			}
			else
			{
				index = 0;
			}
			DeselectTarget();
			selectedTarget = targets[index];
			
		}
		SelectTarget();
	}
	
	
	private void SelectTarget()
	{
		Debug.DrawLine(selectedTarget.position, myTransform.position, Color.red);
	
	}
	
	private void DeselectTarget()
	{
		Debug.DrawLine(selectedTarget.position, myTransform.position, Color.blue);
		selectedTarget = null;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Tab))
		{
			TargetEnemy();
		}
	
	}
}