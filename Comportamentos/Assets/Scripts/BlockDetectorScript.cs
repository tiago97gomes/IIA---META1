using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class BlockDetectorScript : MonoBehaviour {

	public float angle;
	private bool useAngle = true;
	public float strength;
	public int numObjects;

	void Start () {
		strength = 0;
		numObjects = 0;

		if (angle >= 360) {
			useAngle = false;
		}
	}


	void Update () {
		GameObject[] blocks;
		if (useAngle) {
			blocks = GetVisibleBlocks ();
		} else {
			blocks = GetAllBlocks ();
		}
		strength = 0;
		numObjects = blocks.Length;

		/*foreach (GameObject b in blocks) {
			float r = b.GetComponent<BoxCollider> ().bounds.size.z*2;
			strength += 1.0f / ((transform.position - b.transform.position).sqrMagnitude / r + 1);
			Debug.DrawLine(transform.position, b.transform.position, Color.red);
		}*/
		GameObject tMin = null;
		float minDist = Mathf.Infinity;
		Vector3 currentPos = transform.position;
		foreach (GameObject t in blocks)
		{
			float dist = Vector3.Distance(t.transform.position, currentPos);
			if (dist < minDist)
			{
				tMin = t;
				minDist = dist;
			}
		}
		float r = tMin.GetComponent<BoxCollider> ().bounds.size.z*2;
		strength=1.0f / ((transform.position - tMin.transform.position).sqrMagnitude / r + 1);

	}

	// Get linear output value
	public float GetLinearOutput()
	{
		return strength;
	}

	// Get gaussian output value
	public virtual float GetGaussianOutput()
	{
		throw new NotImplementedException ();
	}

	// Returns all "Block" tagged objects. The sensor angle is not taken into account.
	GameObject[] GetAllBlocks()
	{
		return GameObject.FindGameObjectsWithTag ("Block");
	}

	// Returns all "Block" tagged objects that are within the view angle of the Sensor. 
	// Only considers the angle over the y axis. Does not consider objects blocking the view.
	GameObject[] GetVisibleBlocks()
	{
		ArrayList visibleBlocks = new ArrayList();
		float halfAngle = angle / 2.0f;
		//float halfAngle = angle ;
		GameObject[] blocks = GameObject.FindGameObjectsWithTag ("Block");
		foreach (GameObject block in blocks) {
			Vector3 toVector = (block.transform.position - transform.position);
			Vector3 forward = transform.forward;
			toVector.y = 0;
			forward.y = 0;
			float angleToTarget = Vector3.Angle (forward, toVector);
			if (angleToTarget <= halfAngle) {
				visibleBlocks.Add (block);
			}
		}
		return (GameObject[])visibleBlocks.ToArray(typeof(GameObject));
	}


}