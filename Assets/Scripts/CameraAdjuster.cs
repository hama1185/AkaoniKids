using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraAdjuster : MonoBehaviour {
    public Quaternion sentAngle{get;set;} = new Quaternion(0,0,0,1);
    public void Adjust(){
        this.transform.localRotation = sentAngle;
    }
}