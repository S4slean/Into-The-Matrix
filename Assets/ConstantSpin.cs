using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantSpin : MonoBehaviour
{
	public int angularSpeed = 5;
	private Vector3 localForward;
	// Update is called once per frame
	void Update()
    {
		localForward = transform.worldToLocalMatrix.MultiplyVector(transform.forward);

		transform.Rotate(localForward, 5); 
    }
}
