using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour {

	public static int section;

	GameObject holoCursor;

    public GameObject picture;

	public Light leftLight;
	public Light rightLight;

	public AudioSource rumble;




	private float energy;

	// Use this for initialization
	void Start () {
		section = 0;
		StartCoroutine(PlayGame());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator PlayGame() {

		leftLight.intensity = 0;
		rightLight.intensity = 0;

		yield return new WaitUntil(() => section == 1);
		rumble.Play();
		StartCoroutine(FadeLightsIn());

	
	
	
	
	
	
		yield return null;
	}


	public void AreYouHere() {
		if (section == 0)
			section = 1;
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
	
	}
		
}
