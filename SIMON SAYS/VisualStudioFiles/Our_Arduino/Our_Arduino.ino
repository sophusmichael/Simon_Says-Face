/*
 * Control a servo motor, provide teleoperation via GUI from Visual Studio
 * Laura Boccanfuso, Lisa Chen, Colette Torres, 2015
 * Test
 */

#include <Servo.h> 

// create servo objects to control each servo

Servo myservo4;   // right eyeball up/down
Servo myservo5;   // left eyeball up/down
Servo myservo6;   // right eyeball left/right
Servo myservo7;   // left eyeball left/right
Servo myservo8;   // jaw
Servo myservo9;   // right lip
Servo myservo10;  // left lip
Servo myservo11;  // head left/right
Servo myservo12;  // head up/down
Servo myservoA0;  // left eyelid
Servo myservoA1;  // right eyelid
Servo myservoA3;  //left eyebrow
Servo myservo3;   // right eyebrow



void setup() 
{
  
  Serial.begin(9600); //begins serial communication
  delay(500);
} 

//////////////////////  ATTACH/DETACH EVERYTHING HERE  ////////////////////////

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

void attachhead()
{   myservo11.attach(11); myservo12.attach(12);  }

void detachhead()
{   myservo11.detach();  myservo12.detach();  }

void attacheyeballs()
{   myservo4.attach(4); myservo5.attach(5); myservo6.attach(6); myservo7.attach(7);  }

void detacheyeballs()
{   myservo4.detach(); myservo5.detach(); myservo6.detach(); myservo7.detach();  }

///////////////////////////////////////////////////////////////////////////////
////////////////////////  LOW-LEVEL COMMANDS HERE  ////////////////////////////

void eyesblink() 
{
  attacheyelids();
  myservoA0.write(20);
  myservoA1.write(0);
  delay(150);
  myservoA0.write(115); //open left eyelid
  myservoA1.write(95); // open right eyelid
  delay(150);
  detacheyelids();
}

void wink()
{
  attacheyelids();
  myservoA0.write(75);
  delay(150);
  detacheyelids();
  attacheyelids();
  myservoA0.write(115);
  delay(150);
  detacheyelids();
}

void eyeshalfshut()
{
  attacheyelids();
  myservoA0.write(85);
  myservoA1.write(75);
  delay(150);
  detacheyelids();
}

void eyesopen()
{
  attacheyelids();
  myservoA0.write(115); //open left eyelid
  myservoA1.write(95); // open right eyelid 
  delay(150);
  detacheyelids();
}

void eyesopenwide()
{
  attacheyelids();
  myservoA0.write(135); //open left eyelid
  myservoA1.write(120); // open right eyelid 
  delay(150);
  detacheyelids();
}

void eyesright()
{
  attacheyeballs();
  myservo6.write(52);
  myservo7.write(95);
  delay(150);
  detacheyeballs();
}

