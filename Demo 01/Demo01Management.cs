using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Demo01Management : MonoBehaviour {

	public static Demo01Management instance;
	public GameObject[] stains;
	public GameObject[] highlights;
	public GameObject textHighlights;
	public string activeStain;
	public GameObject Content3D;
	public GameObject[] parts3Dgray;
	public GameObject[] parts3Dhighlight;
	GameObject last3DGrey;
	GameObject last3DHighlight;
	public bool allStainsInactive;
	public bool active3D;
	public GameObject toggle3D;
	public GameObject neuron3D;

	public bool leanTouchActive = false;

	 private bool IsPointerOverUIObject() {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
 	}

	private void Awake() {

		instance = this;
		
	}

	void OnEnable () {
		VuforiaManagement.activationEvent += Activation;
	}

	void Activation (bool activation) {
		ChangeStain("Cajal Stain Collider");
	}


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

#if UNITY_EDITOR

		Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(raycast, out hit)){
			//print(hit.transform.name);
			if (!leanTouchActive && Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftControl)){
				RaycastCollision(hit.transform.name.ToString());
			}
		} else {
			if (!leanTouchActive && !IsPointerOverUIObject() && Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftControl)){
				CloseEverything ();
			}
		}

#else

		Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(raycast, out hit)){
			//print(hit.transform.name);

			if (!IsPointerOverUIObject() && !leanTouchActive && Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) {
				RaycastCollision(hit.transform.name.ToString());
        	}
		} else {
			try {
				if (!leanTouchActive && !IsPointerOverUIObject() && Input.touchCount <= 1 && Input.GetTouch(0).phase == TouchPhase.Ended){
				CloseEverything ();
				}	
			}
			catch (Exception i){
				print ("Index out of bounds");
			}
			
		}


