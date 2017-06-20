using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterController : MonoBehaviour {

	public LightEnabler lightE;

	public GameObject letterLighting;

	//should also go from 0 to 1;
	public float intensity;

	private Material letterLightingMat;

	private Color alphaColor;
	private Color regColor;

	// Use this for initialization
	void Start () {
		letterLightingMat = letterLighting.GetComponent<MeshRenderer>().material;
		regColor = letterLightingMat.color;
		alphaColor = new Color(regColor.r, regColor.g, regColor.b, 0);
	}
	
	// Update is called once per frame
	void Update () {

		if (intensity < 0)
			intensity = 0;
		if (intensity > 1)
			intensity = 1;

		lightE.intensity = this.intensity;
		letterLightingMat.color = Color.Lerp(alphaColor, regColor, intensity);

	}
}
