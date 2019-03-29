using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInput : MonoBehaviour {
	
	public static UIInput instance;

	public GameObject codeInputText;
	private string codeString;

	public Vector2 touchPosition;
	public GameObject touchFeedback;

	private void Awake() {

		instance = this;
		
	}

	void Update () {

#if UNITY_EDITOR

		if (Input.GetMouseButton(0)){

			touchFeedback.gameObject.SetActive(true);
			touchFeedback.transform.position = Input.mousePosition;

		}
			
		if (Input.GetMouseButtonUp(0)){
			
			touchFeedback.transform.position = Input.mousePosition;
			touchFeedback.gameObject.SetActive(false);
						
		}

#else
		if (Input.touchCount == 1){ 

			if (Input.GetTouch(0).phase == TouchPhase.Began){
				touchFeedback.gameObject.SetActive(true);
			}

			touchFeedback.transform.position = Input.GetTouch(0).position;

			if (Input.GetTouch(0).phase == TouchPhase.Ended){
				touchFeedback.gameObject.SetActive(false);
			}
		}
#endif
		
	}
	

	public void ButtonDirection (string buttonName) {

		switch (buttonName)
		{
			case "Demo Button": 
				Screens.instance.ScreenChange(2);
				break;

			case "Customize Button":
				//Do nothing				
				break;

			case "Turn Off Button":
				//Do nothing				
				break;

			case "Turn Off Button 02":
				StartCoroutine (CameraImageToMatExample.instance.CaptureTime());
				break;

			case "Support Button":
				Screens.instance.ScreenChange(5);
				break;

			case "Send And Download Button":
				Screens.instance.ScreenChange(1);
				GetCodeText();
				DemoLoading.instance.LoadData(codeString);
				break;

			case "Return Button":
				Screens.instance.ScreenChange(0);
				break;
			
			case "Augment Button":
				VuforiaManagement.instance.EnableARContent();
				break;

			case "Pablo Yus Logo":
				Application.OpenURL("https://www.pabloyus.com/");
				break;
			
			case "Return From Error":
				Screens.instance.ScreenChange(0);
				break;
		}

	}

	private void GetCodeText () {
		codeString = codeInputText.gameObject.GetComponent<InputField>().text;
	}




}
