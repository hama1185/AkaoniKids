using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmpireServer : MonoBehaviour {    // Start is called before the first frame update
    #region Network Settings //----------追記
	public string serverName;
	public int inComingPort; //----------追記
	#endregion //----------追記

	private Dictionary<string, ServerLog> servers;

    void Awake() {
        IpGetter ipGetter = new IpGetter();
        string myIP = ipGetter.GetIp();

        if (myIP == HostList.phone1.ip) {
            inComingPort = HostList.phone2.port_umpireReceive;
        }
        else {
            inComingPort = HostList.phone1.port_umpireReceive;
        }
        serverName = HostList.serverName.umpire;

        // Debug.Log("server IP : " + serverName + "   port : " + inComingPort);

        OSCHandler.Instance.serverInit(serverName,inComingPort); //init OSC　//----------変更
        servers = new Dictionary<string, ServerLog>();
    }

    // Update is called once per frame

    void Update() {
        OSCHandler.Instance.UpdateLogs();
		servers = OSCHandler.Instance.Servers;
    }
    
    void LateUpdate() {
        foreach( KeyValuePair<string, ServerLog> item in servers ){
			// If we have received at least one packet,
			// show the last received from the log in the Debug console
			if(item.Value.log.Count > 0){
				int lastPacketIndex = item.Value.packets.Count - 1;

				// UnityEngine.Debug.Log(String.Format("SERVER: {0} ADDRESS: {1} VALUE 0: {2}",
				// 	item.Key, // Server name
				// 	item.Value.packets[lastPacketIndex].Address, // OSC address
				// 	item.Value.packets[lastPacketIndex].Data[0].ToString())); //First data value

				if(item.Value.packets[lastPacketIndex].Address.ToString() == "/ManageSpawn"){
                    // Debug.Log("aaa");
                    Vector3 spawnPosition;
                    spawnPosition.x = (float)item.Value.packets[lastPacketIndex].Data[0];
                    spawnPosition.y = 0.0f;
                    spawnPosition.z = (float)item.Value.packets[lastPacketIndex].Data[1];
                    if (!Manager.pointedFlag) {
                        Manager.spawnPoint = spawnPosition;
                    }
				}
                if(item.Value.packets[lastPacketIndex].Address.ToString() == "/Mindstatus"){
                    // Debug.Log("bbb");
                    PlayerStatus.relaxed = (float)item.Value.packets[lastPacketIndex].Data[0];
                    PlayerStatus.mind = (float)item.Value.packets[lastPacketIndex].Data[1];
				}
                if(item.Value.packets[lastPacketIndex].Address.ToString() == "/PlayareaSize"){
                    // Debug.Log("ccc");
                    float hol = (float)item.Value.packets[lastPacketIndex].Data[0];
                    float ver = (float)item.Value.packets[lastPacketIndex].Data[1];

                    VelocityController.playArea = new Vector2(hol, ver);
                    VelocityController.SetParameter();
				}
			}
		}
    }
}