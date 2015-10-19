using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class deplacementFourmi : MonoBehaviour {


	float compteurPheromone=0;

	public List<GameObject> traineePheromone;

	float speed = 0.1f;

	float frequenceChangementDirection = 1;
	float maxFrequenceChangementDirection = 3;
	int minEcartAngle = 20;
	int maxEcartAngle = 50;

	float timer = 0;
	float timerPheromone = 0;
	float freqPheromone = 2;

	bool moveRandom = true;
	public bool doitTourner = false;
	public bool stop = false;
	public bool etoileTrouvee = false;
	public bool followExcite = false;

	public int evitement;

	public GameObject monEtoile;
	public GameObject pheromoneTrouve;
	public GameObject pheromoneLePlusExcite;

	public List<GameObject> listePheromonesExcitesAutour;

	public ControllerFourmis.Type monEtat;

	public bool rechercheMeilleurExcite = false;

	void Start(){
		traineePheromone = new List<GameObject>();
		listePheromonesExcitesAutour = new List<GameObject>();
		monEtat = ControllerFourmis.Type.Neutre;
	}
	
	// Update is called once per frame
	void Update () {
		if(stop){
			if(doitTourner){
				choixRotationEvitement(evitement);
				timer = 0;
			}
			if(rechercheMeilleurExcite){
				pheromoneLePlusExcite = chercheMeilleurExcite(ref listePheromonesExcitesAutour);
				stop = false;
				followExcite = true;
			}

		}

		else{
			timer += Time.deltaTime;
			timerPheromone+=Time.deltaTime;

			if (etoileTrouvee){
				deplacementVersEtoile();
			}
			else if(followExcite){
				deplacementExcite();
			}
			else if(moveRandom){
				deplacementAleatoire();
			}
		}

	}

	#region type de deplacement
	void deplacementVersEtoile(){
		transform.LookAt(monEtoile.transform);
		avancer();
		if(Vector3.Distance(transform.position, monEtoile.transform.position)<=1.5f){
			ControllerFourmis.instance.listeFourmis.Remove(gameObject);
			Destroy(gameObject);
		}
	}
	
	void deplacementAleatoire(){
		if(timer>=frequenceChangementDirection){
			timer = 0;
			frequenceChangementDirection = Random.Range(0,maxFrequenceChangementDirection);
			choixRotationDeplacement(Random.Range(minEcartAngle,maxEcartAngle));
		}
		avancer();
	}

	void deplacementExcite(){
		if(listePheromonesExcitesAutour.Count>0 && pheromoneLePlusExcite != null){
			if(Vector3.Distance(transform.position , pheromoneLePlusExcite.transform.position) <= 0.5f){
				pheromoneLePlusExcite = chercheMeilleurExcite(ref listePheromonesExcitesAutour);
			}
			else{
				transform.LookAt(pheromoneLePlusExcite.transform);
				avancer();
			}
		}

		else{
			followExcite = false;
			moveRandom = true;
		}
	}

	#endregion

	#region event 

	public void vientDeTrouverPheromone(GameObject _pheromoneTrouve){
		if(!traineePheromone.Contains(_pheromoneTrouve)){//si ce n'est pas un phéromone a moi
			pheromoneTrouve = _pheromoneTrouve;
			switch(pheromoneTrouve.GetComponent<pheromone>().myType){
			case (ControllerFourmis.Type.Neutre):
				break;

			case (ControllerFourmis.Type.Victoire):

				if(!listePheromonesExcitesAutour.Contains(pheromoneTrouve)){
					listePheromonesExcitesAutour.Add(pheromoneTrouve);
				}

				if(monEtat != ControllerFourmis.Type.Excitee && monEtat!=ControllerFourmis.Type.Victoire){ // si je suis pas déja excité  et que je ne suis pas déja en victoire
					transform.Find("detecteur").GetComponent<CapsuleCollider>().radius = 4;
					stop = true;
					moveRandom = false;
					rechercheMeilleurExcite = true;
					monEtat = ControllerFourmis.Type.Excitee;
				}
				break;

			case (ControllerFourmis.Type.Danger):
				break;
			case (ControllerFourmis.Type.Aide):
				break;
			case (ControllerFourmis.Type.Excitee):
				break;
			}
		}
	}
	
	public void vientDeTrouverEtoile(GameObject _etoile){
		moveRandom = false;
		followExcite = false;
		etoileTrouvee = true;
		monEtoile = _etoile;
		speed = 0.25f;
		freqPheromone = 0.5f;
		monEtat = ControllerFourmis.Type.Victoire;
		pheromoneLePlusExcite = null;
		listePheromonesExcitesAutour = new List<GameObject>();
		changePheromoneEnVictoire();
	}

	#endregion

	void poserPheromone(){
		if(timerPheromone>=freqPheromone){
			timerPheromone = 0 ;
			GameObject lePheromone = Instantiate(Resources.Load("pheromone") as GameObject, transform.Find("startPhero").gameObject.transform.position,transform.Find("startPhero").gameObject.transform.rotation ) as GameObject;
			lePheromone.name = "pheromone";
			traineePheromone.Add(lePheromone);
			lePheromone.GetComponent<pheromone>().myType = (ControllerFourmis.Type) monEtat;
			lePheromone.GetComponent<pheromone>().myFourmiScript = this;
			lePheromone.GetComponent<pheromone>().monRang = compteurPheromone;
			compteurPheromone++;
		}
	}

	GameObject chercheMeilleurExcite(ref List<GameObject> _listePheromonesExcitesAutour ){
		if(listePheromonesExcitesAutour.Count>0){
			GameObject meilleurPheromone = _listePheromonesExcitesAutour[0];
			float meilleurRang = meilleurPheromone.GetComponent<pheromone>().monRang;
			for(int i = 0 ; i < _listePheromonesExcitesAutour.Count; i++){
				if(meilleurRang < _listePheromonesExcitesAutour[i].GetComponent<pheromone>().monRang){
					meilleurRang = _listePheromonesExcitesAutour[i].GetComponent<pheromone>().monRang;
					meilleurPheromone = _listePheromonesExcitesAutour[i];
				}
			}
			return meilleurPheromone;
		}
		else{
			followExcite = false;
			moveRandom = true;
			return null;
		}

	}


	void avancer(){
		poserPheromone();
		gameObject.GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * speed);
	}
	
	void choixRotationDeplacement(float _angle){
		int i = Random.Range(0,2);
		if(i==0){
			transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y +_angle,transform.eulerAngles.z) ;
		}
		else{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y - _angle,transform.eulerAngles.z) ;
		}
	}
	
	void choixRotationEvitement(int _i){
		if(_i==0){
			transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y + 30 ,transform.eulerAngles.z) ;
		}
		else{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y - 30,transform.eulerAngles.z) ;
		}
	}
	
	public void demiTour(){
		transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y + 180 ,transform.eulerAngles.z) ;
		
	}

	void changePheromoneEnVictoire(){
		foreach(GameObject phero in traineePheromone){
			phero.gameObject.GetComponent<pheromone>().myType = (ControllerFourmis.Type) ControllerFourmis.Type.Victoire;
		}
	}  


	public void removePheromone(GameObject go){
		traineePheromone.Remove(go);
	}

	public void retirerPheromoneExcite(GameObject pheromoneToRemove){
		if(listePheromonesExcitesAutour.Contains(pheromoneToRemove)){
			listePheromonesExcitesAutour.Remove(pheromoneToRemove);
		}
	}
}
