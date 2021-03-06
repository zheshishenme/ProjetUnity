﻿using UnityEngine;
using System.Collections;

public class cam : MonoBehaviour
{
	public Transform target;
	
	public float tailleCible = 15f; //taille de la cible à suivre
	public float distance = 15f; // distance entre la caméra et la cible
	public float distanceMur = 0.1f; //distance de sécurité pour les collisions
	
	public float maxDistance = 60; //distance maximale
	public float minDistance = .6f; // distance minimale
	
	public float xSpeed = 200.0f;
	public float ySpeed = 200.0f;
	public float targetSpeed = 50f; // vitesse de déplacement - avec déplacement souris
	
	
	public int yMinLimit = 15;
	public int yMaxLimit = 15;
	
	public int ratioZoom = 40;
	
	public float rotationDampening = 3.0f;
	public float zoomDampening = 5.0f;
	
	public LayerMask collisionLayers = -1;
	
	private float xDeg = 0.0f;
	private float yDeg = 15f;
	private float distanceActuelle; // va garder la distance actuelle 
	private float distanceDesiree; // va garder la distance désirée
	private float distanceCorrigee; // va garder la distance corrigée
	
	void Start ()
	{
		/*Vector3 angles = transform.eulerAngles;
		xDeg = angles.x;

		
		distanceActuelle = distance;
		distanceDesiree = distance;
		distanceCorrigee = distance;

		//transform.localEulerAngles = new Vector3(30,350,0);
		*/
	}


	/**
* La caméra se met a jour après la gestion de tous les mouvements du personnage
*/
	void LateUpdate ()
	{
		Vector3 vTargetOffset;
		
		// Si on a pas de target on ne fait rien
		if (!target)
			return;

		// sinon la caméra va se placer derrière la cible
		if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
		{
			float angleRotationCible = target.eulerAngles.y;
			float angleActuelleRotation = transform.eulerAngles.y;
			xDeg = Mathf.LerpAngle (angleActuelleRotation, angleRotationCible, rotationDampening * Time.deltaTime);
		}
		
		yDeg = 15;
		
		
		// instancie la rotation de la caméra
		Quaternion rotation = Quaternion.Euler (yDeg, xDeg, 0);
		

		// calcul de la position de la caméra désirée
		vTargetOffset = new Vector3 (0, -tailleCible, 0);

		Vector3 position = target.position - (rotation * Vector3.forward * distanceDesiree + vTargetOffset);
		

		// va vérifier qu'il n'y a pas de collisions
		RaycastHit collisionHit;
		Vector3 positionReelleCible = new Vector3 (target.position.x, target.position.y + tailleCible, target.position.z);
		
		// S'il y a une collision, il faut corriger la position de la caméra et recalculé la distance correcte
		bool esCorrigee = false;
		if (Physics.Linecast (positionReelleCible, position, out collisionHit, collisionLayers.value))
		{
			// Calcul de la distance avec la position de collision estimée 
			// on enlève la distance de sécurité de collision "offset" 
			// On va garder la caméra en haut de la surface où il y colision 
			distanceCorrigee = Vector3.Distance (positionReelleCible, collisionHit.point) - distanceMur;
			esCorrigee = true;
		}

		// On va smoother la distance actuelle si la distance n'est pas corrigée ou si la distanceCorigée es plus grande que la distanceActuelle
		distanceActuelle = !esCorrigee || distanceCorrigee > distanceActuelle ? Mathf.Lerp (distanceActuelle, distanceCorrigee, Time.deltaTime * zoomDampening) : distanceCorrigee;

		// Il faut obliger la distance actuelle à rester entre le min et le max prédéfini
		distanceActuelle = Mathf.Clamp (distanceActuelle, minDistance, maxDistance);
		
		// on recalcul la position et on se base sur la distance actuelle
		position = target.position - (rotation * Vector3.forward * distanceActuelle + vTargetOffset);
		
		transform.rotation = rotation;
		position = new Vector3(target.localPosition.x,target.localPosition.y+40f, target.localPosition.z-50f);
		transform.localPosition = position;
	}
	
	private static float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}
}