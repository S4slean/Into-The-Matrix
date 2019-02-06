using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantSpin : MonoBehaviour
{
	public int angularSpeed = 5;
    // Update is called once per frame
    void Update()
    {
		transform.Rotate(transform.forward, 5); 
    }
}
