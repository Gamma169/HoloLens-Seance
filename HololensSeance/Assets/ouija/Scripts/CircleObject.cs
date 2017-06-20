using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class CircleObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Circle());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator Circle() {
        float theta = 0;
        float radius = .5f;
        float frequency = .125f;
        Vector3 originalPos = transform.position;
        while (true) {

            Vector3 pos = new Vector3(radius * Mathf.Sin(theta) + originalPos.x, originalPos.y, radius * Mathf.Cos(theta) + originalPos.z);
            
            theta += frequency * 2 * Mathf.PI * Time.deltaTime;
            if (theta > 2 * Mathf.PI)
                theta -= 2 * Mathf.PI;

            transform.position = pos;

            yield return null;
        }
    }
}
