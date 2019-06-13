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
		startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 Pos = new Vector3(player.position.x - transform.position.x, 1, player.position.z - transform.position.z);
		lRend.SetPosition(1,-startPos); 
    }
}
