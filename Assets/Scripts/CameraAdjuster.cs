using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraAdjuster : MonoBehaviour {
    public float sentAngle{get; set;} = 0.0f;
    public void Adjust(){
        this.transform.localRotation = Quaternion.Euler(new Vector3(0.0f, sentAngle - this.transform.localEulerAngles.y, 0.0f));
    }
}