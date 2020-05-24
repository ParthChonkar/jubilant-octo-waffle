#include <LiquidCrystal.h>
#include <SR04.h>



#define BUZZER_PIN 4

#define TRIG_PIN 5
#define ECHO_PIN 6

#define LCD_RS 7
#define LCD_ENABLE 8
#define LCD_D4 9
#define LCD_D5 10
#define LCD_D6 11
#define LCD_D7 12



const int buzz_time = 400;

LiquidCrystal lcd(7, 8, 9, 10, 11, 12);

SR04 sensor(ECHO_PIN, TRIG_PIN);


int infractions = 0;
void setup() {

	lcd.begin(16, 2);
	lcd.print("PhoneBox");
  lcd.setCursor(0, 1);
  lcd.print("Mess-ups: ");

	pinMode(BUZZER_PIN, OUTPUT);

}


void loop() {
  
	if(sensor.Distance()<9){
		infractions++;
		buzz();
	}
 updateLCD(infractions);
	
}


void updateLCD(int info) {
	lcd.setCursor(11, 1);
	lcd.print(info);
}


//BEEEEEEEEEP
void buzz() {

	for (int i = 0; i<buzz_time; i++) {
		digitalWrite(BUZZER_PIN, HIGH);
		delay(2);
		digitalWrite(BUZZER_PIN, LOW);
		delay(2);		
	}
}
