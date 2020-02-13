using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityOSC;
using System.Text;
using System.Threading.Tasks;

public class AudienceEnemyClient : MonoBehaviour {
    // Start is called before the first frame update
    #region Network Settings //----------追記
    public string ip;
    public int port;
	#endregion //----------追記
    // Start is called before the first frame update

    string address;

    GameObject CameraAngle;

    void Awake() {
        IpGetter ipGetter = new IpGetter();
        string myIP = ipGetter.GetIp();

        ip = HostList.ip_audience;
        if (myIP == HostList.phone1.ip) {
            port = HostList.phone1.port_audienceserver;
        }
        else {
            port = HostList.phone2.port_audienceserver;
        }
        // Debug.Log("client IP : " + ip + "   port : " + port);

        OSCHandler.Instance.clientInit(HostList.clientID.audience, ip,port);//ipには接続先のipアドレスの文字列を入れる。
    }
    
    void Start() {

        if(GameObject.FindWithTag("Enemy").name == "Ogre"){
            address = "/Ogre";
        }
        else {
            address = "/Villager";
        }
    }

    void Update() {
        // Debug.Log("a");
        List<float> statusList = new List<float>();

        statusList.Add(this.transform.position.x);
        statusList.Add(this.transform.position.y);
        statusList.Add(this.transform.position.z);

        statusList.Add(0.0f);
        statusList.Add(0.0f);
        statusList.Add(0.0f);
        statusList.Add(1.0f);

        statusList.Add(0.0f);
        statusList.Add(0.0f);

        OSCHandler.Instance.SendMessageToClient(HostList.clientID.audience, address, statusList);//Akaoniでいいのかな
    }
}