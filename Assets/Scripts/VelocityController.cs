using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VelocityController : MonoBehaviour {
    public static Vector3 inputAxis_Left {get; set;} = Vector3.zero;
    public static Vector3 velocity {get; set;}
    static Vector2 fieldSize {get; set;} = new Vector2(100.0f, 100.0f);
    public static Vector2 playArea {get; set;} = new Vector2(20.0f, 20.0f);
    static float MAX_SPEED = 5.0f;
    public static float speed = 5.0f;
    static float level;
    (string moveH, string moveV, string viewH, string viewV) key;
    [SerializeField] Text message;

    void Start() {
        MAX_SPEED = 5.0f * (20.0f / playArea.x);
        speed = fieldSize.x / playArea.x;
        level = 0.2f * speed;
        key = KeyConfig.SetKeyConfig();
        message.enabled = false;
    }

    void Update() {
        inputAxis_Left *= speed;

        float magnitude = Mathf.Sqrt(Mathf.Pow(inputAxis_Left.x, 2) + Mathf.Pow(inputAxis_Left.z, 2));
        
        if (magnitude > MAX_SPEED) {
            inputAxis_Left = new Vector3(inputAxis_Left.x * MAX_SPEED / magnitude, 0.0f, inputAxis_Left.z * MAX_SPEED / magnitude);
            message.enabled = true;
        }
        else {
            message.enabled = false;
        }

        if (magnitude < level) {
            velocity = Vector3.zero;
        }
        else {
            velocity = inputAxis_Left;
        }

        // Debug.Log(Time.deltaTime);
    }

    public static void SetParameter() {
        MAX_SPEED = 5.0f * (20.0f / playArea.x);
        speed = fieldSize.x / playArea.x;
        level = 0.18f * speed;
    }
}
