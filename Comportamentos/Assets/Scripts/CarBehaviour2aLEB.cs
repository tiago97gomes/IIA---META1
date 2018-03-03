﻿using UnityEngine;
using System.Collections;

public class CarBehaviour2aLEB : CarBehaviourLEB {
	
	void Update()
	{
		//Read sensor values
		float leftSensor = LeftLD.GetLinearOutput ();
		float rightSensor = RightLD.GetLinearOutput ();

		//Calculate target motor values
		m_LeftWheelSpeed = leftSensor * MaxSpeed;
		m_RightWheelSpeed = rightSensor * MaxSpeed;
	}
}
