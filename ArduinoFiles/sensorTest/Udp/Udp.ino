/*
  UDPSendReceive.pde:
  This sketch receives UDP message strings, prints them to the serial port
  and sends an "acknowledge" string back to the sender

  A Processing sketch is included at the end of file that can be used to send
  and received messages for testing with a computer.

  created 21 Aug 2010
  by Michael Margolis

  This code is in the public domain.

  adapted from Ethernet library examples
*/

#include <Wire.h>
#include <ESP8266WiFi.h>
#include <WiFiUdp.h>

#ifndef STASSID
#define STASSID "gatorade"
#define STAPSK  "12345677"
#endif

unsigned int localPort = 8888;      // local port to listen on
const int MPU=0x68;
int16_t AcX,AcY,AcZ,Tmp,GyX,GyY,GyZ;
// buffers for receiving and sending data
char packetBuffer[UDP_TX_PACKET_MAX_SIZE]; //buffer to hold incoming packet,
char  ReplyBuffer[] = "acknowledged\r\n";       // a string to send back
IPAddress remote;
int port;
bool started = false;
WiFiUDP Udp;

void setup() {
  Serial.begin(9600);
  
  Wire.begin();
  Wire.beginTransmission(MPU);
  Wire.write(0x6B);  // PWR_MGMT_1 register
  Wire.write(0);     // set to zero (wakes up the MPU-6050)
  Wire.endTransmission(true);
  
  WiFi.mode(WIFI_STA);
  WiFi.begin(STASSID, STAPSK);
  while (WiFi.status() != WL_CONNECTED) {
    Serial.print('.');
    delay(500);
  }
  Serial.print("Connected! IP address: ");
  Serial.println(WiFi.localIP());
  Serial.printf("UDP server on port %d\n", localPort);
  Udp.begin(localPort);
}

void loop() {
  // if there's data available, read a packet
  if(started == false){
    int packetSize = Udp.parsePacket();
    if (packetSize) {
      started = true;
      Serial.print("Received packet of size ");
      Serial.println(packetSize);
      Serial.print("From ");
      remote = Udp.remoteIP();
      port = Udp.remotePort();
      for (int i = 0; i < 4; i++) {
        Serial.print(remote[i], DEC);
        if (i < 3) {
          Serial.print(".");
        }
      }
      Serial.print(", port ");
      Serial.println(Udp.remotePort());
  
      // read the packet into packetBufffer
      Udp.read(packetBuffer, UDP_TX_PACKET_MAX_SIZE);
      Serial.println("Contents:");
      Serial.println(packetBuffer);
  
      // send a reply, to the IP address and port that sent us the packet we received
  //    Udp.beginPacket(Udp.remoteIP(), Udp.remotePort());
      Udp.beginPacket(remote, port);
      String txString = "";
      txString += 'X';
      txString += GyX;
      txString += 'Y';
      txString += GyY;
      txString += 'Z';
      txString += GyZ;
      txString += "\r\n";
      Udp.print(txString);
      Udp.endPacket();
    }
    get6050();//센서값 갱신
  //  Serial.print(GyX);
  //  Serial.print("");
  //  Serial.print(GyY);
  //  Serial.print("");
  //  Serial.print(GyZ);
  //  Serial.println();
    delay(10);
  }
  else
  {
    get6050();
    Udp.beginPacket(remote, port);
    String txString = "";
    txString += 'X';
    txString += GyX;
    txString += 'Y';
    txString += GyY;
    txString += 'Z';
    txString += GyZ;
    txString += "\r\n";
    Udp.print(txString);
    Udp.endPacket();
    delay(100);
  }
}

void get6050(){
  Wire.beginTransmission(MPU);//MPU6050 호출
  Wire.write(0x3B);//AcX 레지스터 위치 요청
  Wire.endTransmission(false);
  Wire.requestFrom(MPU,14,true);//14byte의 데이터를 요청
  AcX=Wire.read()<<8|Wire.read();//두개의 나뉘어진 바이트를 하나로 이어붙입니다.
  AcY=Wire.read()<<8|Wire.read();
  AcZ=Wire.read()<<8|Wire.read();
  Tmp=Wire.read()<<8|Wire.read();
  GyX=Wire.read()<<8|Wire.read();
  GyY=Wire.read()<<8|Wire.read();
  GyZ=Wire.read()<<8|Wire.read();
}
/*
  test (shell/netcat):
  --------------------
	  nc -u 192.168.esp.address 8888
*/
