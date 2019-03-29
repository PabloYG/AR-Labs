using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoLoading : MonoBehaviour {

	public static DemoLoading instance;

	public GameObject[] demos;
	public int activeDemo;

	private void Awake() {

		instance = this;
		
	}

	void Start () {

		/* 
		for (int i = 0; i < demos.Length; i++) {
			demos[i].gameObject.SetActive (false);
		}
		*/

		if (demos[0].gameObject.activeSelf == true) {
			activeDemo = 0;
		} else if (demos[1].gameObject.activeSelf == true) {
			activeDemo = 1;
		} else if (demos[2].gameObject.activeSelf == true) {
			activeDemo = 2;
		} else if (demos[3].gameObject.activeSelf == true) {
			activeDemo = 3;
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadData (string codeString) {

		print ("Yep");
		CodeChecking(codeString);

	}

	IEnumerator LoadDemoFromCode(int demoIndex)
    {

		Screens.instance.ScreenChange(1);

		yield return new WaitForSeconds(2);

		for (int i = 0; i < demos.Length; i++) {
			demos[i].gameObject.SetActive (false);
		}
		demos[demoIndex].gameObject.SetActive (true);

		activeDemo = demoIndex;
		Animations.instance.DemoSpecificUI(activeDemo);

		Screens.instance.ScreenChange(0);

    }

	IEnumerator WrongCode()
    {

		Screens.instance.ScreenChange(1);

		yield return new WaitForSeconds(2);

		Screens.instance.ScreenChange(3);

    }

	private void CodeChecking (string codeString) {

		// Test Code ---> 05038168

		if (codeString == ""){
			StartCoroutine(WrongCode());
		} else {

			if (codeString.Length >= 6){

				string signalOne = codeString[2].ToString();
				string signalTwo = codeString[5].ToString();

				if (signalOne == "0" && signalTwo == "0") {
					StartCoroutine(LoadDemoFromCode(0));
				} else if (signalOne == "0" && signalTwo == "1") {
					StartCoroutine(LoadDemoFromCode(1));
				} else if (signalOne == "0" && signalTwo == "2"){
					StartCoroutine(LoadDemoFromCode(2));
				} else if (signalOne == "0" && signalTwo == "4") {
					StartCoroutine(LoadDemoFromCode(3));
				} else {
					StartCoroutine(WrongCode());
				}
				
			} else {
				StartCoroutine(WrongCode());
			}
			
		}


		
	}
}
