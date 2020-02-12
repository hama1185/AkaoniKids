using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityOSC;
using System.Text;
using System.Threading.Tasks;

public class RealSenceClient : MonoBehaviour {
    // Start is called before the first frame update
    #region Network Settings //----------追記
    public string ip;
    public int port;
	#endregion //----------追記
    // Start is called before the first frame update

    void Awake() {
        IpGetter ipGetter = new IpGetter();
        string myIP = ipGetter.GetIp();

        if (myIP == HostList.phone1.ip) {
            ip = HostList.phone1.ip_raspberrypi;
            port = HostList.phone1.port_raspberrypi;
        }
        else {
            ip = HostList.phone2.ip_raspberrypi;
            port = HostList.phone2.port_raspberrypi;
        }
        // Debug.Log("client IP : " + ip + "   port : " + port);

        OSCHandler.Instance.clientInit(HostList.clientID.raspberrypi, ip,port);//ipには接続先のipアドレスの文字列を入れる。
    }

    static public async void StartRealSense(){
        for (int i = 0; i < 20; i++) {
            OSCHandler.Instance.SendMessageToClient(HostList.clientID.raspberrypi, "/start", "start");
            await Task.Delay(15);
        }
    }
}