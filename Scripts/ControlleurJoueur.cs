using UnityEngine;
using System.Collections;

public class ControlleurJoueur : MonoBehaviour{
	
	#region singleton
	private static ControlleurJoueur instance;
	
	// Static singleton property
	public static ControlleurJoueur Instance
	{
		// Here we use the ?? operator, to return 'instance' if 'instance' does not equal null
		// otherwise we assign instance to a new component and return that
		get { 
			return instance ?? (instance = new GameObject("Singleton Controleur joueur").AddComponent<ControlleurJoueur>()); 
		}
	}
	#endregion

	mouvement scriptMouvement;

	ControlleurJeu controleur;

	GameObject jauge;
	public GameObject barreVie;

	float nbChangementsDispo = 3;
	int maxJauge = 3;

	float timerJauge = -10;
	float frequenceGainChangement=5;
	Renderer rendererJauge;
	public float nbVie = 3;

	void Start () {
		maxJauge=3;
		nbChangementsDispo=3;
		scriptMouvement = gameObject.transform.Find("tete").GetComponent<mouvement>();
		//scriptMouvement = gameObject.GetComponent<mouvement>();
		controleur = ControlleurJeu.Instance;
		rendererJauge = transform.Find("tete").gameObject.transform.Find("jaugeMouvementsDispos").gameObject.GetComponent<Renderer>();

	}

	/*void Awake(){
		controleur = ControlleurJeu.Instance;
 		rendererJauge = jauge.gameObject.GetComponent<Renderer>();
	}*/

	void Update(){

		rendererJauge.material.SetFloat ("_Cutoff", (float)(1f - (float)nbChangementsDispo/(float)maxJauge));

		barreVie.GetComponent<Renderer>().material.mainTextureScale = new Vector2(nbVie,1);
		barreVie.transform.localScale = new Vector3(nbVie,barreVie.transform.localScale.y,barreVie.transform.localScale.z);

		if(controleur.gameStarted){
			if (nbChangementsDispo >=0){
				if(nbChangementsDispo<=3){
					nbChangementsDispo += Time.deltaTime/frequenceGainChangement;
				}
			}


			if(gameObject.name =="Joueur" ||gameObject.name =="ThirdPersonController" ){
				gameObject.transform.Find("tete").GetComponent<mouvement>().trait = Resources.Load("traitRouge") as GameObject;
				if(Input.GetKey(KeyCode.RightArrow) && controleur.recordInput){
					//scriptMouvement.avance= false;
					if(controleur.touchesInversees){
						scriptMouvement.gauche();
					}
					else{
						scriptMouvement.droite();
					}

				}
				
				else if(Input.GetKey(KeyCode.LeftArrow) && controleur.recordInput){
					//scriptMouvement.avance= false;
					if(controleur.touchesInversees)
					{
						scriptMouvement.droite();
					}
					else{
						scriptMouvement.gauche();
					}

				}

				else if(Input.GetKey(KeyCode.UpArrow) && controleur.recordInput&& peutMonterDescendre() ){
					if(controleur.touchesInversees && scriptMouvement.peuxDescendre()){
						reduireChangementsDispo();
						controleur.recordInput = false;
						scriptMouvement.bas();
						//scriptMouvement.avance= false;
						scriptMouvement.avance= true;
						scriptMouvement.descend = true;

					}
					else if(!controleur.touchesInversees && scriptMouvement.peuxMonter() ){
						reduireChangementsDispo();
						controleur.recordInput = false;
						scriptMouvement.haut();
						//scriptMouvement.avance= false;
						scriptMouvement.avance= true;
						scriptMouvement.monte = true;

					}

				}
				else if(Input.GetKey(KeyCode.DownArrow) && controleur.recordInput && peutMonterDescendre()){
					if(controleur.touchesInversees && scriptMouvement.peuxMonter()){
						reduireChangementsDispo();
						controleur.recordInput = false;
						scriptMouvement.haut();
						//scriptMouvement.avance= false;
						scriptMouvement.avance= true;
						scriptMouvement.monte = true;

					}
					else if(!controleur.touchesInversees && scriptMouvement.peuxDescendre()){
						reduireChangementsDispo();
						controleur.recordInput = false;
						scriptMouvement.bas();
						//scriptMouvement.avance= false;
						scriptMouvement.avance= true;
						scriptMouvement.descend = true;

					}
				}
				else{
					scriptMouvement.avance= true;
				}
				
			}

			else if(gameObject.name == "Joueur2"){
				gameObject.transform.Find("tete").GetComponent<mouvement>().trait = Resources.Load("traitBleu") as GameObject;
				if(Input.GetKey(KeyCode.D)&& controleur.recordInput){

					scriptMouvement.droite();
				}
				
				if(Input.GetKey(KeyCode.Q)&& controleur.recordInput){
					scriptMouvement.gauche();
				}

				if(Input.GetKey(KeyCode.Z)&& controleur.recordInput){
					ControlleurJeu.Instance.recordInput = false;
					scriptMouvement.avance= true;
					scriptMouvement.monte = true;
					scriptMouvement.haut();
				}

				if(Input.GetKey(KeyCode.S)&& controleur.recordInput){
					ControlleurJeu.Instance.recordInput = false;
					scriptMouvement.avance= true;
					scriptMouvement.descend = true;
					scriptMouvement.bas();
				}
				
			}
		}
	}

	void reduireChangementsDispo(){
		if(nbChangementsDispo>=1){
			nbChangementsDispo--;
		}
	}
	void augmenterChangementsDispo(){
		if(nbChangementsDispo <= maxJauge-1){
			nbChangementsDispo++;
		}
	}

	bool peutMonterDescendre(){
		if(nbChangementsDispo == 0){
			return false;
		}
		else if(nbChangementsDispo>=1){
			return true;
		} 
		return false;
	}
	
	void OnGUI()
	{
		GUI.Label(new Rect(10,10,200,30), "Changements dispo : " +nbChangementsDispo);
	}

	/// <summary>
	/// Autodestruction 
	/// </summary>
	public void detruire(){
		Destroy(gameObject);
	}

	public void enlever1Vie(){
		nbVie--;
	}
}
