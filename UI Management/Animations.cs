using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Animations : MonoBehaviour {

	public static Animations instance;

	public GameObject[] trackableMessages;
	public GameObject distanceBar1;
	public GameObject distanceBar2;
	public GameObject augmentButton;
	public GameObject augmentColor;
	public Animator augmentButtonAnimator;
	public Animator markerMessagesAnimator;

	public GameObject[] demo00SpecificUI;
	public GameObject[] demo01SpecificUI;
	public GameObject[] demo02SpecificUI;

// -------- UI Elements ---------- //
	public Image augmentButtonImage;
    public GameObject augmentText;
    public GameObject scanText;
    public Image barImage;
    public Image barFlippedImage;

	public Image loadingBackground;
	public Image codeBackground;
	public Image errorBackground;

	public GameObject defaultSendText;
	public GameObject elcarSendText;

// -------- SPRITES ---------- //
    public Sprite elcarAugment;
    public Sprite defaultAugment;
    public Sprite elcarBar;
    public Sprite defaultBar;
    public Sprite elcarBarFlipped;
    public Sprite defaultBarFlipped;
	public Sprite elcarBackground;
	public Sprite defaultBackground;
	

	private void Awake() {

		instance = this;
		
	}

	// Use this for initialization
	void Start () {

		DemoSpecificUI(DemoLoading.instance.activeDemo);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DemoSpecificUI (int activeDemo) {

		//print (activeDemo);

		switch (activeDemo) {

			case 0:

				augmentButtonImage.sprite = defaultAugment;
				barFlippedImage.sprite = defaultBarFlipped;
				barImage.sprite = defaultBar;

				loadingBackground.sprite = defaultBackground;
				codeBackground.sprite = defaultBackground;
				errorBackground.sprite = defaultBackground;

				defaultSendText.SetActive(true);
				elcarSendText.SetActive(false);

				for(int i = 0; i < demo00SpecificUI.Length; i++) {
					demo00SpecificUI[i].gameObject.SetActive (true);
				}

				for(int i = 0; i < demo01SpecificUI.Length; i++) {
					demo01SpecificUI[i].gameObject.SetActive (false);
				}

				for(int i = 0; i < demo02SpecificUI.Length; i++) {
					demo02SpecificUI[i].gameObject.SetActive (false);
				}

			break;

			case 1:

				augmentButtonImage.sprite = defaultAugment;
				barFlippedImage.sprite = defaultBarFlipped;
				barImage.sprite = defaultBar;

				loadingBackground.sprite = defaultBackground;
				codeBackground.sprite = defaultBackground;
				errorBackground.sprite = defaultBackground;

				defaultSendText.SetActive(true);
				elcarSendText.SetActive(false);

				for(int i = 0; i < demo00SpecificUI.Length; i++) {
					demo00SpecificUI[i].gameObject.SetActive (false);
				}

				for(int i = 0; i < demo01SpecificUI.Length; i++) {
					demo01SpecificUI[i].gameObject.SetActive (true);
				}

				for(int i = 0; i < demo02SpecificUI.Length; i++) {
					demo02SpecificUI[i].gameObject.SetActive (false);
				}

			break;

			case 2:

				augmentButtonImage.sprite = defaultAugment;
				barFlippedImage.sprite = defaultBarFlipped;
				barImage.sprite = defaultBar;

				loadingBackground.sprite = defaultBackground;
				codeBackground.sprite = defaultBackground;
				errorBackground.sprite = defaultBackground;

				defaultSendText.SetActive(true);
				elcarSendText.SetActive(false);

				for(int i = 0; i < demo00SpecificUI.Length; i++) {
					demo00SpecificUI[i].gameObject.SetActive (false);
				}

				for(int i = 0; i < demo01SpecificUI.Length; i++) {
					demo01SpecificUI[i].gameObject.SetActive (false);
				}

				for(int i = 0; i < demo02SpecificUI.Length; i++) {
					demo02SpecificUI[i].gameObject.SetActive (true);
				}
				
			break;

			case 3:

				augmentButtonImage.sprite = elcarAugment;
				barFlippedImage.sprite = elcarBarFlipped;
				barImage.sprite = elcarBar;

				loadingBackground.sprite = elcarBackground;
				codeBackground.sprite = elcarBackground;
				errorBackground.sprite = elcarBackground;

				defaultSendText.SetActive(false);
				elcarSendText.SetActive(true);

			break;

		}

	}

	public void TrackableDetection (bool detection) {

		if (detection) {

			//markerMessagesAnimator.SetBool ("ActiveAR", true);
			markerMessagesAnimator.SetTrigger("Found");

		}else{

			//markerMessagesAnimator.SetBool ("ActiveAR", false);
			markerMessagesAnimator.SetTrigger("Lost");

		}

	}

	public void DistanceBars(float distanceToTarget) {

		//distanceBar1.gameObject.GetComponent<RectTransform>().offsetMax.x;
		float newX = Mathf.Lerp(2f, 0f, distanceToTarget / 6);

		//print(newX);

		if(newX >= 2)
        {
            distanceBar1.gameObject.GetComponent<RectTransform>().localScale = new Vector3 (2, 1, 1);
			distanceBar2.gameObject.GetComponent<RectTransform>().localScale = new Vector3 (2, 1, 1);
			VuforiaManagement.instance.closeEnough = true;
			EnableAugment(true);
            return;
        } else {
			VuforiaManagement.instance.closeEnough = false;
			EnableAugment(false);
		}



		print (newX);
		distanceBar1.gameObject.GetComponent<RectTransform>().localScale = new Vector3 (newX, 1, 1);
		distanceBar2.gameObject.GetComponent<RectTransform>().localScale = new Vector3 (newX, 1, 1);
	}

	public void EnableAugment(bool active) {
		
		if (active) {

			augmentButtonAnimator.SetBool ("ActiveAR", true);

			//augmentButton.gameObject.GetComponent<Button>().enabled = true;
			//augmentColor.gameObject.SetActive(true);
		} else {

			augmentButtonAnimator.SetBool ("ActiveAR", false);

			//augmentButton.gameObject.GetComponent<Button>().enabled = false;
			//augmentColor.gameObject.SetActive(false);
		}




	}
}
