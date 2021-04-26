using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO.Ports;

//게임의 상태 enum
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

    //UI용 객체
    GameObject[] buttons;
    DirectionButton temp;
    public Text tutorialMessage;
    public Text orderMessage;
    CountdownTimer countdown;

    //게임 상황
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
        try
        {
            serial = new SerialPort(portNumber.ToString(), baudrate, Parity.None, 8, StopBits.One);        
            //serial.Open();
            serial.ReadTimeout = 5;
        }
        catch(MissingComponentException e)
        {
            Debug.Log(e);
            throw;
        }

        StartCoroutine(SetUpGame());
        
    }

    //메세지 지우고 카운트다운 시작.
    IEnumerator SetUpGame()
    {
        Debug.Log("Entered");
        yield return new WaitForSeconds(1);
        Text.Destroy(tutorialMessage);
        countdown.Active = true;

        state = GameState.ORDERING;
    }

    //시간이 끝났을 때 CountDownTimer가 호출
    public void TimeOver()
    {
        //버튼 별로 삭제
        for (int i = 0; i < 4; i++)
        {
            Debug.Log(buttons[i].name);
            buttons[i].GetComponent<DirectionButton>().TimeOver();

        }

        //플레이어 움직임
        StartCoroutine(GameObject.FindGameObjectWithTag("Player").GetComponent<Playermove>().Movement());
        state = GameState.MOVEMENT;
    }
    
    //성공하지 못했을 때, 스테이지 재시작
    public void stageRestart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    //스테이지 클리어
    public void stageClear()
    {
        SceneManager.LoadScene("geSuccess");
    }

    // Update is called once per frame
    void Update()
    {
        //시리얼 값 받아서 로그 찍기
        if (serial.IsOpen)
        {
            player.GetComponent<Playermove>().movewithArduino(serial.ReadByte());
            //Debug.Log(serial.ReadByte());
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
