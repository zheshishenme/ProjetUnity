using UnityEngine;
using System.Collections;

public class ControlleurJeu : MonoBehaviour {

	#region singleton
	static ControlleurJeu _instance;
	
	static public bool isActive { 
		get { 
			return _instance != null; 
		} 
	}
	
	static public ControlleurJeu instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(ControlleurJeu)) as ControlleurJeu;
				
				if (_instance == null)
				{
					GameObject go = new GameObject("ControlleurJeu");
					DontDestroyOnLoad(go);
					_instance = go.AddComponent<ControlleurJeu>();
				}
			}
			return _instance;
		}
	}


	#endregion

	public enum Etat{Neutre, Excitee, Victoire, Danger, Aide};

	float tempsInitiale = 60;
	float tempsRestant = 0;
	public bool gameStarted = false;
	bool ask = false;
	public bool recordInput = true;
	public bool touchesInversees = false;
	bool finJeu = false;

	void Start(){
		createNewPlayer();
	}
	
	void Update () {
		if(gameStarted){
			tempsRestant -= Time.deltaTime;

			///// test ///
		 	/*
			GameObject joueur = GameObject.Find("Joueur");
			joueur.transform.Find("tete").GetComponent<mouvement>().traceTrait = false;
			*/
			///// fin test ////

			/*if(!ControllerBonus.Instance.bonusSpeedUpHere){
				ControllerBonus.Instance.creerSpeedUpBonus();
			}
			if(!ControllerBonus.Instance.bonusDarkHere){
				ControllerBonus.Instance.creerDarkBonus();
			}*/

			if(tempsRestant<=0){
				finJeu=true;
				ask = true;
			}
		}
		else{
			if(Input.GetKey(KeyCode.Space)){
				gameStarted = true;
				recordInput = true;
			}
			tempsRestant = tempsInitiale;
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
			if(GUI.Button(new Rect(Screen.width/2 -40, Screen.height/2-20,250,60),"Recommencer")){

				// destruction des explosions
				foreach ( GameObject obj in GameObject.FindGameObjectsWithTag("effetExplose")){
					Destroy(obj);
				}

				// destruction des bonnus 
				foreach ( GameObject obj in GameObject.FindGameObjectsWithTag("goodBonus")){
					Destroy(obj);

				}
				foreach ( GameObject obj in GameObject.FindGameObjectsWithTag("badBonus")){
					Destroy(obj);

				}

				// destruction des joueurs
				foreach ( GameObject obj in GameObject.FindGameObjectsWithTag("joueur")){
					Destroy(obj);
				}

				createNewPlayer();

				//GameObject.Find("sun").GetComponent<Light>().enabled = true;

				gameStarted = false;
				ask = false;
			}
		}
		if(!gameStarted){
			if(GUI.Button(new Rect(Screen.width/2 -40, Screen.height/2+20,250,60),"Appuyer sur espace pour commencer")){
				gameStarted = true;
			}

		}
		


		GUI.Label(new Rect(10,10,200,30), "Temps restant :  " + (int)tempsRestant + " secondes.");

		if(GUI.Button(new Rect(10 , 50 ,60,30),"Quitter")){
			Application.LoadLevel("Menu");
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

	public void ajouterTemps(){
		tempsRestant += 15;
	}
}
