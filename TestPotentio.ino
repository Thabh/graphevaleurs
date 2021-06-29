void setup() {
  Serial.begin(9600);
}

void loop() {
  int val = analogRead(A0);

  Serial.print(val, DEC); // affiche la variable dans le Terminal Serie
  Serial.println(); 

  delay(500);
}
