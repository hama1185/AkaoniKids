using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEffectController : MonoBehaviour {
    public Material red;
    float max = 0.1f;

    void Update() {
        red.SetFloat("_Darkness", max - max * PlayerStatus.mind/100.0f);
    }
}