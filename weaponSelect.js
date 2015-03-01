#pragma strict


private var weaponPosition : GameObject;
private var activeWeapon : GameObject;
private var weapon : int;
private var player : GameObject;
private var playerAudio : AudioSource;

public var weapon1 : GameObject;
public var weapon2 : GameObject;
public var weapon3 : GameObject;
var arrowSound : AudioClip;
var baseballSound : AudioClip;

private var bullitPrefab : GameObject;
private var spawnPoint : Transform;

function Start ()
{
	weaponPosition = GameObject.Find("weaponPosition");
	spawnPoint = GameObject.Find("spawnPoint").transform;
	player = GameObject.FindWithTag("Player");
	playerAudio = player.GetComponent("AudioSource");
}

function Update ()
{

	if (Input.GetButtonDown("weapon1"))
		{
			Destroy(activeWeapon);
		}
	if (Input.GetButtonDown("weapon2"))
		{
			Destroy(activeWeapon);
			weapon = 2;
			activeWeapon = Instantiate(weapon2);
			activeWeapon.transform.parent = weaponPosition.transform;
		}
	if (Input.GetButtonDown("weapon3"))
		{
			Destroy(activeWeapon);
			weapon = 3;
			bullitPrefab = GameObject.Find("arrow");
			activeWeapon = Instantiate(weapon3);
			activeWeapon.transform.parent = weaponPosition.transform;
		}

	if (activeWeapon != null)
	{
		activeWeapon.transform.position = weaponPosition.transform.position;
		activeWeapon.transform.rotation = weaponPosition.transform.rotation;
	}

	if(Input.GetButtonDown("Fire1"))
	{
		switch (weapon)
		{
			case 2:
	    		playerAudio.PlayOneShot(baseballSound, 0.9F);
	        break;

	    	case 3:
	    		var bullit =  Instantiate(bullitPrefab, spawnPoint.position, spawnPoint.rotation);
				bullit.rigidbody.velocity = spawnPoint.TransformDirection(Vector3(0,-100,0));
				Destroy (bullit, 5);
				playerAudio.PlayOneShot(arrowSound, 0.1F);
	        break;
		}
	}
}