using UnityEngine;
using System.Collections;

public class CarBehaviour2a : CarBehaviour {
	
	void Update()
	{
		//Read sensor values
		//float leftSensor = LeftLD.GetLinearOutput ();
		//float rightSensor = RightLD.GetLinearOutput ();

		//Proximity sensor values
		float leftSensorB = LeftBD.GetLinearOutput ();
		float rightSensorB = RightBD.GetLinearOutput ();

		//Calculate target motor values
		//m_LeftWheelSpeed = leftSensor * MaxSpeed;
		//m_RightWheelSpeed = rightSensor * MaxSpeed;
		if (leftSensorB == rightSensorB) {
			m_LeftWheelSpeed = leftSensorB * MaxSpeed ;
			m_RightWheelSpeed = rightSensorB * MaxSpeed;
		} else {
		
			m_LeftWheelSpeed = leftSensorB * MaxSpeed *1.4f;
			m_RightWheelSpeed = rightSensorB * MaxSpeed;
		}
	}
}
