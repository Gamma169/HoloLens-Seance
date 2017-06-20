using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEnabler : MonoBehaviour {

	//goes from 0 to 1;
	public float intensity;


	// actual max intensity for light
	public float maxLightIntensity;

	private Light pointlight;


	void Awake() {
		pointlight = GetComponent<Light>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		pointlight.intensity = maxLightIntensity * intensity;


		pointlight.enabled = intensity > .1f;


	}
}
