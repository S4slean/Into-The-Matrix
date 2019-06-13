using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hookChain : MonoBehaviour
{
	LineRenderer lRend;
	Transform player;
	Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
		lRend = GetComponent<LineRenderer>();
		player = FindObjectOfType<CharaController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
		lRend.SetPosition(0, transform.position);
		lRend.SetPosition(1,player.position + Vector3.up); 
    }
}
