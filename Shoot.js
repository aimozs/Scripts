var bullitPrefab : GameObject;
var spawnPoint : Transform;


function Awake () {
		
}

function Update () {

	if(Input.GetButtonDown("Fire1"))
	{
		var bullit =  Instantiate(bullitPrefab, GameObject.Find("spawnPoint").transform.position, GameObject.Find("spawnPoint").transform.rotation);
		bullit.rigidbody.velocity = spawnPoint.TransformDirection(Vector3(0,-100,0));
	}
	

}