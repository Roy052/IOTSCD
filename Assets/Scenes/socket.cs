using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class socket : MonoBehaviour
{
    // Start is called before the first frame update
    UdpClient srv;
    IPEndPoint remoteEP;        
    void Start()
    {
        srv = new UdpClient(8888);
        IPAddress ip1 = IPAddress.Parse("192.168.0.11");
        remoteEP= new IPEndPoint(ip1, 0);

        byte[] StrByte = Encoding.UTF8.GetBytes("hi");
        srv.Send(StrByte,StrByte.Length,remoteEP);
    }

    // Update is called once per frame
    void Update()
    {
        byte[] dgram = srv.Receive(ref remoteEP);
        Debug.Log("[Receive] "+remoteEP.ToString()+" 로부터 "+dgram.Length+" 바이트 수신");

        srv.Send(dgram, dgram.Length, remoteEP);
        Debug.Log("[Send] "+remoteEP.ToString()+" 로부터 "+dgram.Length+" 바이트 수신");
    }
}
