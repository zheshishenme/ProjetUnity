using UnityEngine;
using System.Collections;

public class ControlleurJeu : MonoBehaviour {

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

	void OnGUI(){
		if(ask){
			if(GUI.Button(new Rect(Screen.width/2 -40, Screen.height/2-20,100,60),"Recommencer")){

				foreach ( GameObject obj in GameObject.FindGameObjectsWithTag("effetExplose")){
					Destroy(obj);
				}
				foreach ( GameObject obj in GameObject.FindGameObjectsWithTag("joueur")){
					Destroy(obj);
				}
				GameObject joueur1 = Resources.Load("Joueur")as GameObject;
				GameObject joueur2 = Resources.Load("Joueur2")as GameObject;
				
				GameObject go = Instantiate(joueur1,joueur1.transform.position, joueur1.transform.rotation) as GameObject;
				go.name = "Joueur";

				go = Instantiate(joueur2,joueur2.transform.position, joueur2.transform.rotation) as GameObject;
				go.name = "Joueur2";
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
}
