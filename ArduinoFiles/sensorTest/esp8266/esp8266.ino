#include <SoftwareSerial.h>

const byte rxPin = 8;
const byte txPin = 9;

SoftwareSerial mySerial(rxPin, txPin); // RX, TX
void setup() {
  Serial.begin(9600);
  mySerial.begin(9600);
}

void loop() {
  if (mySerial.available()) {
    Serial.write(mySerial.read());
  }
  if (Serial.available()) {
    mySerial.write(Serial.read());
  }
}
