int ref;
int sensor;

void setup() {
 pinMode(A0,INPUT);
 pinMode(A1,INPUT);
 Serial.begin(9600);
}

void loop() {
  ref = analogRead(A0);
  sensor = analogRead(A1);
 // Serial.print("Ref:");
  //Serial.print(ref);
  //Serial.print(" Sensor:");
  Serial.print(1);
  Serial.println(sensor - ref);
  //Serial.println((sensor - ref)*5/1024*1000);
  delay(30);
}
