using UnityEngine;
using System.Collections;

public class ControlleurJoueur : MonoBehaviour{
	
	#region singleton
	static ControlleurJoueur _instance;
	
	static public bool isActive { 
		get { 
			return _instance != null; 
		} 
	}
	
	static public ControlleurJoueur instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(ControlleurJoueur)) as ControlleurJoueur;
				
				if (_instance == null)
				{
					GameObject go = new GameObject("ControlleurJoueur");
					DontDestroyOnLoad(go);
					_instance = go.AddComponent<ControlleurJoueur>();
				}
			}
			return _instance;
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
	float frequenceGainChangement=8;
	Renderer rendererJauge;

	float timerCamera = -10;
	public float nbVie = 3;
	GameObject camTop;
	GameObject camFP;
	GameObject camOblique;

	Vector3 positionFPJauge = new Vector3(0.28f,50,3.1f);
	Vector3 rotationFPJauge = new Vector3(0.4f, 0, 0);
	Vector3 scaleFPJauge = new Vector3(3f, 15, 1);

	Vector3 positionFPVie  = new Vector3(0.25f,37,3.07f);
	Vector3 rotationFPVie = new Vector3(0.42f,0,0);
	Vector3 scaleFPVie = new Vector3(3,15,1);


	Vector3 positionTopJauge = new Vector3(1.1f,488,39.3f);
	Vector3 rotationTopJauge = new Vector3(3, 0, 0);
	Vector3 scaleTopJauge = new Vector3(6.61f, 17.83f, 3.56f);

	Vector3 positionTopVie = new Vector3(1f,475,37.4f);
	Vector3 rotationTopVie = new Vector3(3, 0, 0);
	Vector3 scaleTopVie = new Vector3(6.29f, 31.47f, 2.1f);

	Vector3 positionObliqueJauge = new Vector3(0.28f,144,1.44f);
	Vector3 rotationObliqueJauge = new Vector3(3.15f, 0, 0);
	Vector3 scaleObliqueJauge = new Vector3(3.151f, 8.5f, 1.7f);
	
	Vector3 positionObliqueVie = new Vector3(0.2f,131,1.43f);
	Vector3 rotationObliqueVie = new Vector3(3.1f, 0, 0);
	Vector3 scaleObliqueVie = new Vector3(3f, 15f, 1f);


	void Start () {

		camTop = GameObject.Find("CameraTop");
		camFP = transform.Find("tete").gameObject.transform.Find("CameraFP").gameObject;
		camOblique = transform.Find("tete").gameObject.transform.Find("CameraOblique").gameObject;
		maxJauge=1;
		nbChangementsDispo=1;
		scriptMouvement = gameObject.transform.Find("tete").GetComponent<mouvement>();
		//scriptMouvement = gameObject.GetComponent<mouvement>();
		controleur = ControlleurJeu.instance;
		jauge =transform.Find("tete").gameObject.transform.Find("jaugeMouvementsDispos").gameObject;
		rendererJauge = jauge.GetComponent<Renderer>();
	}
	
	void Update(){

		rendererJauge.material.SetFloat ("_Cutoff", (float)(1f - (float)nbChangementsDispo/(float)maxJauge));

		barreVie.GetComponent<Renderer>().material.mainTextureScale = new Vector2(nbVie,1);
		barreVie.transform.localScale = new Vector3(nbVie,barreVie.transform.localScale.y,barreVie.transform.localScale.z);

		#region input joueur
		if(controleur.gameStarted){
			if (nbChangementsDispo >=0){
				if(nbChangementsDispo<=1){
					nbChangementsDispo += Time.deltaTime/frequenceGainChangement;
				}
			}
			
			
			if(gameObject.name =="Joueur" ||gameObject.name =="ThirdPersonController" ){
				gameObject.transform.Find("tete").GetComponent<mouvement>().trait = Resources.Load("traitRouge") as GameObject;
				if(Input.GetKey(KeyCode.RightArrow) && controleur.recordInput){
					//scriptMouvement.avance= false;
					if(controleur.touchesInversees){
						scriptMouvement.tourneGauche();
					}
					else{
						scriptMouvement.tourneDroite();
					}
					
				}
				
				else if(Input.GetKey(KeyCode.LeftArrow) && controleur.recordInput){
					//scriptMouvement.avance= false;
					if(controleur.touchesInversees)
					{
						scriptMouvement.tourneDroite();
					}
					else{
						scriptMouvement.tourneGauche();
					}
					
				}
				
				else if(Input.GetKey(KeyCode.UpArrow) && controleur.recordInput&& peutMonterDescendre() ){
					/*if(controleur.touchesInversees && scriptMouvement.peuxDescendre()){
						reduireChangementsDispo();
						controleur.recordInput = false;
						scriptMouvement.bas();
						//scriptMouvement.avance= false;
						scriptMouvement.avance= true;
						scriptMouvement.descend = true;

					}

					else*/ if(!controleur.touchesInversees /*&& scriptMouvement.peuxMonter() */){
						reduireChangementsDispo();
						controleur.recordInput = false;
						scriptMouvement.doitSauter = true;
						scriptMouvement.orienteMontee();
						//scriptMouvement.avance= false;
						scriptMouvement.avance= true;
						scriptMouvement.doitMonter = true;
						
					}
					
				}
				
				else if(Input.GetKey(KeyCode.DownArrow) && controleur.recordInput && peutMonterDescendre()){
					if(controleur.touchesInversees /*&& scriptMouvement.peuxMonter()*/){
						reduireChangementsDispo();
						controleur.recordInput = false;
						scriptMouvement.doitSauter = true;
						scriptMouvement.orienteMontee();
						//scriptMouvement.avance= false;
						scriptMouvement.avance= true;
						scriptMouvement.doitMonter = true;
						
					}
					/*else if(!controleur.touchesInversees && scriptMouvement.peuxDescendre()){
						reduireChangementsDispo();
						controleur.recordInput = false;
						scriptMouvement.bas();
						//scriptMouvement.avance= false;
						scriptMouvement.avance= true;
						scriptMouvement.descend = true;

					}*/
				}
				else{
					scriptMouvement.avance= true;
				}
			}
			
			
			
			
			/*else if(gameObject.name == "Joueur2"){
				gameObject.transform.Find("tete").GetComponent<mouvement>().trait = Resources.Load("traitBleu") as GameObject;
				if(Input.GetKey(KeyCode.D)&& controleur.recordInput){

					scriptMouvement.tourneDroite();
				}
				
				if(Input.GetKey(KeyCode.Q)&& controleur.recordInput){
					scriptMouvement.tourneGauche();
				}

				if(Input.GetKey(KeyCode.Z)&& controleur.recordInput){
					ControlleurJeu.GetInstance().recordInput = false;
					scriptMouvement.avance= true;
					scriptMouvement.doitMonter = true;
					scriptMouvement.orienteMontee();
				}

				if(Input.GetKey(KeyCode.S)&& controleur.recordInput){
					ControlleurJeu.GetInstance().recordInput = false;
					scriptMouvement.avance= true;
					scriptMouvement.doitDescendre = true;
					scriptMouvement.orienteDescente();
				}
				
			}*/
		}
		#endregion

		#region timer
		if(timerCamera != -10){
			timerCamera -= Time.deltaTime;
			if(timerCamera<=0){
				timerCamera = -10;
				returnCamInitiale();
			}

		}
		#endregion 

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

	/// <summary>
	/// Autodestruction 
	/// </summary>
	public void detruire(){
		Destroy(gameObject);
	}

	public void enlever1Vie(){
		nbVie--;
	}
	public void ajouter1Vie(){
		if(nbVie<3){
			nbVie++;
		}
	}

	public void changeCamera(){
		timerCamera = 10;
		switch (Random.Range(0,2)){
			case 0:
				desactiverCameraOblique();
				desactiverCameraTop();
				activerCameraFP();
				break;
			case 1:
				desactiverCameraOblique();
				desactiverCameraFP();
				activerCameraTop();
				break;
		}

	}

	void returnCamInitiale(){
		desactiverCameraFP();
		desactiverCameraTop();
		activerCameraOblique();
	}

	#region changement etat caméra

	void desactiverCameraOblique(){
		camOblique.SetActive(false);
	}
	
	void activerCameraOblique(){
		positionOblique();
		camOblique.SetActive(true);
	}
	
	void desactiverCameraFP(){
		camFP.SetActive(false);
	}
	
	void activerCameraFP(){
		positionFP();
		camFP.SetActive(true);
	}
	
	void desactiverCameraTop(){
		camTop.GetComponent<Camera>().enabled = false;
	}
	
	void activerCameraTop(){
		positionTop();
		camTop.GetComponent<Camera>().enabled = true;
	}


	
	void positionTop(){
		jauge.transform.localPosition= positionTopJauge;
		jauge.transform.localEulerAngles = rotationTopJauge ;
		jauge.transform .localScale = scaleTopJauge;
		
		barreVie.transform.localPosition= positionTopVie;
		barreVie.transform.localEulerAngles = rotationTopVie ;
		barreVie.transform .localScale = scaleTopVie;
	}
	
	void positionFP(){
		jauge.transform.localPosition= positionFPJauge;
		jauge.transform.localEulerAngles = rotationFPJauge ;
		jauge.transform .localScale = scaleFPJauge;
		
		barreVie.transform.localPosition= positionFPVie;
		barreVie.transform.localEulerAngles = rotationFPVie ;
		barreVie.transform .localScale = scaleFPVie;
	}
	
	void positionOblique(){
		jauge.transform.localPosition= positionObliqueJauge;
		jauge.transform.localEulerAngles = rotationObliqueJauge;
		jauge.transform .localScale = scaleObliqueJauge;
		
		barreVie.transform.localPosition=positionObliqueVie;
		barreVie.transform.localEulerAngles = rotationObliqueVie;
		barreVie.transform .localScale = scaleObliqueVie;
	}

	#endregion

}
