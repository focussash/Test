
char instring[5]; //This stores the input from the Serial port
int instring_int[5]; //This stores the input from the Serial port converted to integer 


int i; 
int k;
int j;//Placeholder counter

int pumppins[6]= {3,5,6,9,10,0};//pins for pumps
int valvepins[6] = {3,5,6,9,10,0};//pins for valves

int device_number;//To store the numbering of device
int device_type;//To store the type of device
int control_type;//To store the control type of device (digital/analog)
int device_intensity;//To store intensity of device

void setup() {
  Serial.begin(9600);
  for (i=0;i<6;i++)
  {
    pinMode (pumppins[i],OUTPUT);
    pinMode (valvepins[i],OUTPUT);
  }
  
  Serial.setTimeout(50);
}

void loop() {
  // clear all the input caches and variables
  for (i = 0;i<5;i++)
  {
    instring[i] = ""; 
  }
    device_number = 0;
    device_type = 0;
    control_type = 0;
    device_intensity = 0;

  if (Serial.available()>0){
  Serial.readBytesUntil("\r",instring,5);
  if ((int)instring[3]>48){ //check if the signal is valid?
    for (i = 0;i<5;i++)
    {
      instring_int[i] = (int)instring[i]-48; //parse the input data into 5 integer; the -48 is to convert the ascii number into integer
      Serial.print(instring_int[i]);
    }
    Serial.println("");
    device_number = instring_int[2]*10+ instring_int[3];
    device_type = instring_int[1];
    control_type = instring_int[4];
    device_intensity = instring_int[0];
  } 
  
  switch (device_type){
    case 1://if its a pump
      for (k=1;k<6;k++){
        if (k == device_number){//digital control
          if (control_type == 1){
            if (device_intensity == 1){
              digitalWrite(pumppins[k-1],HIGH);
            }
            else {
              digitalWrite(pumppins[k-1],LOW);
            }
          }
          else //analog control
          {
            analogWrite(pumppins[k-1],device_intensity*255/9);
            Serial.println(device_intensity*255/9);//for trouble shooting
          }
        }
      }
    break;
    
    case 2://if its a solenoid valve
       for (k=1;k<6;k++){
        if (k == device_number){//digital control
          if (control_type == 1){
            if (device_intensity == 1){
              digitalWrite(valvepins[k-1],HIGH);
            }
            else {
              digitalWrite(valvepins[k-1],LOW);
            }
          }
          else //flash-click (on/off quickly) control
          {
            digitalWrite(valvepins[k-1],HIGH);
            delay(500*device_intensity);
            digitalWrite(valvepins[k-1],LOW);
            //for trouble shooting
            Serial.print("Flashed on for ");
            Serial.print(500* device_intensity);
            Serial.println("ms");
          }
        }
      }


    break;
    
    case 3://if its a sensor

    break;
 }
 delay(50);
  }
 }
