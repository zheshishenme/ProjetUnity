using UnityEngine;
using System.Collections;

public class ControlleurJeu : MonoBehaviour {

	#region singleton
	private static ControlleurJeu instance;
	
	// Static singleton property
	public static ControlleurJeu Instance
	{
		// Here we use the ?? operator, to return 'instance' if 'instance' does not equal null
		// otherwise we assign instance to a new component and return that
		get { 
			return instance ?? (instance = new GameObject("Singleton Controleur Jeu").AddComponent<ControlleurJeu>()); 
		}
	}
	#endregion


	float timeFromStart = 0;
	float timer = 0;
	public bool gameStarted = false;
	bool ask = false;
	public bool recordInput = true;
	public bool touchesInversees = false;
	
	void Update () {
		if(gameStarted){
			timeFromStart += Time.deltaTime;
			timer+= Time.deltaTime;

			if(timer >= 5){

				///// test ///
			 	/*
				GameObject joueur = GameObject.Find("Joueur");
				joueur.transform.Find("tete").GetComponent<mouvement>().traceTrait = false;
				*/
				///// fin test ////

				if(!ControllerBonus.Instance.bonusSpeedUpHere){
					ControllerBonus.Instance.creerSpeedUpBonus();
				}
				if(!ControllerBonus.Instance.bonusDarkHere){
					ControllerBonus.Instance.creerDarkBonus();
				}
				timer = 0;
			}
		}
		else{
			if(Input.GetKey(KeyCode.Space)){
				gameStarted = true;
			}
			timeFromStart = 0;
		}

	}

	/// <summary>
	/// Asks the new game.
	/// </summary>
	public void askNewGame(){
		ask = true;
	}

	/// <summary>
	/// Pour l'affichage à l'écran
	/// </summary>
	void OnGUI(){
		if(ask){

			//bouton recommencer
			if(GUI.Button(new Rect(Screen.width/2 -40, Screen.height/2-20,100,60),"Recommencer")){

				// destruction des explosions
				foreach ( GameObject obj in GameObject.FindGameObjectsWithTag("effetExplose")){
					Destroy(obj);
				}

				// destruction des bonnus 
				foreach ( GameObject obj in GameObject.FindGameObjectsWithTag("bonus")){
					Destroy(obj);
					ControllerBonus.Instance.bonusSpeedUpHere = false;
				}

				// destruction des joueurs
				foreach ( GameObject obj in GameObject.FindGameObjectsWithTag("joueur")){
					Destroy(obj);
				}

				createNewPlayer();

				GameObject.Find("sun").GetComponent<Light>().enabled = true;

				gameStarted = false;
				ask = false;
			}
		}
		if(!gameStarted){
			if(GUI.Button(new Rect(Screen.width/2 -40, Screen.height/2+20,250,60),"Appuyer sur espace pour commencer")){
				gameStarted = true;
			}
		}
	}

	/// <summary>
	/// Création des nouveaux joueurs (fontion à optimiser)
	/// </summary>
	void createNewPlayer(){
		//chargement des prefabs
		GameObject joueur1 = Resources.Load("Joueur")as GameObject;
		//GameObject joueur2 = Resources.Load("Joueur2")as GameObject;

		//instantiation dans la scène
		GameObject go = Instantiate(joueur1,joueur1.transform.position, joueur1.transform.rotation) as GameObject;
		go.name = "Joueur";
		
		//go = Instantiate(joueur2,joueur2.transform.position, joueur2.transform.rotation) as GameObject;
		//go.name = "Joueur2";
	}
}
