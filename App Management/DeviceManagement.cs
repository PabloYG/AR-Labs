using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DeviceManagement : MonoBehaviour {

    void Update() {
        if (Screens.instance.activeScreen == 0) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Application.Quit(); 
            }
        } else {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                UIInput.instance.ButtonDirection("Return Button");
            }
        }
        
    }


}
