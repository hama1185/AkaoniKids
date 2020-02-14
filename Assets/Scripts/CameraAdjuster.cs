using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraAdjuster : MonoBehaviour {
    public float sentAngleX{get; set;} = 0.0f;
    public float sentAngleY{get; set;} = 0.0f;
    public float sentAngleZ{get; set;} = 0.0f;
    public void Adjust(){
        this.transform.localRotation = Quaternion.Euler(new Vector3(sentAngleX, sentAngleY, sentAngleZ));
    }
}