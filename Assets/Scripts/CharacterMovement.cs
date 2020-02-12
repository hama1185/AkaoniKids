using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
    Rigidbody character;
    // VelocityController velocityController;
    float sensitivity;
    (string moveH, string moveV, string viewH, string viewV) key;
    
    void Start() {
        character = this.GetComponent<Rigidbody>();
        // velocityController = this.GetComponent<VelocityController>();
        sensitivity = 1.5f;
        key = KeyConfig.SetKeyConfig();
    }

    void Update() {
        this.transform.rotation = Quaternion.Euler(Vector3.zero);
        this.transform.position = new Vector3(this.transform.position.x, 1.2f, this.transform.position.z);
        
        
        
        // // / RealSenseによる入力(ここから)
        character.velocity = VelocityController.velocity;
        // // / (ここまで)

        // Debug.Log("speed : " + character.velocity.magnitude + "    vecter : " + character.velocity);
        // Debug.Log(Time.deltaTime);




        // // / ゲームパッドによる入力(ここから)
        // Vector3 angle = Vector3.zero;
        // float rotH = 0.0f;
        // float rotV = 0.0f;

        // rotH = Input.GetAxis(key.viewH) * sensitivity;
	    // // rotV = Input.GetAxis(key.viewV) * sensibility;

        // angle = new Vector3(rotV, rotH, 0.0f);

        // this.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).transform.Rotate(angle);

        // Vector3 movement = Vector3.zero;
		// float moveLR = 0.0f;
        // float moveFB = 0.0f;
        
        // moveLR = Input.GetAxis(key.moveH) * VelocityController.speed;
	    // moveFB = Input.GetAxis(key.moveV) * VelocityController.speed;

        // movement = new Vector3(moveLR, 0.0f, moveFB);
        // movement = this.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).rotation * movement;

        // character.velocity = movement;
        // // / (ここまで)
	}
}