void eyesleft()
{
  attacheyeballs();
  myservo6.write(0);
  myservo7.write(50);
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

void eyessquirrely()
{
  
}

void lipsup()
{
  attachlips();
  myservo10.write(20); 
  myservo9.write(310); 
  delay(250);
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
  delay(250);
  detachlips();
}

void lipsslant()
{
  attachlips();
  myservo10.write(120);//left lip down
  myservo9.write(200);//right lip slightly up 
  delay(200);
  detachlips();
}

void headright()
{
  attachhead();
  myservo11.write(110);
  delay(300);
  detachhead();
}

void headleft()
{
  attachhead();
  myservo11.write(50);
  delay(300);
  detachhead();
}

void headstraightLR()
{
  attachhead();
  myservo11.write(75);
  delay(300);
  detachhead();
}

void headup()
{
  attachhead();
  myservo12.write(10);
  delay(300);
  detachhead();
}

void headdown()
{
  attachhead();
  myservo12.write(40);
  delay(300);
  detachhead();  
}

void headstraightUD()
{
  attachhead();
  myservo12.write(20);
  delay(300);
  detachhead();
}

void mouthclosed()
{
  attachmouth();
  myservo8.write(210);
  delay(50);
  detachmouth();
}

void mouthopenbig()
{
  attachmouth();
  myservo8.write(120);   // jaw opens big
  delay(50);
  detachmouth();
}

void mouthopenmed()
{
  attachmouth();
  myservo8.write(140);
  delay(50);
  detachmouth();
}

void mouthopensmall()
{
  attachmouth();
  myservo8.write(160);
  delay(50);
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
        lipsup();
      }
      else if(pos=='1') {
        // SAD
        lipsdown();
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
        //EYES RIGHT
        eyesright();
      }
      else if(pos=='9') {
        //EYES LEFT
        eyesleft();
      }
       else if(pos=='10') {
        // STOP EVERYTHING
        detachlips();
        detacheyeballs();
        detacheyelids();
        detachmouth();
        detachhead();
        delay(150);
      }
       
     else if(pos=='A') {
        headleft();
      }
      else if(pos=='B') {
        headright();
      }       
      else if(pos=='C') {
        //HEAD UP
        headup();
      }
      else if(pos=='D') {
        //HEAD DOWN
        headdown();
      }    
     else if(pos=='E') {
        //WINK
        lipsup();
        wink();
      }   
      else if(pos=='F') {
        // CONFUSED
        
        eyesup();
        eyesright();
        lipsslant();
        eyesopenwide();
        mouthopenmed();
        eyesstraightUD();
        
      }
      if(pos=='G') {
        // SURPRISED
        mouthopenbig();
        eyesopenwide();
        eyesstraightLR();
        eyesstraightUD();
      }
      else if(pos=='H') {
        // ANGRY
        lipsdown();
        eyeshalfshut();
        mouthopenmed();
        eyesstraightLR();
        eyesstraightUD();
      }
     
      else if(pos=='I') {
        // CROSS EYES
        eyescrossed();
        lipsup();
        mouthclosed();
      }
      else if(pos=='J') {
        // AWKWARD
       
        lipsslant();
        eyeshalfshut();
        eyesleft();
        mouthclosed();
   
        
      }
      else if(pos=='K') {
        // FUNNY FACE
        eyessilly();
        lipsup();
        mouthopenbig();
        
      }
     else if(pos=='L') {
        // AFRAID
        lipsdown();
        mouthopensmall();
        eyesopenwide();
        eyesdown();
       
      }
      else if(pos=='M') {
        //SLEEPY
        eyeshalfshut();
        lipsneutral();
      }
      else if(pos=='N') {
        // YELLING
        
        eyesopenwide();
        eyesstraightLR();
        eyesstraightUD();
        lipsdown();
        mouthopenbig();
       
      }
      else if(pos=='O') {
        // YAWN
        eyeshalfshut();
        eyesstraightLR();
        eyesstraightUD();
        lipsneutral();
        mouthopenmed(); 
      }
      else if(pos=='P') {
        // BURP
        myservo3.attach(A4);
        myservo6.attach(6); // 
        myservo7.attach(7);  // 
        myservo4.attach(4); // 
        myservo5.attach(5);  
        myservo10.attach(10); // 
        myservo9.attach(9);   //
        myservo12.attach(12); 
        //myservo12.attach(12); // 
        //myservo11.attach(11);  // 
        myservoA0.attach(A0); // 
        myservoA1.attach(A1);  // 
        myservo8.attach(8);
        myservoA3.attach(A3);
        
        myservo10.write(145);  // left lip down
        myservo9.write(18);  // right lip down
        myservo8.write(70);
        //myservo12.write(110); // left eyebrow sad
        //myservo11.write(80); // right eyebrow sad
        myservoA0.write(150);
        myservoA1.write(100);
        myservo6.write(25); // 
        myservo7.write(67);  // 
        myservo4.write(145);
        myservo5.write(60);
        myservo12.write(20);
        myservo3.write(225);
        delay(12000);
        myservo8.write(115);
        myservoA3.write(225);
        delay(400);
        myservo3.detach();
        myservo10.detach(); //
        myservo9.detach(); //
        myservo8.detach();
        //myservo12.detach(); // 
        //myservo11.detach();  // 
        myservoA0.detach(); // 
        myservoA1.detach();  // 
        myservo6.detach(); // left right
        myservo7.detach();  // left right
        myservo4.detach(); // up down
        myservo5.detach();  // up down
        myservo12.detach();
        myservoA3.detach();
      }
       else if(pos=='Q') {
        // LOOPY EYES        
        delay(200);
        myservo8.attach(8); //
        myservo10.attach(10); // 
        myservo9.attach(9); // 
        myservo12.attach(12); // 
        myservo11.attach(11);  //
        myservo6.attach(6); // 
        myservo7.attach(7);  // 
        myservo4.attach(4); // 
        myservo5.attach(5);  // 
        myservoA0.attach(A0); // 
        myservoA1.attach(A1); // 
        myservo3.attach(A4);
        myservoA3.attach(A3);
        
        myservo8.write(90);  // 
        myservo10.write(70);  // 
        myservo9.write(120);  // 

        delay(100);
        myservo7.write(68);  //
        myservo6.write(25);  
        delay(100);  
        myservo6.write(80);// 
        myservo7.write(30); 
        delay(100);  
        myservo4.write(130); //
        myservo5.write(60);
        delay(100);  
        myservo4.write(35); //
        myservo5.write(30);
        delay(100);  
        myservo6.write(-60);// 
        myservo7.write(95); 
        delay(100);  
        myservo4.write(130); //
        myservo5.write(60);
      
        delay(100);
        myservoA0.write(150); // 
        myservoA1.write(100); //
        myservo12.write(20); // 
        myservo11.write(75);  //
        myservo3.write(225);
        myservoA3.write(225);
        delay(250);
        
        myservo8.detach(); // 
        myservo10.detach(); // 
        myservo9.detach(); // 
        myservo11.detach();  // 
        myservo6.detach(); // 
        myservo7.detach();  // 
        myservo4.detach(); // 
        myservo5.detach();  // 
        myservoA0.detach(); // 
        myservoA1.detach(); // 
        myservo3.detach();
        myservoA3.detach();
        delay(200);
      }
     if(pos=='R') {
        // ANIMATED TALKING
      
      }
        else if(pos=='S') { 
        //Look what I can do with my eyes
        eyesleft();
        delay(100);
        eyesright();
        delay(100);
        eyesleft();
        delay(100);
        eyesright();
        delay(100);
        eyesleft();
        delay(100);
        eyesright();
        delay(300);
        eyesstraightLR();
        delay(300);
        
        eyesup();
        delay(100);
        eyesdown();  
        delay(100);
        eyesup();
        delay(100);
        eyesdown();  
        delay(100);
        eyesup();
        delay(100);
        eyesdown();  
        delay(300);
        eyesstraightUD();
        delay(300);
        
        eyescrossed();
        delay(100);
        eyesup();
        delay(100);
        eyesstraightLR();
        delay(100);
        eyesstraightUD();
        delay(500);
      }
        else if(pos=='T') {
        // BLINK, TURN, BACK TO NEUTRAL
    
        myservoA0.attach(A0);
        myservoA1.attach(A1);
        myservoA0.write(75);
         myservoA1.write(75);
         delay(150);
        myservoA0.write(150); //open eyelid
        myservoA1.write(100);
         delay(150); 
         myservoA0.detach();
         myservoA1.detach();
         delay(100);
        myservo11.attach(11);
        myservoA0.attach(A0); //  
        myservoA1.attach(A1);
        myservoA0.write(150); //  
        myservoA1.write(100);
        myservo11.write(110);   // head right
        delay(250);
        myservo11.detach();
        myservoA0.detach(); //  
        myservoA1.detach();
        delay(4000);         
      }
    
      else if(pos=='U') {
        // I CAN BLINK MY EYES        
        eyesblink();
      }
         
      else if(pos=='V') {
        //I CAN TURN MY HEAD!
        myservo11.attach(11);
        myservoA0.attach(A0); //  
        myservoA1.attach(A1);
        myservo11.write(50); 
        myservoA0.write(150); //  
        myservoA1.write(100);   // head left
        delay(350);
        myservo11.detach();
        myservoA0.detach(); //  
        myservoA1.detach();
        delay(300);
        myservo11.attach(11);
        myservo11.write(110);   // head right
        delay(350);
        myservo11.detach();
        delay(100);
      }  
      
  /*
   else if(pos=='W') {
        // I CAN CAN ALSO BE VERY SILLY! LOOK AT ALL THE FUNNY FACES I CAN MAKE!
        //1) funny face 2) loopy eyes 3) cross eyed
        myservo8.attach(8); //
        myservo10.attach(10); // 
        myservo9.attach(9); // 
        myservo12.attach(12); // 
        myservo11.attach(11);  //
        myservo6.attach(6); // 
        myservo7.attach(7);  // 
        myservo4.attach(4); // 
        myservo5.attach(5);  // 
        myservoA0.attach(A0); // 
        myservoA1.attach(A1); // 
        myservo3.attach(A4);
        myservoA3.attach(A3);
        
        myservo3.write(225);
        myservo8.write(80);  // 
        myservo10.write(-30);  // 
        myservo9.write(350);  // 
        //myservo.write(80);  // 
        //myservo.write(100); //
        delay(100);
        myservo7.write(-30);  //  
        myservo6.write(70);  // 
        delay(200);
        myservo4.write(40); //
        myservo5.write(25);  // 
        delay(100);
        myservoA0.write(75); // 
        myservoA1.write(75); //
        myservo12.write(20); // 
        myservo11.write(75);  //
        myservoA3.write(225);
        delay(150);
        
        myservo3.detach();
        myservo8.detach(); // 
        myservo10.detach(); // 
        myservo9.detach(); // 
        //myservo12.detach(); //
        myservo11.detach();  // 
        myservo6.detach(); // left right
        myservo7.detach();  // left right
        myservo4.detach(); // up down
        myservo5.detach();  // up down
        myservoA0.detach(); // 
        myservoA1.detach(); // 
        myservoA3.detach();
        //end funny face 
        delay(400);
        myservo8.attach(8); //
        myservo10.attach(10); // 
        myservo9.attach(9); // 
        myservo12.attach(12); // 
        myservo11.attach(11);  //
        myservo6.attach(6); // 
        myservo7.attach(7);  // 
        myservo4.attach(4); // 
        myservo5.attach(5);  // 
        myservoA0.attach(A0); // 
        myservoA1.attach(A1); // 
        myservo3.attach(A4);
        myservoA3.attach(A3);
        
        myservo8.write(90);  // 
        ;  // 
        myservo9.write(120);  // 

        delay(100);
        myservo7.write(68);  //
        myservo6.write(25);  
        delay(100);  
        myservo6.write(80);// 
        myservo7.write(0); 
        delay(100);  
        myservo4.write(130); //
        myservo5.write(60);
        delay(100);  
        myservo4.write(35); //
        myservo5.write(0);
        delay(100);  
        myservo6.write(-60);// 
        myservo7.write(110); 
        delay(100);  
        myservo4.write(130); //
        myservo5.write(60);
      
        delay(100);
        myservoA0.write(150); // 
        myservoA1.write(100); //
        myservo12.write(20); // 
        myservo11.write(75);  //
        myservo3.write(225);
        myservoA3.write(225);
        //end next face
        delay(400);
        
        myservo8.detach(); // 
        myservo10.detach(); // 
        myservo9.detach(); // 
        myservo11.detach();  // 
        myservo6.detach(); // 
        myservo7.detach();  // 
        myservo4.detach(); // 
        myservo5.detach();  // 
        myservoA0.detach(); // 
        myservoA1.detach(); // 
        myservo3.detach();
        myservoA3.detach();
        delay(200);
        
      myservo8.attach(8); //
        myservo10.attach(10); // 
        myservo9.attach(9); // 
        myservo12.attach(12); // 
        myservo11.attach(11);  //
        myservo6.attach(6); // 
        myservo7.attach(7);  // 
        myservo4.attach(4); // 
        myservo5.attach(5);  // 
        myservoA0.attach(A0); // 
        myservoA1.attach(A1); // 
        myservo3.attach(A4);
        myservoA3.attach(A3);
        
        myservo3.write(225);
        myservo8.write(90);  // 
        myservo10.write(-20);  // 
        myservo9.write(350);  // 
        //myservo.write(80);  // 
        //myservo.write(100); //
        delay(100);
        myservo7.write(105);  //  
        myservo6.write(-30);  // 
        delay(200);
        myservo4.write(170); //
        myservo5.write(25);  // 
        delay(100);
        myservoA0.write(170); // 
        myservoA1.write(100); //
        myservo12.write(20); // 
        myservo11.write(75);  //
        myservoA3.write(225);
        delay(250);
        
        myservo3.detach();
        myservo8.detach(); // 
        myservo10.detach(); // 
        myservo9.detach(); // 
        //myservo12.detach(); //
        myservo11.detach();  // 
        myservo6.detach(); // left right
        myservo7.detach();  // left right
        myservo4.detach(); // up down
        myservo5.detach();  // up down
        myservoA0.detach(); // 
        myservoA1.detach(); // 
        myservoA3.detach();
    }
    /*
    if(pos=='X') {
        // SOMETIMES I'M
        //1) happy 2) sad 3) confused 4) mad 
        myservo8.attach(8);   // jaw (20-110, open-to-c)
        myservo10.attach(10); // 
        myservo9.attach(9);   //
        myservo12.attach(12); // 
        myservo11.attach(11); //  
        myservoA0.attach(A0); //  
        myservoA1.attach(A1); //  
        myservo6.attach(6); // 
        myservo7.attach(7);  // 
        myservo4.attach(4); // 
        myservo5.attach(5);  
        myservo3.attach(A4);
        myservoA3.attach(A3);
       
        myservo8.write(100);   // 
        myservo10.write(20);  // 
        myservo9.write(350);   // 
        myservo12.write(10);   //
        myservo11.write(75);    // 
        myservoA0.write(150);   //
        myservoA1.write(100);  
        myservo6.write(25); // 
        myservo7.write(68);  // 
        myservo4.write(130);
        myservo5.write(60);
        myservo3.write(225);
        myservoA3.write(225);
      //end happy
        delay(300);
        
        myservo8.detach();   //  
        myservo10.detach();  // 
        myservo9.detach();   // 
        myservo12.detach();  //
        myservo11.detach();  // 
        myservoA0.detach();  //  
        myservoA1.detach();  //  
        myservo6.detach(); // left right
        myservo7.detach();  // left right
        myservo4.detach(); // up down
        myservo5.detach();
        myservo3.detach();
        myservoA3.detach();
        delay(100);
        
        myservo10.attach(10); // 
        myservo9.attach(9);   // 
        //myservo12.attach(12); // 
        //myservo11.attach(11);  // 
        myservoA0.attach(A0); // 
        myservoA1.attach(A1);  // 
        myservo6.attach(6); // 
        myservo7.attach(7);  // 
        myservo4.attach(4); // 
        myservo5.attach(5);  
        myservo3.attach(A4);
        myservoA3.attach(A3);
        
        myservo3.write(225);
        myservo6.write(25); // 
        myservo7.write(68);  // 
        myservo4.write(130);
        myservo5.write(60);
        myservo10.write(140);  // left lip down
        myservo9.write(70);  // right lip down
        myservoA0.write(150);
        myservoA1.write(100);
        myservoA3.write(225);
        delay(400);
        
        myservo3.detach();
        myservo10.detach(); //
        myservo9.detach(); //
        myservoA0.detach(); // 
        myservoA1.detach();  // 
        myservo6.detach(); // left right
        myservo7.detach();  // left right
        myservo4.detach(); // up down
        myservo5.detach();  // up down
        myservoA3.detach();
        //end sad
        
        delay (400);
        
        myservo8.attach(8); //
        myservo10.attach(10); // 
        myservo9.attach(9); // 
        myservo12.attach(12); // 
        myservo11.attach(11);  //
        myservo6.attach(6); // 
        myservo7.attach(7);  // 
        myservo4.attach(4); // 
        myservo5.attach(5);  // 
        myservoA0.attach(A0); // 
        myservoA1.attach(A1); // 
        myservo3.attach(A4);
        myservoA3.attach(A3);
        
        myservo3.write(225);
        myservo8.write(115);  // 
        myservo10.write(-20);  // 
        myservo9.write(70);  // 
        delay(100);
        myservo7.write(95);  //  
        myservo6.write(52);  // 
        delay(200);
        myservo4.write(60); //
        myservo5.write(90);  // 
        delay(100);
        myservoA0.write(170); // 
        myservoA1.write(100); //
        myservo12.write(20); // 
        myservo11.write(75);  //
        myservoA3.write(225);
        delay(150);
        
        myservo3.detach();
        myservo8.detach(); // 
        myservo10.detach(); // 
        myservo9.detach(); // 
        //myservo12.detach(); //
        myservo11.detach();  // 
        myservo6.detach(); // left right
        myservo7.detach();  // left right
        myservo4.detach(); // up down
        myservo5.detach();  // up down
        myservoA0.detach(); // 
        myservoA1.detach(); // 
        myservoA3.detach();
        //end confused
        delay(400);
        myservo6.attach(6); // 
        myservo7.attach(7);  // 
        myservo4.attach(4); // 
        myservo5.attach(5);  
        myservo10.attach(10); // 
        myservo9.attach(9);   // 
        //myservo12.attach(12); // 
        //myservo11.attach(11);  // 
        myservoA0.attach(A0); // 
        myservoA1.attach(A1);  
        myservo3.attach(A4);  // 
        myservo8.attach(8);
        myservoA3.attach(A3);
        
        myservo10.write(145);  // left lip down
        myservo9.write(70);  // right lip down
        myservo8.write(90);
        //myservo12.write(110); // left eyebrow sad
        //myservo11.write(80); // right eyebrow sad
        myservoA0.write(75);
        myservoA1.write(75);
        myservo3.write(225);  
        myservo6.write(25); // 
        myservo7.write(67);  // 
        myservo4.write(130);
        myservo5.write(60);
        myservoA3.write(225);
        delay(400);
        
        myservo10.detach(); //
        myservo9.detach(); //
        myservo8.detach();
        //myservo12.detach(); // 
        //myservo11.detach();  // 
        myservoA0.detach(); // 
        myservoA1.detach();  // 
        myservo6.detach(); // left right
        myservo7.detach();  // left right
        myservo4.detach(); // up down
        myservo5.detach();  // up down
        myservoA3.detach();
        
      }
      */
       else if(pos=='W')
      {
        //Look at all the faces I can make!
         lipsup(); //1)Smile
         delay(300);
         lipsdown(); //2) Frown
         delay(300);
         mouthopenbig(); //Surprise
         delay(50);
         eyesopenwide();
         delay(50);
         eyesstraightLR();
         delay(50);
         eyesstraightUD();
         delay(300);
         lipsdown(); //Angry
         delay(50);
         eyeshalfshut();
         delay(50);
         mouthopenmed();
         delay(50);
         eyesstraightLR();
         delay(50);
         eyesstraightUD();
         delay(300);
         eyesup(); //Confused
         delay(50);
         eyesright();
         delay(50);
         lipsslant();
         delay(50);
         eyesopenwide();
         delay(50);
         mouthopenmed();
         delay(50);
         eyesstraightUD();
         delay(300);
         eyessilly(); //Funny
         delay(50);
         lipsup();
         delay(50);
         mouthopenbig();
         delay(300);
      }
      else if(pos=='X') { 
        //Look what I can do with my eyes
        lipsup();
        delay(50);
        mouthopenbig();
        delay(200);
        lipsslant();
        delay(50);
        mouthclosed();
        delay(200);
        lipsdown();
        delay(100);
        mouthopenmed();
        delay(200);
        lipsneutral();
        delay(50);
        mouthclosed();
        delay(500);
      }
    } 
  } 
} 

