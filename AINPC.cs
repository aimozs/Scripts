using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class AINPC : MonoBehaviour
{
	
	public GameObject T;
	// Cached variables
	CharacterController _controller;
	Transform _transform;
	//Movement variables
	public float Speed = 5f;
	float gravity = 100f;
	Vector3 moveDirection;
	Vector3 _target;
	float maxRotSpeed = 200.0f;
	float minTime = 0.1f;
	float _velocity;
	bool change;
	float range;
	
	void Start()
	{
		_controller = GetComponent<CharacterController>();
		_transform = GetComponent<Transform>();
		
		range = 2f;
		_target = GetTarget();
		Instantiate(T, _target, Quaternion.identity);
	}
	
	void Update()
	{
		//        if (change)
		//		{
		//			_target = GetTarget();
		//			Instantiate(T, _target, Quaternion.identity);
		//			Debug.Log ("target changed");
		//			change = false;
		//		}
		
		if (Vector3.Distance(_transform.position, _target) > range)
		{
			Move();
			animation.CrossFade("walk");
			//			Debug.Log ("walking toward target");
		}
		else
		{
			_target = GetTarget();
			Instantiate(T, _target, Quaternion.identity);
			//			InvokeRepeating("NewTarget", 0.01f, 5.0f);
		}
	}
	
	void Move()
	{
		// Movement
		moveDirection = _transform.forward;
		moveDirection *= Speed;
		moveDirection.y -= gravity * Time.deltaTime;
		_controller.Move(moveDirection * Time.deltaTime);
		//Rotation
		var newRotation = Quaternion.LookRotation(_target - _transform.position).eulerAngles;
		var angles = _transform.rotation.eulerAngles;
		_transform.rotation = Quaternion.Euler(angles.x,
		                                       Mathf.SmoothDampAngle(angles.y, newRotation.y, ref _velocity, minTime, maxRotSpeed),
		                                       angles.z);
	}
	
	Vector3 GetTarget()
	{
		return new Vector3(Random.Range(_transform.position.x - 20, _transform.position.x + 20), Random.Range(_transform.position.y - 1, _transform.position.y + 1), Random.Range(_transform.position.z - 20, _transform.position.z + 20));
	}
	
	//    void NewTarget()
	//    {
	//        int choice = Random.Range(0, 2);
	//        switch (choice)
	//        {
	//            case 2: //new target
	//                change = true;
	//                break;
	//            case 1: //still walking toward old target
	//                change = false;
	//                break;
	//            case 0: //waiting, staying at same position
	//                _target = transform.position;
	//                break;
	//        }
	//    }
}
