using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControllerFourmis : MonoBehaviour {
	#region singleton
	static ControllerFourmis _instance;
	
	static public bool isActive { 
		get { 
			return _instance != null; 
		} 
	}
	
	static public ControllerFourmis instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(ControllerFourmis)) as ControllerFourmis;
				
				if (_instance == null)
				{
					GameObject go = new GameObject("ContrllerFourmis");
					DontDestroyOnLoad(go);
					_instance = go.AddComponent<ControllerFourmis>();
				}
			}
			return _instance;
		}
	}
	
	#endregion

	public enum Type{Neutre, Excitee, Victoire, Danger, Aide};
	public List<GameObject> listeFourmis;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}

	public void deletePheromonesExcitees(GameObject _pheromone){
		for(int i = 0; i < listeFourmis.Count ; i++){
			listeFourmis[i].GetComponent<deplacementFourmi>().retirerPheromoneExcite(_pheromone);
		}
	}
}
