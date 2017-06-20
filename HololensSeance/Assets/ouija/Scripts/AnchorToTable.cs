using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class AnchorToTable : MonoBehaviour {


    public float distanceAbove = 2;
    public float xMax = 5;
    public float zMax = 5;

        // Use this for initialization
        void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.localPosition = new Vector3(transform.localPosition.x, distanceAbove, transform.localPosition.z);

        if (transform.localPosition.x > xMax)
            transform.localPosition = new Vector3(xMax, transform.localPosition.y, transform.localPosition.z);
        if (transform.localPosition.x < -xMax)
            transform.localPosition = new Vector3(-xMax, transform.localPosition.y, transform.localPosition.z);

        
        if (transform.localPosition.z > zMax)
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, zMax);
        if (transform.localPosition.z < -zMax)
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -zMax);

    }
}
