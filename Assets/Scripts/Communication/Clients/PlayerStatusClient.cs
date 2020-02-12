using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityOSC;
using System.Text;
using System.Threading.Tasks;

public class PlayerStatusClient : MonoBehaviour {
    // Start is called before the first frame update
    #region Network Settings //----------追記
    public string ip;
    public int port;
	#endregion //----------追記
    // Start is called before the first frame update

    float elapsedTime = 0.0f;
    float interval = 0.033f;
    GameObject CameraAngle;

    void Awake() {
        IpGetter ipGetter = new IpGetter();
        string myIP = ipGetter.GetIp();

        if (myIP == HostList.phone1.ip) {
            ip = HostList.phone2.ip;
            port = HostList.phone1.port_status;
        }
        else {
            ip = HostList.phone1.ip;
            port = HostList.phone2.port_status;
        }
        // Debug.Log("client IP : " + ip + "   port : " + port);

        OSCHandler.Instance.clientInit(HostList.clientID.enemy, ip,port);//ipには接続先のipアドレスの文字列を入れる。
    }
    
    void Start() {
        CameraAngle = this.transform.GetChild(0).GetChild(0).gameObject;
    }

    void Update() {
        elapsedTime += Time.deltaTime;
        
        if (elapsedTime > interval) {
            List<float> positionList = new List<float>();
            float eulerY;

            eulerY = CameraAngle.transform.eulerAngles.y;

            positionList.Add(transform.position.x);
            positionList.Add(transform.position.y);
            positionList.Add(transform.position.z);
            positionList.Add(eulerY);

            OSCHandler.Instance.SendMessageToClient(HostList.clientID.enemy,"/position",positionList);//Akaoniでいいのかな
        }
    }

    void LateUpdate() {
        if (elapsedTime > interval) {
            List<float> statusList = new List<float>();

            statusList.Add(PlayerStatus.relaxed);
            statusList.Add(PlayerStatus.mind);

            OSCHandler.Instance.SendMessageToClient(HostList.clientID.enemy,"/status",statusList);//Akaoniでいいのかな

            elapsedTime = 0.0f;
        }
    }
}
