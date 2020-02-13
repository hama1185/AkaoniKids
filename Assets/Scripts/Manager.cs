using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
    public Text resultText;
    static float time = 0.0f;
    float end = 100.0f;
    static public bool setuped = false;
    static public bool pointedFlag = false;
    static public bool preparedFlag = false;
    static public bool startFlag = false;
    static public bool endFlag = false;
    public static Vector3 spawnPoint {set; get;} = Vector3.zero;
    float setupTime = 0.0f;
    float setupEnd = 60.0f;
    public Text timeText;

    void Update() {
        if (setupTime > setupEnd && !setuped) {
            setuped = true;
            RealSenceClient.StartRealSense();
            timeText.transform.position = new Vector3(-300.0f, 170.0f, 0.0f);
            timeText.text = "";
        }
        else if (!setuped){
            setupTime += Time.deltaTime;
            timeText.text = "  準備中...    " + (setupEnd - setupTime).ToString("f2");
            // Debug.Log(setupTime);
        }
        else {
            GameStart();
        }

        if (startFlag) {
            time += Time.deltaTime;
            if (time > 60.0f) {
                StartOgreMove();
            }
        }
        // if (spawnPoint != Vector3.zero && !pointedFlag) {
        //     // CreateSpawnPointMarker(spawnPoint);
        //     // pointedFlag = true;
        // }
        // if (startFlag) {
        //     time += Time.deltaTime;
        //     timeText.text = "time : " + (end - time).ToString();
        //     if (time >= end && !endFlag) {
        //         Debug.Log("Time Up");
        //         endFlag = true;
        //         if(GameObject.FindWithTag("Player").name == "Villager"){
        //             resultText.text = "You Win!!!";
        //         }
        //         else{
        //             resultText.text = "You Lose...";
        //         }
        //     }
        // }
    }

    public static void Prepared() {
        preparedFlag = true;

        if(GameObject.FindWithTag("Player").name == "Ogre") {
            Master.flagCount++;
        }
        else {
            FlagClient.ReturnPflag();
        }
        // preparedFlag が true になった後に地形生成をする
        // 本来は地形生成後 startedFlag を true にする
    }

    public static void GameStart() {
        startFlag = true;
        GameObject.FindWithTag("Enemy").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("Walls").transform.GetChild(0).gameObject.SetActive(true);
        if(GameObject.FindWithTag("Player").name == "Ogre"){
            GameObject.FindWithTag("Enemy").GetComponent<FootSpawn>().enabled = true;
            /// ↓ debug中はコメントアウトしてる
            // GameObject.FindWithTag("Player").transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject.GetComponent<PostEffect>().enabled = true;
        }
    }

    public static void CreateSpawnPointMarker(Vector3 spawnPoint) {
        // GameObject obj = (GameObject)Resources.Load("Prefabs/SpawnPoint 1");
        GameObject obj = (GameObject)Resources.Load("Prefabs/SpawnPoint");
        Instantiate(obj, spawnPoint, Quaternion.identity);
    }

    void StartOgreMove() {
        GameObject.FindWithTag("Enemy").GetComponent<AutoOgreMove>().enabled = true;
    }
}