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
    public Text scoreMessage;
    CountdownTimer countdown;

    //게임 상황
    public GameState state;
    public static int score = 0;
    int failCount = 0;

    //게임 객체
    GameObject player;
    GameObject startPoint;

    // Start is called before the first frame update
    void Start()
    {

        //Game Start
        state = GameState.START;
        buttons = GameObject.FindGameObjectsWithTag("Button");
        countdown = GameObject.FindGameObjectWithTag("Countdown").GetComponent<CountdownTimer>();
        player = GameObject.FindGameObjectWithTag("Player");
        startPoint = GameObject.FindGameObjectWithTag("Start");

        //시리얼 통신
        try
        {
            serial = new SerialPort(portNumber.ToString(), baudrate, Parity.None, 8, StopBits.One);
            //serial.Open();
            serial.ReadTimeout = 5;
        }
        catch (MissingComponentException e)
        {
            Debug.Log(e);
            throw;
        }

        StartCoroutine(SetUpGame());

    }

    //메세지 지우고 카운트다운 시작.
    IEnumerator SetUpGame()
    {
        yield return new WaitForSeconds((float) 1.5);
        Text.Destroy(tutorialMessage);
        countdown.Active = true;
        failCount = 0;

        state = GameState.ORDERING;
    }

    //시간이 끝났을 때 CountDownTimer가 호출
    public void TimeOver()
    {
        state = GameState.MOVEMENT;
        //버튼 별로 삭제
        for (int i = 0; i < 4; i++)
        {
            Debug.Log(buttons[i].name);
            buttons[i].SetActive(false);
        }

        //플레이어 움직임
        StartCoroutine(player.GetComponent<Playermove>().Movement());

    }

    //성공하지 못했을 때, 스테이지 재시작
    public void stageRestart()
    {
        state = GameState.FAIL;
        failCount += 1;

        //버튼 불러오기
        for (int i = 0; i < 4; i++)
        {
            buttons[i].SetActive(true);
        }

        //order 리셋
        player.GetComponent<Playermove>().ResetButtonClicked();
        orderMessage.text = "";

        //캐릭터 움직이기
        player.transform.position = new Vector2((float)8.12, (float)-4.11);


        //시간 되돌리기
        countdown.Retry();
        countdown.Active = true;
    }

    //스테이지 클리어
    public void stageClear()
    {
        score += 1000 - (failCount * 100);
        player.GetComponent<Playermove>().ResetButtonClicked();
        SceneManager.LoadScene("geSuccess");
    }

    // Update is called once per frame
    void Update()
    {
        scoreMessage.text = "Score : " + score;
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
    