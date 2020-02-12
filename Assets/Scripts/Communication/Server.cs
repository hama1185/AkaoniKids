using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityOSC;
using System.Text;
using System.Threading.Tasks;

public class Server : MonoBehaviour {
    // Start is called before the first frame update
    #region Network Settings //----------追記
	public string serverName;
	public int inComingPort; //----------追記
	#endregion //----------追記

	private Dictionary<string, ServerLog> servers;
    // public Text a;
    // public Text b;
    // public Text c;
    void Awake() {
        if(this.name == "Ogre"){
            serverName = "Astatus";
            inComingPort = HostList.phone1.port_audienceserver;
            // a.text = "ogre";
        }
        else{
            serverName = "Bstatus";
            inComingPort = HostList.phone2.port_audienceserver;
            // a.text = "villager";
        }
        
        // Debug.Log("server IP : " + serverName + "   port : " + inComingPort);

        OSCHandler.Instance.serverInit(serverName,inComingPort); //init OSC　//----------変更
        servers = new Dictionary<string, ServerLog>();
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
                
                Debug.Log("b");
                // b.text = "value get";
				int lastPacketIndex = item.Value.packets.Count - 1;
                
                Vector3 position;
                Quaternion camRotation;
                float angleY;
                position.x = (float)item.Value.packets[lastPacketIndex].Data[0];
                position.y = (float)item.Value.packets[lastPacketIndex].Data[1];
                position.z = (float)item.Value.packets[lastPacketIndex].Data[2];

                camRotation.x = (float)item.Value.packets[lastPacketIndex].Data[3];
                camRotation.y = (float)item.Value.packets[lastPacketIndex].Data[4];
                camRotation.z = (float)item.Value.packets[lastPacketIndex].Data[5];
                camRotation.w = (float)item.Value.packets[lastPacketIndex].Data[6];

                angleY = camRotation.eulerAngles.y;

				// if(item.Value.packets[lastPacketIndex].Address.ToString() == "/Ogre"){
                //     c.text = "ogre sent";
                //     OgreStatus.angleY = angleY;
                //     OgreStatus.position = position;
                //     OgreStatus.quaternion = camRotation;
				// }
                // else{
                //     VillagerStatus.angleY = angleY;
                //     VillagerStatus.position = position;
                //     VillagerStatus.quaternion = camRotation;
                //     c.text = "vllager sent";
                // }
			}
		}
        // Debug.Log(Time.deltaTime);
    }
}