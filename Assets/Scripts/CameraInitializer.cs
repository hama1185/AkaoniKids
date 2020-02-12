using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInitializer : MonoBehaviour {
    GameObject phoneCam;

    void Start() {
        /// ゲームパッド入力時 GetChild 0 0 0
        /// RealSense入力時   GetChild 0 0 0 1
        // phoneCam = this.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        phoneCam = this.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject;
    }

    void Update() {
        if (!Manager.setuped) {
            this.transform.rotation = Quaternion.Inverse(Quaternion.Euler(0.0f, phoneCam.transform.localEulerAngles.y, 0.0f));
        }
    }
}
