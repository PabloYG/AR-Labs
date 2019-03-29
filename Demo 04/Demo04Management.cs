using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demo04Management : MonoBehaviour {

	public static Demo04Management instance;
    public bool activeScanAnim = true;

	public bool leanTouchActive = false;
    public Animator scanAnimator;
    public GameObject infoScreen;


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
			if (!IsPointerOverUIObject() && !leanTouchActive && Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftControl)){
				RaycastCollision(hit.transform.name.ToString());
			}
		} else {
			if (!leanTouchActive && !IsPointerOverUIObject() && Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftControl)){
				//CloseEverything ();
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
			if (!leanTouchActive && !IsPointerOverUIObject() && Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended){
				//CloseEverything ();
			}
		}


#endif

	}

    public void ScanAnimation () {

        if (activeScanAnim) {
            scanAnimator.SetTrigger("Scan");
            activeScanAnim = false;
        } else {
            //Do nothing
        }

    }

	public void RaycastCollision (string hitName) {

        if (hitName == "Trigger") {
            infoScreen.SetActive(true);
        }
        
	}

    public void CloseInfo () {
        infoScreen.SetActive(false);
    }

}
