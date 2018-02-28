using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class LightDetectorScript : MonoBehaviour {

	[Header ("Values")]
	public float angle;
	private bool useAngle = true;
	public float strength;
	public int numObjects;
	public enum Modo{Linear, Gaussiano};
	public Modo m;

	[Header ("Gaussian Values")]
	public float mean=0.5f;
	public float stddev=0.12f;

	[Header ("Limits")]
	public float StrenghtBottomLimit;
	public float StrenghtTopLimit;
	public float OutputBottomLimit;
	public float OutputTopLimit;







	void Start () {
		strength = 0;
		numObjects = 0;

		if (angle >= 360) {
			useAngle = false;
		}
	}

	void Update () {
		GameObject[] lights;

		if (useAngle) {
			lights = GetVisibleLights ();
		} else {
			lights = GetAllLights ();
		}

		strength = 0;
		numObjects = lights.Length;
	
		foreach (GameObject light in lights) {
			float r = light.GetComponent<Light> ().range;
			strength += 1.0f / ((transform.position - light.transform.position).sqrMagnitude / r + 1);
			Debug.DrawLine (transform.position, light.transform.position, Color.cyan);
		}

		if (numObjects > 0) {
			strength = strength / numObjects;
		}
	}

	// Get linear output value
	public float GetLinearOutput()
	{
		float s;
		if (strength > StrenghtBottomLimit && strength < StrenghtTopLimit) {
			s = strength;
		} 
		else {
			s = 0f;
		}

		if (s < OutputBottomLimit) {
			return OutputBottomLimit;

		} else if (s > OutputTopLimit) {
			return OutputTopLimit;

		} else {
			return s;
		}


	}

	// Get gaussian output value

	public float GetGaussianOutput()
	{

		float gauss;
		if (strength > StrenghtBottomLimit && strength < StrenghtTopLimit) {
			gauss = 1 / (stddev * (float)Math.Sqrt (2 * Math.PI)) * (float)Math.Exp (-(strength - mean) * (strength - mean) / (stddev * stddev));
		}
		else{
			gauss = 0;
		}
		if (gauss < OutputBottomLimit) {
			return OutputBottomLimit;

		} else if (gauss > OutputTopLimit) {
			return OutputTopLimit;
		
		} else {
			return gauss;
		}

	}

	public float GetOutput(){
		if (m == Modo.Linear) {
			return GetLinearOutput ();
		} else  {
			return GetGaussianOutput ();
		}
	}

		
	// Returns all "Light" tagged objects. The sensor angle is not taken into account.
	GameObject[] GetAllLights()
	{
		return GameObject.FindGameObjectsWithTag ("Light");
	}

	// Returns all "Light" tagged objects that are within the view angle of the Sensor. 
	// Only considers the angle over the y axis. Does not consider objects blocking the view.
	GameObject[] GetVisibleLights()
	{
		ArrayList visibleLights = new ArrayList();
		float halfAngle = angle / 2.0f;

		GameObject[] lights = GameObject.FindGameObjectsWithTag ("Light");

		foreach (GameObject light in lights) {
			Vector3 toVector = (light.transform.position - transform.position);
			Vector3 forward = transform.forward;
			toVector.y = 0;
			forward.y = 0;
			float angleToTarget = Vector3.Angle (forward, toVector);

			if (angleToTarget <= halfAngle) {
				visibleLights.Add (light);
			}
		}

		return (GameObject[])visibleLights.ToArray(typeof(GameObject));
	}


}
