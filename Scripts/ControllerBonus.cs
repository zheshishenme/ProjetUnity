using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ControllerBonus : MonoBehaviour {
	#region singleton
	static ControllerBonus _instance;
	
	static public bool isActive { 
		get { 
			return _instance != null; 
		} 
	}
	
	static public ControllerBonus instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(ControllerBonus)) as ControllerBonus;
				
				if (_instance == null)
				{
					GameObject go = new GameObject("ControllerBonus");
					DontDestroyOnLoad(go);
					_instance = go.AddComponent<ControllerBonus>();
				}
			}
			return _instance;
		}
	}

	#endregion

	float frequenceApparition = 2;
	float timer=0;

	float pourcentageBad = 50;
	float pourcentageGood = 50;



	public List<GameObject> listGoodBonus;
	public List<GameObject> listBadBonus;


	void Start(){
		/*listGoodBonus = new List<GameObject>();
		listBadBonus = new List<GameObject>();*/
	}
	
	// Update is called once per frame
	void Update () {
		if(ControlleurJeu.instance.gameStarted){

			timer += Time.deltaTime;

			if(timer>=frequenceApparition){
				creerBonus();
				timer = 0;
			}

		}
	
	}


	void creerBonus(){
		int nb = Random.Range(0,(int)(pourcentageBad+pourcentageGood));

		if(nb < pourcentageBad-1){
			//Creer bad bonus
			instancierBonus(listBadBonus[Random.Range(0,listBadBonus.Count)]);
		}

		else if(nb>=pourcentageBad)
			//creer good bonus
			instancierBonus(listGoodBonus[Random.Range(0,listGoodBonus.Count)]);
		}


	void instancierBonus(GameObject bonus){
		GameObject _bonus = Resources.Load(bonus.name)as GameObject;
		GameObject _leBonus = Instantiate(_bonus,new Vector3(getRandom(),23,getRandom()), _bonus.transform.rotation)as GameObject;
		_leBonus.name = bonus.name;
	}

	float getRandom(){
		return Random.Range(0,500);
	}
}
