/*
 * Control a servo motor, provide teleoperation via GUI from Visual Studio
 * Laura Boccanfuso, Lisa Chen, Colette Torres, 2015
 */

#include <Servo.h> 

// create servo objects to control each servo

Servo myservo4;   // right eyebrow
Servo myservo5;   // left eyebrow
Servo myservo6;   // right eyeball left/right
Servo myservo7;   // left eyeball left/right
Servo myservo8;   // jaw
Servo myservo9;   // right lip
Servo myservo10;  // left lip
Servo myservo11;  // head left/right
Servo myservo12;  // head up/down
Servo myservoA0;  // left eyelid
Servo myservoA1;  // right eyelid


void setup() 
{
  
  Serial.begin(9600); //begins serial communication
  delay(500);
} 

//////////////////////  ATTACH/DETACH EVERYTHING HERE  ////////////////////////

void attacheyebrows()
{   myservo5.attach(5); 
    myservo4.attach(4);  }

void detacheyebrows()
{   myservo5.detach();  
    myservo4.detach();    }

void attachlips()
{   myservo10.attach(10); myservo9.attach(9);  }

void detachlips()
{   myservo10.detach(); myservo9.detach();     }

void attacheyelids()
{   myservoA0.attach(A0); myservoA1.attach(A1); }

void detacheyelids()
{   myservoA0.detach(); myservoA1.detach();    }

void attachmouth()
{   myservo8.attach(8);  }

void detachmouth()
{   myservo8.detach();   }

void attachpanhead()
{   myservo11.attach(11);  }

void detachpanhead()
{   myservo11.detach();  }

void attachtilthead()
{   myservo12.attach(12);  }

void detachtilthead()
{   myservo12.detach();  }


void attacheyeballs()
{   myservo4.attach(4); myservo5.attach(5); myservo6.attach(6); myservo7.attach(7);  }

void detacheyeballs()
{   myservo4.detach(); myservo5.detach(); myservo6.detach(); myservo7.detach();  }

///////////////////////////////////////////////////////////////////////////////
////////////////////////  LOW-LEVEL COMMANDS HERE  ////////////////////////////

void eyesblink() 
{
  attacheyelids();
  myservoA0.write(0);
  myservoA1.write(10);
  delay(150);
  myservoA0.write(125); //open left eyelid
  myservoA1.write(107); // open right eyelid
  delay(250);
  detacheyelids();
}

void eyesclosed()
{
  attacheyelids();
  myservoA0.write(0);
  myservoA1.write(10);
  delay(200);
  detacheyelids();
}

void eyesopen()
{
  attacheyelids();
  myservoA0.write(125);
  myservoA1.write(107);
  delay(200);
  detacheyelids();
}

void wink()
{
  attacheyelids();
  myservoA0.write(80);
  delay(150);
  detacheyelids();
  attacheyelids();
  myservoA0.write(125);
  delay(150);
  detacheyelids();
}

void eyeshalfshut()
{
  attacheyelids();
  myservoA0.write(100);
  myservoA1.write(70);
  delay(150);
  detacheyelids();
}

void eyesopenwide()
{
  attacheyelids();
  myservoA0.write(140); //open right eyelid
  myservoA1.write(110); // open left eyelid 
  delay(150);
  detacheyelids();
}

void eyesright()
{
  attacheyeballs();
  myservo6.write(88);  // 68/25 straight
  myservo7.write(45);
  delay(150);
  detacheyeballs();
}

void eyesleft()
{
  attacheyeballs();
  myservo6.write(48);
  myservo7.write(5);
  delay(150);
  detacheyeballs();
}

void eyessilly()
{
  attacheyeballs();
  myservo6.write(52); //right eye ball look right
  myservo7.write(50); //left eye ball look left
  delay(150);
  myservo4.write(80);//right eye ball look up 
  myservo5.write(30); //left eye ball look down
  delay(150);
  detacheyeballs();
}

void eyessquirrely()
{
  attacheyeballs();

}
  
void eyescrossed()
{
  attacheyeballs();
  myservo7.write(95);  //  
  myservo6.write(-30);  // 
  delay(150);
  myservo4.write(170); //
  myservo5.write(25);  // 
  delay(150);
  detacheyeballs();
}

void eyesstraightLR()
{
  attacheyeballs();
  myservo7.write(68); 
  myservo6.write(25);
  delay(150);
  detacheyeballs();
}

void eyesup()
{
  attacheyeballs();
  myservo4.write(80);
  myservo5.write(90);  
  delay(150);
  detacheyeballs();
}

void eyesdown()
{
  attacheyeballs();
  myservo4.write(160);
  myservo5.write(30);
  delay(150);
  detacheyeballs();
}

void eyesstraightUD()
{
  attacheyeballs();
  myservo4.write(130); 
  myservo5.write(60);
  delay(150);
  detacheyeballs();
}

void eyebrowssad()
{
  attacheyebrows();
  myservo5.write(110);
  myservo4.write(105);
  delay(200);
  detacheyebrows();
}

