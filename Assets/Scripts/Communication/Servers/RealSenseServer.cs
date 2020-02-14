using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityOSC;
using System.Text;
using System.Threading.Tasks;

public class RealSenseServer : MonoBehaviour {
    // Start is called before the first frame update
    #region Network Settings //----------追記
	public string serverName;
	public int inComingPort; //----------追記
	#endregion //----------追記

	private Dictionary<string, ServerLog> servers;
    
    CameraAdjuster cameraAdjuster;


    void Awake() {
        IpGetter ipGetter = new IpGetter();
        string myIP = ipGetter.GetIp();

        if (myIP == HostList.phone1.ip) {
            inComingPort = HostList.phone2.port_realsense;
        }
        else {
            inComingPort = HostList.phone1.port_realsense;
        }
        serverName = HostList.serverName.realsense;

        // Debug.Log("server IP : " + serverName + "   port : " + inComingPort);

        OSCHandler.Instance.serverInit(serverName,inComingPort); //init OSC　//----------変更
        servers = new Dictionary<string, ServerLog>();
        cameraAdjuster = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<CameraAdjuster>();
    }

    // Update is called once per frame

    void Update() {
        OSCHandler.Instance.UpdateLogs();
		servers = OSCHandler.Instance.Servers;
    }
    
    void LateUpdate(){
        foreach( KeyValuePair<string, ServerLog> item in servers ){
			// If we have received at least one packet,
			// show the last received from the log in the Debug console
			if(item.Value.log.Count > 0){
				int lastPacketIndex = item.Value.packets.Count - 1;

				// UnityEngine.Debug.Log(String.Format("SERVER: {0} ADDRESS: {1} VALUE 0: {2}",
				// 	item.Key, // Server name
				// 	item.Value.packets[lastPacketIndex].Address, // OSC address
				// 	item.Value.packets[lastPacketIndex].Data[0].ToString())); //First data value

                if(item.Value.packets[lastPacketIndex].Address.ToString() == "/input"){
                    Vector3 velocity;
                    Quaternion rot;

                    velocity.x = (float)item.Value.packets[lastPacketIndex].Data[0];
                    velocity.y = 0.0f;
                    velocity.z = (float)item.Value.packets[lastPacketIndex].Data[1];
                    rot.x = (float)item.Value.packets[lastPacketIndex].Data[2];
                    rot.y = (float)item.Value.packets[lastPacketIndex].Data[3];
                    rot.z = (float)item.Value.packets[lastPacketIndex].Data[4];
                    rot.w = (float)item.Value.packets[lastPacketIndex].Data[5];
                    
                    VelocityController.inputAxis_Left = velocity;

                    // CameraAdjusterにRealSenseから送られてきたrot.eulerAngles.yを割り当てる (float型)
                    // y軸中心の回転のずれだけを補正する (x,z軸についてもずれを補正するのはめんどそう)
                    float eulerY = rot.eulerAngles.y;

                    eulerY = 360.0f - eulerY;
                    if (eulerY > 180.0f) {
                        eulerY -= 360.0f;
                    }

                    // Debug.Log(eulerY);
                    cameraAdjuster.sentAngle = eulerY;
                    cameraAdjuster.Adjust();
                }
			}
		}
        // Debug.Log(Time.deltaTime);
    }
}