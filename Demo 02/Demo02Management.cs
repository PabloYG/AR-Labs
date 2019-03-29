using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demo02Management : MonoBehaviour {

	public static Demo02Management instance;
	//public GameObject[] highlights;
	//public GameObject textHighlights;
	public GameObject Content3D;
	public GameObject[] graphs3D;
	public bool active3D;
	//public GameObject toggle3D;

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
		//ChangeStain("Cajal Stain Collider");
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
			if (!leanTouchActive && Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) {
				RaycastCollision(hit.transform.name.ToString());
        	}
		} else {
			if (!leanTouchActive && !IsPointerOverUIObject() && Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended){
				CloseEverything ();
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

		/*
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
		 */
	}

	public void RaycastCollision (string hitName) {


		switch (hitName){
		
			case "Hyperboloid Text Collider":
				//ActivateHighlights ("Neuron");
				Activate3D ("Hyperboloid");
			break;

			case "Hyperbolic Paraboloid Text Collider":
				//ActivateHighlights ("Astrocyte");
				Activate3D ("Paraboloid");
			break;

			}

	}

	/*
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
	 */

	/*
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
	 */

	public void Activate3D (string target){

		switch (target){

			case "Hyperboloid":

				Content3D.gameObject.SetActive(true);
				graphs3D[0].gameObject.SetActive(true);
				graphs3D[1].gameObject.SetActive(false);

			break;

			case "Paraboloid":
						
				Content3D.gameObject.SetActive(true);
				graphs3D[0].gameObject.SetActive(false);
				graphs3D[1].gameObject.SetActive(true);

			break;
		}

	}
	/*
	public void Enable3D() {

		if (toggle3D.gameObject.transform.GetComponent<Toggle>().isOn) {

			neuron3D.gameObject.SetActive(true);

		} else {

			neuron3D.gameObject.SetActive(false);
		
		}

		
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
		

	}
	*/

}

