using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityOSC;
using System.Text;
using System.Threading.Tasks;

public class AudienceClient : MonoBehaviour {
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
        CameraAngle = this.transform.GetChild(0).GetChild(0).gameObject;

        if(GameObject.FindWithTag("Player").name == "Ogre"){
            address = "/Ogre";
        }
        else {
            address = "/Villager";
        }
    }

    void Update() {
        // Debug.Log("a");
        List<float> charList = new List<float>();

        charList.Add(this.transform.position.x);
        charList.Add(this.transform.position.y);
        charList.Add(this.transform.position.z);

        charList.Add(CameraAngle.transform.rotation.x);
        charList.Add(CameraAngle.transform.rotation.y);
        charList.Add(CameraAngle.transform.rotation.z);
        charList.Add(CameraAngle.transform.rotation.w);

        charList.Add(PlayerStatus.mind);
        charList.Add(PlayerStatus.relaxed);

        OSCHandler.Instance.SendMessageToClient(HostList.clientID.audience, address, charList);//Akaoniでいいのかな
    }
}