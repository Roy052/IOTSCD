/*
  AK975X Human Presence and Movement Sensor Example Code
  By: Nathan Seidle
  SparkFun Electronics
  Date: March 10th, 2017
  License: This code is public domain but you buy me a beer if you use this and we meet someday (Beerware license).

  Outputs the four IR readings and internal temperature.

  IR readings increase as warm bodies enter into the observable areas.

  Hardware Connections:
  Attach a Qwiic shield to your RedBoard or Uno.
  Plug the Qwiic sensor into any port.
  Serial.print it out at 9600 baud to serial monitor.
*/

#include <Wire.h>

#include "SparkFun_AK975X_Arduino_Library.h" //Use Library Manager or download here: https://github.com/sparkfun/SparkFun_AK975X_Arduino_Library

AK975X movementSensor; //Hook object to the library

int ir1, ir2, ir3, ir4, temperature;
int beforeir1, beforeir2, beforeir3, beforeir4;
int correction = 100, delaytime = 3;

void setup()
{
  Serial.begin(9600);
  Serial.println("AK975X Read Example");

  Wire.begin();

  //Turn on sensor
  if (movementSensor.begin() == false)
  {
    Serial.println("Device not found. Check wiring.");
    while (1);
  }

  if (movementSensor.available())
  {
    beforeir1 = movementSensor.getIR1();
    beforeir2 = movementSensor.getIR2();
    beforeir3 = movementSensor.getIR3();
    beforeir4 = movementSensor.getIR4();
    movementSensor.refresh();
  }
}

void loop()
{
  if (movementSensor.available())
  {
    ir1 = movementSensor.getIR1();
    ir2 = movementSensor.getIR2();
    ir3 = movementSensor.getIR3();
    ir4 = movementSensor.getIR4();
    float tempF = movementSensor.getTemperatureF();

    movementSensor.refresh(); //Read dummy register after new data is read

    //Note: The observable area is shown in the silkscreen.
    //If sensor 2 increases first, the human is on the left
    if(beforeir1 > ir1 + correction ){
      Serial.write(0);
    }
    else if(beforeir2 > ir2 + correction ){
      Serial.write(1);
    }
    else if(beforeir3 > ir3 + correction ){
      Serial.write(2);
    }
    else if(beforeir4 > ir4 + correction ){
      Serial.write(3);
    }
    
    beforeir1 = ir1;
    beforeir2 = ir2;
    beforeir3 = ir3;
    beforeir4 = ir4;
  }
  delay(delaytime);
}
