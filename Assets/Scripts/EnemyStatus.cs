using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour {
    Transform player;

    public static float relaxed {set; get;} = 0.0f;    // リラックス度
    public static float mind {set; get;} = 0.0f;       // 集中度

    void Start() {
        player = GameObject.FindWithTag("Player").transform;
    }
}