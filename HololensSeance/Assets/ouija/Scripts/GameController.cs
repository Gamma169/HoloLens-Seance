using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using HoloToolkit.Unity.InputModule;


public class GameController : MonoBehaviour {

	public static int section;

	public MeshRenderer holoCursor;
	public HandDraggableCustom markerDragger;

	public TextMesh instructionText;

    public GameObject picture;

	public Light leftLight;
	public Light rightLight;

	public AudioSource theme;
	public AudioSource firstAppear;
	public AudioSource successSound;
	public AudioSource indicationRumble;
	public AudioSource darkSound;

	public AudioSource secret1;
	public AudioSource secret2;

	public Transform[] indicatorLocations;
	public LetterController[] letterControllers;

	private int proximitySpot;
	private int letterControllerActive;
	private float proximity;
	private float energy;



	public MeshRenderer[] imageRends;
	private Color[] endColors;


	// Use this for initialization
	void Start () {
		section = 0;
		proximitySpot = 0;
		instructionText.text = "";

		endColors = new Color[imageRends.Length];
		for (int i = 0; i < imageRends.Length; i++) {
			endColors[i] = imageRends[i].material.color;
			imageRends[i].material.color = Color.clear;
		}

		StartCoroutine(PlayGame());
	}
	
	// Update is called once per frame
	void Update () {

		holoCursor.enabled = !markerDragger.BeingDragged;


		CheckProximity();
		CheckEnergy();
		indicationRumble.volume = Mathf.Sin(Mathf.PI / 2 * proximity);



	}

	private IEnumerator PlayGame() {
		//====================================    SECTION 0    ====================================\\
		leftLight.intensity = 0;
		rightLight.intensity = 0;

		markerDragger.IsDraggingEnabled = false;

		yield return new WaitForSeconds(1.25f);
		if (section == 0)
			StartCoroutine(FadeInInstructions("Say: 'Welcome'"));

		yield return new WaitUntil(() => section == 1);
		//====================================    SECTION 1    ====================================\\

		if (instructionText.color.a > .1f)
			StartCoroutine(FadeOutInstructions());

		firstAppear.Play();
		StartCoroutine(FadeLightsIn());

		markerDragger.IsDraggingEnabled = true;

		yield return new WaitForSeconds(1.25f);
		if (section == 1)
			StartCoroutine(FadeInInstructions("Ask: 'Are you here?'"));

		yield return new WaitUntil(() => section == 2);
		//====================================    SECTION 2    ====================================\\
		StartCoroutine(FadeInTheme());
		theme.Play();

		if (instructionText.color.a > .1f)
			StartCoroutine(FadeOutInstructions());

		yield return new WaitForSeconds(1);
		StartCoroutine(FadeInInstructions("Move the marker"));


		proximitySpot = 1;
		letterControllerActive = 1;
		yield return new WaitUntil(() => energy > .925f);
		section++;
		//====================================    SECTION 3    ====================================\\

		if (instructionText.color.a > .1f)
			StartCoroutine(FadeOutInstructions());

		proximitySpot = 0;
		letterControllerActive = 0;
		StartCoroutine(FadeOutEnergy(1));
		successSound.Play();
		yield return new WaitForSeconds(1f);

		if (section == 3)
			StartCoroutine(FadeInInstructions("Ask: 'Who are you?'"));

		yield return new WaitUntil(() => section == 4);
		//====================================    SECTION 4    ====================================\\

		//successSound.Play();
		darkSound.Play();
		yield return new WaitForSeconds(.75f);
		if (instructionText.color.a > .1f)
			StartCoroutine(FadeOutInstructions());

		StartCoroutine(FadeInSpatials());

		yield return new WaitForSeconds(1);

		StartCoroutine(FadeInInstructions("Move The Marker"));

		proximitySpot = 2;
		letterControllerActive = 2;
		yield return new WaitUntil(() => energy > .925f);
		successSound.Play();
		StartCoroutine(FadeInInstructions("L"));
		StartCoroutine(FadeOutEnergy(2));
		yield return null;
		energy = 0;
		proximitySpot = 3;
		letterControllerActive = 3;
		yield return new WaitUntil(() => energy > .925f);
		successSound.Play();
		StartCoroutine(FadeInInstructions("LA"));
		StartCoroutine(FadeOutEnergy(3));
		yield return null;
		energy = 0;
		proximitySpot = 4;
		letterControllerActive = 4;
		yield return new WaitUntil(() => energy > .925f);
		successSound.Play();
		StartCoroutine(FadeInInstructions("LAU"));
		StartCoroutine(FadeOutEnergy(4));
		yield return null;
		energy = 0;
		proximitySpot = 5;
		letterControllerActive = 5;
		yield return new WaitUntil(() => energy > .925f);
		successSound.Play();
		StartCoroutine(FadeInInstructions("LAUR"));
		StartCoroutine(FadeOutEnergy(5));
		yield return null;
		energy = 0;
		proximitySpot = 3;
		letterControllerActive = 3;
		yield return new WaitUntil(() => energy > .925f);
		successSound.Play();
		StartCoroutine(FadeInInstructions("LAURA"));
		StartCoroutine(FadeOutEnergy(3));
		yield return null;

		proximitySpot = 0;
		letterControllerActive = 0;

		yield return new WaitForSeconds(1.5f);
		StartCoroutine(FadeOutInstructions());
		yield return new WaitForSeconds(1.5f);

		section++;
		//====================================    SECTION 5    ====================================\\
		StartCoroutine(FadeInInstructions("Say: 'Show yourself'"));

		yield return new WaitUntil(() => section == 6);
		//====================================    SECTION 6    ====================================\\

		yield return new WaitForSeconds(.15f);
		if (instructionText.color.a > .1f)
			StartCoroutine(FadeOutInstructions());

		successSound.Play();

		StartCoroutine(FadeOutSpatials());
		StartCoroutine(FadeInPicuture());

		yield return new WaitForSeconds(5f);
		StartCoroutine(FadeInInstructions("Say: 'Goodbye'"));
		yield return new WaitUntil(() => section == 7);
		//====================================    SECTION 7    ====================================\\

		firstAppear.Play();
		StartCoroutine(FadeOutPicuture());
		StartCoroutine(FadeOutTheme());
		StartCoroutine(FadeOutInstructions());

		yield return null;
	}

