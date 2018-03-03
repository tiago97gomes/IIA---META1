using UnityEngine;
using System.Collections;

public class CarBehaviour2aBlocks : CarBehaviourBlocks {
	
	void Update()
	{
		//Read sensor values
		float leftSensorBlocks = LeftSensorBlocks.GetLinearOutput();
		float rightSensorBlocks = RightSensorBlocks.GetLinearOutput ();

		//Calculate target motor values
		m_LeftWheelSpeed = leftSensorBlocks * MaxSpeed;
		m_RightWheelSpeed = rightSensorBlocks * MaxSpeed;
	}
}