#endif
		
		if (Content3D.gameObject.activeSelf){
			active3D = true;
		} else {
			active3D = false;
		}

	}

	public void CloseEverything () {

		Content3D.gameObject.SetActive(false);

		for (int j = 0; j < stains.Length; j++) {	
			for (int i = 0; i < highlights.Length; i++) {
				foreach (Transform child in highlights[i].gameObject.transform) {
					child.gameObject.SetActive(false);				
				}
				highlights[i].gameObject.SetActive(false);
			}
			stains[j].gameObject.SetActive(false);
		}

		textHighlights.gameObject.SetActive(false);

	}

	public void RaycastCollision (string hitName) {

		if (hitName.Contains("Stain") & !hitName.Contains("Image")) {

			ChangeStain (hitName);

		} else {
			
			if (!stains[0].gameObject.activeSelf && !stains[1].gameObject.activeSelf && !stains[2].gameObject.activeSelf) {
				ChangeStain("Cajal Stain Collider");
			}

			switch (hitName){

				case "Neuron Text Collider":
					ActivateHighlights ("Neuron");
					Activate3D ("Neuron");
				break;

				case "Astrocyte Text Collider":
					ActivateHighlights ("Astrocyte");
					Activate3D ("Astrocyte");
				break;

				case "Dendrite Text Collider":
					ChangeStain ("Cajal Stain Collider");
					ActivateHighlights ("Dendrite");
					Activate3D ("Dendrite");
				break;

				case "Axon Text Collider":
					ChangeStain ("Cajal Stain Collider");
					ActivateHighlights ("Axon");
					Activate3D ("Axon");
				break;

				case "Myelin Text Collider":
					ChangeStain ("Kluver-Barrera Stain Collider");
					ActivateHighlights ("Myelin");
					Activate3D ("Myelin");
				break;

				case "Neuron Image Collider":
					ActivateHighlights ("Neuron");
					Activate3D ("Neuron");
				break;

				case "Astrocyte Image Collider":
					ActivateHighlights ("Astrocyte");
					Activate3D ("Astrocyte");
				break;

				case "Dendrite Image Collider":
					ActivateHighlights ("Dendrite");
					Activate3D ("Dendrite");
				break;

				case "Axon Image Collider":
					ActivateHighlights ("Axon");
					Activate3D ("Axon");
				break;

				case "Myelin Image Collider":
					ActivateHighlights ("Myelin");
					Activate3D ("Myelin");
				break;

			}

		}

	}

	public void ChangeStain (string stainName) {

		CloseEverything();

		for (int i = 0; i < highlights.Length; i++) {
			foreach (Transform child in highlights[i].gameObject.transform) {
				child.gameObject.SetActive(false);				
			}
			highlights[i].gameObject.SetActive(false);
		}

		switch (stainName){

				case "Cajal Stain Collider":
				
					stains[0].gameObject.SetActive(true);
					stains[1].gameObject.SetActive(false);
					stains[2].gameObject.SetActive(false);

					activeStain = "Cajal Stain";

				break;

				case "Nissl Stain Collider":
				
					stains[0].gameObject.SetActive(false);
					stains[1].gameObject.SetActive(true);
					stains[2].gameObject.SetActive(false);

					activeStain = "Nissl Stain";

				break;

				case "Kluver-Barrera Stain Collider":
				
					stains[0].gameObject.SetActive(false);
					stains[1].gameObject.SetActive(false);
					stains[2].gameObject.SetActive(true);

					activeStain = "Kluver-Barrera Stain";

				break;

			}

	}

	public void ActivateHighlights (string target) {

			switch (activeStain){

				case "Cajal Stain":
					
					switch (target){

						case "Neuron":
						
							highlights[0].gameObject.SetActive(true);
							foreach (Transform child in highlights[0].gameObject.transform)
							{
								child.gameObject.SetActive(false);
								if(child.name == target) {
									child.gameObject.SetActive(true);
								}
									
							}

							textHighlights.gameObject.SetActive(true);
							foreach (Transform child in textHighlights.gameObject.transform)
							{
								child.gameObject.SetActive(false);
								if(child.name == target) {
									child.gameObject.SetActive(true);
								}
									
							}

						break;

						case "Astrocyte":
						
							highlights[0].gameObject.SetActive(true);
							foreach (Transform child in highlights[0].gameObject.transform)
							{
								child.gameObject.SetActive(false);
								if(child.name == target) {
									child.gameObject.SetActive(true);
								}
									
							}

							textHighlights.gameObject.SetActive(true);
							foreach (Transform child in textHighlights.gameObject.transform)
							{
								child.gameObject.SetActive(false);
								if(child.name == target) {
									child.gameObject.SetActive(true);
								}
									
							}

						break;

						case "Axon":
						
							highlights[0].gameObject.SetActive(true);
							foreach (Transform child in highlights[0].gameObject.transform)
							{
								child.gameObject.SetActive(false);
								if(child.name == target) {
									child.gameObject.SetActive(true);
								}
									
							}

							textHighlights.gameObject.SetActive(true);
							foreach (Transform child in textHighlights.gameObject.transform)
							{
								child.gameObject.SetActive(false);
								if(child.name == target) {
									child.gameObject.SetActive(true);
								}
									
							}							

						break;

						case "Dendrite":
						
							highlights[0].gameObject.SetActive(true);
							foreach (Transform child in highlights[0].gameObject.transform)
							{
								child.gameObject.SetActive(false);
								if(child.name == target) {
									child.gameObject.SetActive(true);
								}
									
							}

							textHighlights.gameObject.SetActive(true);
							foreach (Transform child in textHighlights.gameObject.transform)
							{
								child.gameObject.SetActive(false);
								if(child.name == target) {
									child.gameObject.SetActive(true);
								}
									
							}

						break;

					}

				break;

				case "Nissl Stain":
				
					switch (target){

						case "Neuron":
						
							highlights[1].gameObject.SetActive(true);
							foreach (Transform child in highlights[1].gameObject.transform)
							{
								child.gameObject.SetActive(false);
								if(child.name == target) {
									child.gameObject.SetActive(true);
								}
									
							}

							textHighlights.gameObject.SetActive(true);
							foreach (Transform child in textHighlights.gameObject.transform)
							{
								child.gameObject.SetActive(false);
								if(child.name == target) {
									child.gameObject.SetActive(true);
								}
									
							}

						break;

						case "Astrocyte":
						
							highlights[1].gameObject.SetActive(true);
							foreach (Transform child in highlights[1].gameObject.transform)
							{
								child.gameObject.SetActive(false);
								if(child.name == target) {
									child.gameObject.SetActive(true);
								}
									
							}

							textHighlights.gameObject.SetActive(true);
							foreach (Transform child in textHighlights.gameObject.transform)
							{
								child.gameObject.SetActive(false);
								if(child.name == target) {
									child.gameObject.SetActive(true);
								}
									
							}

						break;

					}

				break;

				case "Kluver-Barrera Stain":
				
					switch (target){

						case "Neuron":
						
							highlights[2].gameObject.SetActive(true);
							foreach (Transform child in highlights[2].gameObject.transform)
							{
								child.gameObject.SetActive(false);
								if(child.name == target) {
									child.gameObject.SetActive(true);
								}
									
							}

							textHighlights.gameObject.SetActive(true);
							foreach (Transform child in textHighlights.gameObject.transform)
							{
								child.gameObject.SetActive(false);
								if(child.name == target) {
									child.gameObject.SetActive(true);
								}
									
							}

						break;

						case "Astrocyte":
						
							highlights[2].gameObject.SetActive(true);
							foreach (Transform child in highlights[2].gameObject.transform)
							{
								child.gameObject.SetActive(false);
								if(child.name == target) {
									child.gameObject.SetActive(true);
								}
									
							}

							textHighlights.gameObject.SetActive(true);
							foreach (Transform child in textHighlights.gameObject.transform)
							{
								child.gameObject.SetActive(false);
								if(child.name == target) {
									child.gameObject.SetActive(true);
								}
									
							}							

						break;

						case "Myelin":
						
							highlights[2].gameObject.SetActive(true);
							foreach (Transform child in highlights[2].gameObject.transform)
							{
								child.gameObject.SetActive(false);
								if(child.name == target) {
									child.gameObject.SetActive(true);
								}
									
							}

							textHighlights.gameObject.SetActive(true);
							foreach (Transform child in textHighlights.gameObject.transform)
							{
								child.gameObject.SetActive(false);
								if(child.name == target) {
									child.gameObject.SetActive(true);
								}
									
							}

						break;

					}

				break;

			}

	}

	public void Activate3D (string target){

		foreach (GameObject grayPart in parts3Dgray) {
			grayPart.gameObject.SetActive (true);
		}

		foreach (GameObject highlightedPart in parts3Dhighlight) {
			highlightedPart.gameObject.SetActive (false);
		}

		switch (activeStain){

				case "Cajal Stain":
				
					switch (target){

						case "Neuron":
						
							parts3Dgray[0].gameObject.SetActive(false);
							parts3Dgray[1].gameObject.SetActive(false);

							parts3Dhighlight[0].gameObject.SetActive(true);
							parts3Dhighlight[3].gameObject.SetActive(true);

							Content3D.gameObject.SetActive(true);

						break;

						case "Astrocyte":
						
							Content3D.gameObject.SetActive(false);

						break;

						case "Axon":

							parts3Dgray[2].gameObject.SetActive(false);
							parts3Dgray[3].gameObject.SetActive(false);

							parts3Dhighlight[6].gameObject.SetActive(true);
							parts3Dhighlight[7].gameObject.SetActive(true);
							
							Content3D.gameObject.SetActive(true);

						break;

						case "Dendrite":

							parts3Dgray[4].gameObject.SetActive(false);

							parts3Dhighlight[8].gameObject.SetActive(true);
							
							Content3D.gameObject.SetActive(true);

						break;

					}

				break;

				case "Nissl Stain":
				
					switch (target){

						case "Neuron":

							parts3Dgray[0].gameObject.SetActive(false);
							parts3Dgray[1].gameObject.SetActive(false);

							parts3Dhighlight[1].gameObject.SetActive(true);
							parts3Dhighlight[4].gameObject.SetActive(true);
							
							Content3D.gameObject.SetActive(true);

						break;

						case "Astrocyte":
						
							Content3D.gameObject.SetActive(false);

						break;

					}

				break;

				case "Kluver-Barrera Stain":
				
					switch (target){

						case "Neuron":

							parts3Dgray[0].gameObject.SetActive(false);
							parts3Dgray[1].gameObject.SetActive(false);

							parts3Dhighlight[2].gameObject.SetActive(true);
							parts3Dhighlight[5].gameObject.SetActive(true);
							
							Content3D.gameObject.SetActive(true);

						break;

						case "Astrocyte":
						
							Content3D.gameObject.SetActive(false);

						break;

						case "Myelin":

							parts3Dgray[5].gameObject.SetActive(false);

							parts3Dhighlight[9].gameObject.SetActive(true);
							
							Content3D.gameObject.SetActive(true);

						break;

					}

				break;

		}

	}

	public void Enable3D() {

		if (toggle3D.gameObject.transform.GetComponent<Toggle>().isOn) {

			neuron3D.gameObject.SetActive(true);

		} else {

			neuron3D.gameObject.SetActive(false);
		
		}

		/*
		if (toggle3D.gameObject.transform.GetComponent<Toggle>().isOn) {

			foreach (GameObject part in parts3Dgray){
				if (part.gameObject.activeSelf == false) {
					last3DGrey = part;
				}
				part.gameObject.SetActive(false);
			}

			foreach (GameObject part in parts3Dhighlight){
				if (part.gameObject.activeSelf == true) {
					last3DHighlight = part;
				}
				part.gameObject.SetActive(false);
			}

		} else {
			
			foreach (GameObject part in parts3Dgray){
				if (part == last3DGrey) {
					part.gameObject.SetActive(false);
				} else {
					part.gameObject.SetActive(true);
				}
				
			}

			foreach (GameObject part in parts3Dhighlight){

				if (part == last3DHighlight) {
					part.gameObject.SetActive(true);
				} else {
					part.gameObject.SetActive(false);
				}

			}
 
		}
		*/

	}

}
