using UnityEngine;
using System.Collections;

public class CarBehaviour2a : CarBehaviour {
	
	void Update()
	{
		//Read sensor values
		float leftSensor = LeftLD.GetOutput();
		float rightSensor = RightLD.GetOutput ();

		//Calculate target motor values
		m_LeftWheelSpeed = leftSensor * MaxSpeed;
		m_RightWheelSpeed = rightSensor * MaxSpeed;
	}
}
