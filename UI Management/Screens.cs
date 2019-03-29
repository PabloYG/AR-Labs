using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screens : MonoBehaviour
{

	public static Screens instance;

    public GameObject[] screenList;

	public int activeScreen = 0;
	public int lastScreen;


	private void Awake() {

		instance = this;

	}

	public void ScreenChange (int screenNumber) {

		Animations.instance.DemoSpecificUI(DemoLoading.instance.activeDemo);
		
		for (int i = 0; i < screenList.Length; i++){
			if (i == 0) {
				screenList[i].SetActive (true);
			} else {
				if (screenList[i].activeSelf == true){
					lastScreen = i;
					screenList [i].SetActive (false);
				} else {
					screenList [i].SetActive (false);
				}
			}

		}

		screenList[screenNumber].SetActive (true);
		activeScreen = screenNumber;

		switch (screenNumber)
		{
			case 0: 
				//Do nothing
				break;
			case 1:
				//Do nothing				
				break;
			case 2:
				screenList[0].SetActive (true);
				screenList[2].transform.Find("Default").gameObject.SetActive (true);
				activeScreen = 2;
				break;
			case 3:
				screenList[2].SetActive (true);
				screenList[2].transform.Find("Default").gameObject.SetActive (false);
				screenList[1].SetActive (false);
				lastScreen = 0;
				break;
			
			case 6:
				screenList[2].SetActive (false);
				screenList[3].SetActive (false);
				screenList[4].SetActive (false);			
				break;
		}

	}


}
