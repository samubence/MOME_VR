using UnityEngine;
using System.Collections;

public class OSCBridge : MonoBehaviour {

	public string remoteIP = "127.0.0.1";
	public int sendToPort = 54321;
	public int listenerPort = 12345;
	
	Osc oscHandler;
	UDPPacketIO udp;

    Vector3 head, origPos;
    public float scale = 10;

	// Use this for initialization
	void Start ()
    {
        head = new Vector3(0, 0, 0);
        origPos = transform.position;
		udp = GetComponent<UDPPacketIO>();
		udp.init(remoteIP, sendToPort, listenerPort);
		
		oscHandler = GetComponent<Osc>();
		oscHandler.init(udp);
		
		oscHandler.SetAddressHandler("/head", oscEventListener);
		
		Debug.Log("OSC listening at port: " + listenerPort + " ");
	}

    void Update()
    {
        transform.position = origPos + head;
    }
	
	public void oscEventListener(OscMessage oscMessage)
	{
        if (oscMessage.Values.Count == 3)
        {
            head = new Vector3((float)oscMessage.Values[0], (float)oscMessage.Values[1], -(float)oscMessage.Values[2]);
			head *= scale; 
        }		
	}
	
	public void sendMessage(string addr, float val)
	{
		ArrayList values = new ArrayList();
		values.Add(val);
		OscMessage msg = new OscMessage();
		msg.Address = addr;
		msg.Values = values;
		oscHandler.Send(msg);
	}	
}
