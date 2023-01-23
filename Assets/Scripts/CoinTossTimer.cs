using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinTossTimer : MonoBehaviour
{
    public Sprite[] bigDigits, smallDigits;
    public Image digit1Image, digit2Image, digit3Image;

    //Reset the timer to 0.00
    public void ResetTimer(){
        digit1Image.sprite = bigDigits[0];
        digit2Image.sprite = smallDigits[0];
        digit3Image.sprite = smallDigits[0];
    }

    public void UpdateTimer(float time){
        //Floor the value to get the ones digit
        int digit1 = Mathf.FloorToInt(time);
        float fraction = time - digit1;
        //Multiple the fractional part by 10 and 100 respectively to get the first 2 decimal places
        int digit2 = Mathf.FloorToInt(fraction * 10f);
        int digit3 = Mathf.FloorToInt(fraction * 100f) % 10;

        digit1Image.sprite = bigDigits[digit1];
        digit2Image.sprite = smallDigits[digit2];
        digit3Image.sprite = smallDigits[digit3];
    }
}
