int a = 0;

void setup() {
  // put your setup code here, to run once:
pinMode(A0, INPUT);
Serial.begin(74880);
}

void loop() {
  // put your main code here, to run repeatedly:
a = analogRead(A0);

Serial.println(a);
delay(250);
}
