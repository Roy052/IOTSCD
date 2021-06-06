using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

public class Receiver_mole : MonoBehaviour
{
    public int sensitivity = 70000;
    public float minusY = 0.0027f;
    public float minusX = 0.0006f;
    private UdpClient m_Receive;
    public int arduinoPort = 50001;
    public int m_Port = 50002;
    public string arduinoIP = "192.168.0.11";
    public string m_ReceivePacket;
    private byte[] m_ReceiveBytes;
    IPEndPoint arduinoEndPoint;
    byte[] StrByte;
    char[] seperateChar = {'X', 'Y', 'Z'};
    public string[] inputData;

    void Awake()
    {
        arduinoEndPoint = new IPEndPoint(IPAddress.Parse(arduinoIP), arduinoPort);
        StrByte = Encoding.UTF8.GetBytes("hi");
        InitReceiver();
    }

    void OnApplicationQuit()
    {
        CloseReceiver();
    }

    void InitReceiver()
    {
        try
        {
            if (m_Receive == null)
            {
                m_Receive = new UdpClient(m_Port);
                int dat = m_Receive.Send(StrByte,StrByte.Length,arduinoEndPoint);
                Debug.Log("Send : "+dat.ToString()+"Bytes. "+arduinoEndPoint.ToString());
                m_Receive.BeginReceive(new AsyncCallback(DoBeginReceive), null);
            }
        }
        catch (SocketException e)
        {
            Debug.Log(e.Message);
        }
    }

    void DoBeginReceive(IAsyncResult ar)
    {
        IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, m_Port);


        if (m_Receive != null)
        {
            m_ReceiveBytes = m_Receive.EndReceive(ar, ref ipEndPoint);
        }

        else
        {
            return;
        }

        m_Receive.BeginReceive(new AsyncCallback(DoBeginReceive), null);
        DoReceive();
    }

    void DoReceive()
    {
        m_ReceivePacket = Encoding.ASCII.GetString(m_ReceiveBytes);

        // Debug.Log("Receive: "+m_ReceivePacket.ToString());
    }

    void CloseReceiver()
    {
        if (m_Receive != null)
        {
            m_Receive.Close();
            m_Receive = null;
        }
    }

    private void Update() {
        float x, y, z;
        inputData = m_ReceivePacket.Split(seperateChar);
        if(inputData.Length > 1)
        {
            x = float.Parse(inputData[1]) / sensitivity;
            y = float.Parse(inputData[2]) / sensitivity;
            z = float.Parse(inputData[3]) / sensitivity;

            //transform.position = transform.position + new Vector3(z, -x, 0) - new Vector3(minusX, minusY,0);
            MouseCursorObjScript.instance.setPosition(new Vector3(z, -x, 0) - new Vector3(minusX, minusY, 0));
        }
        
        Vector3 posBorder = Camera.main.WorldToViewportPoint(transform.position);
        if (posBorder.x < 0f) posBorder.x = 0f;
        if (posBorder.x > 1f) posBorder.x = 1f;
        if (posBorder.y < 0f) posBorder.y = 0f;
        if (posBorder.y > 1f) posBorder.y = 1f;
        transform.position = Camera.main.ViewportToWorldPoint(posBorder);
    }
}