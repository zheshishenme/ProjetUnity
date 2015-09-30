#pragma strict
//script pour faire jouer les animations de déplacement de l'avatar en extérieur
var myAnimation: Animator;

function Start () {

}

function Update () {

				
		if((Input.GetKey("up")&&Input.GetKey("down") || Input.GetKey("left")&&Input.GetKey("right")))
		{
			myAnimation.SetBool("marche",false);
			myAnimation.SetBool("recule",false);
			myAnimation.SetBool("droite",false);
			myAnimation.SetBool("gauche",false);
		}

		else if(Input.GetKey("up"))
		{
			myAnimation.SetBool("avance",true);
		}
		else if(Input.GetKey("left"))
		{
			myAnimation.SetBool("gauche",true);
		}
		else if(Input.GetKey("right"))
		{
			myAnimation.SetBool("droite",true);
		}
		else if(Input.GetKey("down"))
		{
			myAnimation.SetBool("recule",true);
		}
		
		else{
			myAnimation.SetBool("avance",false);
			myAnimation.SetBool("droite",false);
			myAnimation.SetBool("gauche",false);
			myAnimation.SetBool("recule",false);
		}
}