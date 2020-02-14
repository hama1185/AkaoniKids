using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraAdjuster : MonoBehaviour {
    public Quaternion sentAngle{get;set;} = new Quaternion(0,0,0,1);
    public void Adjust(){
        // eulerX = 360.0f - eulerX;	
        // if (eulerX > 180.0f) {	
        //     eulerX -= 360.0f;	
        // }	

        // eulerY = 360.0f - eulerY;	
        // if (eulerY > 180.0f) {	
        //     eulerY -= 360.0f;	
        // }	

        // eulerZ = 360.0f - eulerZ;	
        // if (eulerZ > 180.0f) {	
        //     eulerZ -= 360.0f;	
        // }
        this.transform.localRotation = Quaternion.Euler(-sentAngle.eulerAngles.x, -sentAngle.eulerAngles.y, sentAngle.eulerAngles.z);
    }
}