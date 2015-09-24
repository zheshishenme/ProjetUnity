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
			return instance ?? (instance = new GameObject("Singleton Controleur Bonus").AddComponent<ControllerBonus>()); 
		}
	}
	#endregion

	public bool bonusSpeedUpHere = false;
	public bool bonusDarkHere = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void creerSpeedUpBonus(){
		GameObject speedup = Resources.Load("SpeedUp5")as GameObject;

		GameObject o = Instantiate(speedup,new Vector3(getRandom(),23,getRandom()), speedup.transform.rotation)as GameObject;
		o.name="SpeedUp5";
		bonusSpeedUpHere = true;
	}

	float getRandom(){
		return Random.Range(0,500);
	}

	public void creerDarkBonus(){
		GameObject dark = Resources.Load("Dark")as GameObject;	
		GameObject o = Instantiate(dark,new Vector3(getRandom(),23,getRandom()), dark.transform.rotation)as GameObject;
		o.name="Dark";
		bonusDarkHere = true;
	}
}