	public void CheckProximity() {
		float maxDistance = .06f;

		float dist = Vector3.Distance(markerDragger.transform.position, indicatorLocations[proximitySpot].position);

		if (dist > maxDistance)
			proximity = 0;
		else {
			proximity = (maxDistance - (dist - .014f)) / maxDistance;
		}


		//print(dist);
		//print(proximity);
	
	}

	public void CheckEnergy() {
		if (energy > 0)
			energy -= .3f * Time.deltaTime;
		else
			energy = 0;
		if (letterControllerActive != 0) {
			if (proximity > .9f) {
				energy += 1f * Time.deltaTime;
			}
			else if (proximity > .8f)
				energy += .8f * Time.deltaTime;
			else if (proximity > .7f)
				energy += .6f * Time.deltaTime;
			else if (proximity > .6f)
				energy += .5f * Time.deltaTime;
		
			if (energy > 1)
				energy = 1;

			letterControllers[letterControllerActive].intensity = energy;
		}
		else
			energy = 0;
	}

	public void Welcome() {
		if (section == 0)
			section = 1;
	}

	public void AreYouHere() {
		if (section == 1)
			section = 2;
	}

	public void WhoAreYou() {
		if (section == 3)
			section = 4;
	}

	public void ShowYouself() {
		if (section == 5)
			section = 6;
	}

	public void Goodbye() {
		if (section == 6)
			section = 7;
	}


    public void EnablePicture() {
        picture.SetActive(true);
    }

    public void DisablePicture() {
        picture.SetActive(false);
    }

	public void ResetScene() {
		SceneManager.LoadScene("TestScene2");
	}

	private IEnumerator FadeLightsIn() {
		for (float i = 0; i < 2; i += Time.deltaTime) {
		
			leftLight.intensity = .35f * i / 2;
			rightLight.intensity = .35f * i / 2;
				

			yield return null;
		}
	}

	private IEnumerator FadeInInstructions(string s) {
		Color col = new Color(.66f, .55f, .2f, 1);
		instructionText.text = s;
		for (float i = 0; i < 2; i += Time.deltaTime) {
			instructionText.color = Color.Lerp(Color.clear, col, i / 2f);
			yield return null;
		}
	}
	private IEnumerator FadeOutInstructions() {
		Color col = new Color(.66f, .55f, .2f, 1);
		for (float i = 0; i < .75f; i += Time.deltaTime) {
			instructionText.color = Color.Lerp(col, Color.clear, i / .75f);
			yield return null;
		}
		instructionText.color = Color.clear;
	}
		
	private IEnumerator FadeOutEnergy(int i) {
		while (letterControllers[i].intensity > 0) {
			letterControllers[i].intensity -= .5f * Time.deltaTime;
			yield return null;
		}
		letterControllers[i].intensity = 0;
	}

	private IEnumerator FadeInTheme() {
		for (float i = 0; i < 1; i += Time.deltaTime) {
			theme.volume = i / 1.4f;
			yield return null;
		}
	}

	private IEnumerator FadeOutTheme() {
		for (float i = 2; i > 0; i -= Time.deltaTime) {
			theme.volume = i / 2f;
			yield return null;
		}
	}

	private IEnumerator FadeInSpatials() {
		secret1.volume = 0;
		secret2.volume = 0;
		secret1.Play();
		secret2.Play();
		for (float i = 0; i < 1.5f; i += Time.deltaTime) {
			secret1.volume = i / 1.5f;
			secret2.volume = i / 1.5f;
			yield return null;
		}

	}


	private IEnumerator FadeOutSpatials() {
		for (float i = 1; i > 0; i -= Time.deltaTime) {
			secret1.volume = i;
			secret2.volume = i;
			yield return null;
		}


		secret1.volume = 0;
		secret2.volume = 0;
	}

	private IEnumerator FadeInPicuture() {
		for (float i = 0; i < 1.5f; i += Time.deltaTime) {
			for (int j = 0; j < imageRends.Length; j++) {
				imageRends[j].material.color = Color.Lerp(Color.clear, endColors[j], i / 1.5f);
			}
			yield return null;
		}
		yield return null;
	}

	private IEnumerator FadeOutPicuture() {
		for (float i = 1.5f; i >0; i -= Time.deltaTime) {
			for (int j = 0; j < imageRends.Length; j++) {
				imageRends[j].material.color = Color.Lerp(Color.clear, endColors[j], i / 1.5f);
			}
			yield return null;
		}
		yield return null;
	}
}
