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
    IPEndPoint RemoteIpEndPoint;  // byte[] StrByte;   
    public string arduinoIP = "192.168.0.11";
    public int port = 8888;
    private IEnumerator m_Coroutine;
    void Start()
    {
        RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

        Debug.Log("start");
        srv = new UdpClient(arduinoIP,port);
        // IPEndPoint remoteEP= new IPEndPoint(ip1, 0);

        byte[] StrByte = Encoding.UTF8.GetBytes("hi");
        int dat = srv.Send(StrByte,StrByte.Length);
        Debug.Log("send data : "+dat.ToString()+"Bytes");

        byte[] receiveBytes = srv.Receive(ref RemoteIpEndPoint);
        string returnData = Encoding.ASCII.GetString(receiveBytes);
        Debug.Log(returnData);
        m_Coroutine = receiveUDP();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(m_Coroutine);
    }

    IEnumerator receiveUDP(){
        try{
            Debug.Log("coroutine in");
            byte[] receiveBytes = srv.Receive(ref RemoteIpEndPoint);
            string returnData = Encoding.ASCII.GetString(receiveBytes);
            Debug.Log(returnData);
        }
        catch{
            Debug.Log("failed");
        }
        yield return null;
    }
}
