#pragma strict

public var playerPos : Transform;
public var jack : GameObject;

function Start ()
{
	playerPos = GameObject.FindGameObjectWithTag("Player").transform;
}

function Update ()
{

}

function SpawnJack()
{
	Instantiate(jack, playerPos.position, playerPos.rotation);
}