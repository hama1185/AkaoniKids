using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsController : MonoBehaviour {
    AudioClip heartBeat;
    AudioClip footStep;
    AudioSource heartBeatAudio;
    AudioSource footStepAudio;
    AudioReverbZone audioZone;
    GameObject player;

    static public float vol = 0.0f;

    void Start() {
        player = GameObject.FindWithTag("Player");
        heartBeat = (AudioClip)Resources.Load("Sounds/HeartBeat");
        footStep = (AudioClip)Resources.Load("Sounds/HeartBeat");
        heartBeatAudio = GameObject.FindWithTag("Player").transform.GetChild(1).GetComponent<AudioSource>();
        footStepAudio = GameObject.FindWithTag("Enemy").transform.GetChild(1).GetComponent<AudioSource>();
        audioZone = GameObject.FindWithTag("Player").transform.GetChild(1).GetComponent<AudioReverbZone>();
        heartBeatAudio.loop = true;
        footStepAudio.loop = true;
    }

    void Update() {
        float eachOtherDistance = Vector3.Distance(player.transform.position,EnemyPositionTracker.enemyPosition);

        if (Manager.startFlag) {
            heartBeatAudio.volume = 1.0f - 8 * (PlayerStatus.relaxed) / 1000.0f;//0.2から1.0
            heartBeatAudio.pitch = 1.4f - (PlayerStatus.relaxed) / 160.0f;
        }
        else {
            heartBeatAudio.volume = 0.0f;
        }
        
        
        // footStepAudio.volume = 0.5f + EnemyStatus.mind / 2.0f;
        if(eachOtherDistance >=  50 - 7 * EnemyStatus.relaxed / 20 || eachOtherDistance > 50.0f){//25
            footStepAudio.Stop();
        }
        else if (Manager.startFlag && !footStepAudio.isPlaying) {
            footStepAudio.Play();
        }
        //10から30
    }
}