using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VuforiaManagement : MonoBehaviour                                   
{

	public static VuforiaManagement instance;

	public delegate void ActivationEvent (bool activation);
	public static event ActivationEvent activationEvent;

	public bool targetFound = false;
	public bool contentActivation = false;
	public GameObject trackableInUse;
	public float distanceToTarget;
	public GameObject arCamera;

	public bool closeEnough = false;

	public void DemoContentActivation () {
		contentActivation = true;
		if (activationEvent != null) {
			activationEvent(contentActivation);
		}
	}

	public void DemoContentDeactivation () {
		contentActivation = false;
		if (activationEvent != null) {
			activationEvent(contentActivation);
		}
	}



	private void Awake() {

		instance = this;
		
	}

	void Start() {
		arCamera = GameObject.Find("ARCamera").gameObject;
	}

	void Update(){

		 if (DefaultTrackableEventHandler.detection == true){
			//targetFound = true;
			TrackableFeedback(true, DefaultTrackableEventHandler.trackableName);
		 } else {
			TrackableFeedback(false, DefaultTrackableEventHandler.trackableName);
		 }

		/* 
		if (targetFound){
			DistanceToTarget();
			Animations.instance.DistanceBars(distanceToTarget);
		}
		*/
	}

	public void TrackableFeedback(bool detection, string trackableName) {

		if (detection){
			trackableInUse = GameObject.Find(trackableName).gameObject;
			print ("Found: " + trackableInUse.name);
			targetFound = true;
			DistanceToTarget();
			Animations.instance.DistanceBars(distanceToTarget);
		} else {
			targetFound = false;
			Animations.instance.EnableAugment(false);
			contentActivation = false;
		}
		
	}

	
	public void DistanceToTarget () {

		Vector3 delta = arCamera.transform.position - (trackableInUse.transform.position);
  		distanceToTarget = delta.magnitude - 6;
		//print(distanceToTarget);
	
	}
	

	public void EnableARContent () {

		switch (trackableInUse.name) {
			case "TEST_4":

			break;

			case "DEMO_01_MARKER":

				if (targetFound){

					var rendererComponents = trackableInUse.GetComponentsInChildren<Renderer>(true);
        			var colliderComponents = trackableInUse.GetComponentsInChildren<Collider>(true);
       				var canvasComponents = trackableInUse.GetComponentsInChildren<Canvas>(true);

					if (closeEnough){
				
						DemoContentActivation ();

						// Enable rendering:
						foreach (var component in rendererComponents)
							component.enabled = true;

						// Enable colliders:
						foreach (var component in colliderComponents)
							component.enabled = true;

						// Enable canvas:
						foreach (var component in canvasComponents)
							component.enabled = true;

					} else {

						contentActivation = false;

						// Enable rendering:
						foreach (var component in rendererComponents)
							component.enabled = false;

						// Enable colliders:
						foreach (var component in colliderComponents)
							component.enabled = false;
							

						// Enable canvas':
						foreach (var component in canvasComponents)
							component.enabled = false;
					}

				}

			break;

			case "DEMO_02_MARKER_02":

			break;

			case "DEMO_04_PROJECT_ELCAR_MARKER":

				Demo04Management.instance.activeScanAnim = true;
				Demo04Management.instance.ScanAnimation();

			break;
		}

	}

	public void VuforiaErrorMessage () {
		Screens.instance.ScreenChange(6);
	}


    /*
	public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            // Play audio when target is found
            //audio.Play();
        }
        else
        {
            // Stop audio when target is lost
            //audio.Stop();
        }
    }
	*/
	
}
