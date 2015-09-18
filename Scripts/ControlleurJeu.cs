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
			return instance ?? (instance = new GameObject("Singleton").AddComponent<ControlleurJeu>()); 
		}
	}
	#endregion
	
	public bool start = false;
	bool ask = false;

	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Space)){
			start = true;
		}
	}

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

				// destruction des joueurs
				foreach ( GameObject obj in GameObject.FindGameObjectsWithTag("joueur")){
					Destroy(obj);
				}

				createNewPlayer();

				start = false;
				ask = false;
			}
		}
		if(!start){
			if(GUI.Button(new Rect(Screen.width/2 -40, Screen.height/2+20,250,60),"Appuyer sur espace pour commencer")){
				start = true;
			}
		}
	}

	/// <summary>
	/// Création des nouveaux joueurs (fontion à optimiser)
	/// </summary>
	void createNewPlayer(){
		//chargement des prefabs
		GameObject joueur1 = Resources.Load("Joueur")as GameObject;
		GameObject joueur2 = Resources.Load("Joueur2")as GameObject;

		//instantiation dans la scène
		GameObject go = Instantiate(joueur1,joueur1.transform.position, joueur1.transform.rotation) as GameObject;
		go.name = "Joueur";
		
		go = Instantiate(joueur2,joueur2.transform.position, joueur2.transform.rotation) as GameObject;
		go.name = "Joueur2";
	}
}
