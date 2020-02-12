using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeSharder : MonoBehaviour
{
    private Material material;
    float r;
    float g;
    float b;
    // Start is called before the first frame update
    void Start(){
        material = this.GetComponent<Renderer>().material;
        r = material.color.r;
        g = material.color.g;
        b = material.color.b;
    }

    // Update is called once per frame
    void Update(){
        // Materialクラスの`Set****`メソッドを使ってシェーダに値を送信
        
        material.SetColor("_Color", new Color(r, g, b, PlayerStatus.mind / 100));
    }
}
