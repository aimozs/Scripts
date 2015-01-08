var toggleGUI : boolean;
var preso : Texture;
var presoSize : float;
var screenH : float;

function OnTriggerEnter()
{
    toggleGUI = true;
}

function OnTriggerExit()
{
	toggleGUI = false;
}

function Update()
{
	presoSize = Screen.width*8/10;
	screenH = Screen.height*1/10;
}

function OnGUI ()
{
    if (toggleGUI == true)
    {
    	GUI.DrawTexture(Rect((Screen.width/2-presoSize/2), screenH, presoSize, presoSize), preso, ScaleMode.ScaleToFit, true);
    }
    
}