void eyebrowsneutral()
{
  attacheyebrows();
  myservo5.write(90);
  myservo4.write(120);
  delay(300);
  detacheyebrows();
}

void lipsup()
{
  attachlips();
  myservo10.write(20); 
  myservo9.write(310); 
  delay(350);
  detachlips();
}

void lipsneutral()
{
  attachlips();
  myservo10.write(50);  // 
  myservo9.write(150);  // 
  delay(150);
  detachlips();
}

void lipsdown()
{
  attachlips();
  myservo10.write(120);  // left lip down
  myservo9.write(70);  // right lip down
  delay(300);
  detachlips();
}

void lipsslant1()
{
  attachlips();
  myservo10.write(120);//left lip down
  myservo9.write(200);//right lip slightly up 
  delay(200);
  detachlips();
}
void lipsslant2()
{
  attachlips();
  myservo10.write(30);//left lip up
  myservo9.write(110);//right lip slightly down
  delay(200);
  detachlips();
}

void smallheaddown()
{
  attachtilthead();
  myservo12.write(55);
  delay(200);
  detachtilthead();
}

void smallheadleft()
{
  attachpanhead();
  myservo11.write(70);
  delay(140);
  detachpanhead();
}
void smallheadright()
{
  attachpanhead();
  myservo11.write(95);
  delay(140);
  detachpanhead();
}
void headright()
{
  attachpanhead();
  myservo11.write(95);
  delay(250);
  detachpanhead();
}

void headleft()
{
  attachpanhead();
  myservo11.write(50);
  delay(250);
  detachpanhead();
}

void headstraightLR()
{
  attachpanhead();
  myservo11.write(70);
  delay(350);
  detachpanhead();
}

void headup()
{
  attachtilthead();
  myservo12.write(10);
  delay(200);
  detachtilthead();
}

void headdown()
{
  attachtilthead();
  myservo12.write(40);
  delay(300);
  detachtilthead();  
}

void headstraightUD()
{
  attachtilthead();
  myservo12.write(10);
  delay(300);
  detachtilthead();
}

void mouthclosed()
{
  attachmouth();
  myservo8.write(205);
  delay(100);
  detachmouth();
}

void mouthopenbig()
{
  attachmouth();
  myservo8.write(140);   // jaw opens big
  delay(50);
  detachmouth();
}

void mouthopenhuge()
{
  attachmouth();
  myservo8.write(120);   // jaw opens big
  delay(200);
  detachmouth();
}

void mouthopenmed()
{
  attachmouth();
  myservo8.write(145);
  delay(50);
  detachmouth();
}

void mouthopensmall()
{
  attachmouth();
  myservo8.write(150);
  delay(50);
  detachmouth();
}

void stopall()
{
  detachlips();
  detacheyeballs();
  detachpanhead();
  detachtilthead();
  detacheyelids();
  detachmouth();
}
/////////////////////////////////////////////////////////////////////////////////

void loop() 
{ 
  char pos='2';

  if (Serial.available()){
    delay(50);
    while(Serial.available()>0){
      pos=Serial.read();   
      
      // SMILE
      if(pos=='0') {
        eyebrowsneutral();
        lipsup();
      }
      else if(pos=='1') {
        // SAD
        lipsdown();
        eyebrowssad();
      }
      else if(pos=='2') {
        // NEUTRAL
        headstraightUD();
        headstraightLR();
        mouthclosed();
        lipsneutral();
        eyesopen();
        eyesstraightLR();
        eyesstraightUD();
        eyebrowsneutral();
        delay(300);
      }
      else if(pos=='3') {
        // BLINK
        eyesblink();
      }
      else if(pos=='4') { 
        //JAW DROP BIG
        mouthopenbig();
      }
      else if(pos=='5') { 
        mouthopenmed();
      }
      else if(pos=='6') { 
        //JAW DROP SMALL
        mouthopensmall();
      }
      else if(pos=='7') {
        //JAW SHUT
        mouthclosed();
      }
       else if(pos=='8') {
        //SHAKE HEAD NO
        smallheadleft();
        smallheadright();
        smallheadleft();
        smallheadright();
        smallheadleft();
        smallheadright();
        headstraightLR();
      }    
      else if(pos=='9') {
        // JAW DROP
        mouthopenhuge();
        delay(900);
        mouthclosed();
      }
       else if(pos=='10') {
        // STOP EVERYTHING
        delay(400);
        wink();
        delay(400);
      }
       
     else if(pos=='A') {
        headleft();
      }
      else if(pos=='B') {
        headright();
      }       
     else if(pos=='C') {
        // NOD
        headstraightLR();
        smallheaddown();
        delay(500);
        headup();
     }
     else if(pos=='D') {
        eyesclosed();
     }
     
     else if(pos=='E') {
        //WINK
        lipsup();
        wink();     
      }
     else if(pos=='F') {
        // HEAD CENTER
        headstraightLR();
     }
     else if(pos=='G') {
        eyesopen();
     }
    }   
  } 
} 

