using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO.Ports;

public enum GameState { START, ORDERING, MOVEMENT, SUCCESS, FAIL}
public class GameSystem : MonoBehaviour
{
    //포트 enum
    public enum PortNumber
    {
        COM1, COM2, COM3, COM4, COM5, COM6, COM7, COM8,
        COM9, COM10, COM11, COM12, COM13, COM14, COM15, COM16,
    }

    private SerialPort serial;

    //내 포트 기준이라 다를 수 있음.
    [SerializeField]
    private PortNumber portNumber = PortNumber.COM6;
    [SerializeField]
    private int baudrate = 9600;

    GameObject[] buttons;
    DirectionButton temp;
    public Text orderMessage;
    CountdownTimer countdown;

    public GameState state;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {

        //Game Start
        state = GameState.START;
        buttons = GameObject.FindGameObjectsWithTag("Button");
        countdown = GameObject.FindGameObjectWithTag("Countdown").GetComponent<CountdownTimer>();
        player = GameObject.FindGameObjectWithTag("Player");

        //시리얼 통신
        serial = new SerialPort(portNumber.ToString(), baudrate, Parity.None, 8, StopBits.One);
        serial.Open();
        serial.ReadTimeout = 5;


        StartCoroutine(SetUpGame());
        
    }

    IEnumerator SetUpGame()
    {
        Debug.Log("Entered");
        yield return new WaitForSeconds(1);
        Text.Destroy(orderMessage);
        countdown.Active = true;
    }

    public void TimeOver()
    {

        for (int i = 0; i < 3; i++)
        {
            Debug.Log(buttons[i].name);
            buttons[i].GetComponent<DirectionButton>().TimeOver();

        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<Playermove>().Movement();
    }
    // Update is called once per frame
    void Update()
    {
        //시리얼 값 받아서 로그 찍기
        if (serial.IsOpen)
        {
            player.GetComponent<Playermove>().movewithArduino(serial.ReadByte());
            Debug.Log(serial.ReadByte());
            /*try
            {
                Debug.Log(serial.ReadByte());
            }
            catch (System.TimeoutException e)
            {
                Debug.Log(e);
                throw;
            }
            Debug.Log("Connected");*/
        }
            
    }
}
