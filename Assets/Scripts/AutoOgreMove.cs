using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoOgreMove : MonoBehaviour {
    Transform ogre;

    void Start() {
        ogre = GameObject.FindWithTag("Enemy").transform;
        ogre.position = new Vector3(0.0f, 1.0f, -51.0f);
    }

    void Update() {
        if (ogre.position.z < -5.0f) {
            ogre.position += new Vector3(0.0f, 0.0f, 2.0f * Time.deltaTime);
        }
    }
}