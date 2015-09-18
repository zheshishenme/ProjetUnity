using UnityEngine;
using System.Collections;

public class ControllerBonus : MonoBehaviour {
	#region singleton
	private static ControllerBonus instance;
	
	// Static singleton property
	public static ControllerBonus Instance
	{
		// Here we use the ?? operator, to return 'instance' if 'instance' does not equal null
		// otherwise we assign instance to a new component and return that
		get { 
			return instance ?? (instance = new GameObject("Singleton Controleur joueur").AddComponent<ControllerBonus>()); 
		}
	}
	#endregion

	public bool bonusSpeedUp = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void creerSpeedUpBonnus(){
		GameObject speedup = Resources.Load("SpeedUp5")as GameObject;
		GameObject o = Instantiate(speedup,new Vector3(getRandom(),23,getRandom()), speedup.transform.rotation)as GameObject;
		o.name="SpeedUp5";
		bonusSpeedUp = true;
	}

	float getRandom(){
		return Random.Range(0,500);
	}
}
