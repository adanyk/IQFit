using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    // Choose level
    private static bool lvlChosen;
    public static int lvl;


    // Start is called before the first frame update
    void Start()
    {
        lvlChosen = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Choose level
        if (lvlChosen)
        {
            SceneManager.LoadScene(1);
            this.gameObject.SetActive(false);
        }
    }

    // Level Buttons
    public static void Lvl1()  { lvl = 1;  lvlChosen = true; }
    public static void Lvl2()  { lvl = 2;  lvlChosen = true; }
    public static void Lvl3()  { lvl = 3;  lvlChosen = true; }
    public static void Lvl4()  { lvl = 4;  lvlChosen = true; }
    public static void Lvl5()  { lvl = 5;  lvlChosen = true; }
    public static void Lvl6()  { lvl = 6;  lvlChosen = true; }
    public static void Lvl7()  { lvl = 7;  lvlChosen = true; }
    public static void Lvl8()  { lvl = 8;  lvlChosen = true; }
    public static void Lvl9()  { lvl = 9;  lvlChosen = true; }
    public static void Lvl10() { lvl = 10; lvlChosen = true; }
    public static void Lvl11() { lvl = 11; lvlChosen = true; }
    public static void Lvl12() { lvl = 12; lvlChosen = true; }
    public static void Lvl13() { lvl = 13; lvlChosen = true; }
    public static void Lvl14() { lvl = 14; lvlChosen = true; }
    public static void Lvl15() { lvl = 15; lvlChosen = true; }
    public static void Lvl16() { lvl = 16; lvlChosen = true; }
    public static void Lvl17() { lvl = 17; lvlChosen = true; }
    public static void Lvl18() { lvl = 18; lvlChosen = true; }
    public static void Lvl19() { lvl = 19; lvlChosen = true; }
    public static void Lvl20() { lvl = 20; lvlChosen = true; }
    public static void Lvl21() { lvl = 21; lvlChosen = true; }
    public static void Lvl22() { lvl = 22; lvlChosen = true; }
    public static void Lvl23() { lvl = 23; lvlChosen = true; }
    public static void Lvl24() { lvl = 24; lvlChosen = true; }
    public static void Lvl25() { lvl = 25; lvlChosen = true; }
    public static void Lvl26() { lvl = 26; lvlChosen = true; }
    public static void Lvl27() { lvl = 27; lvlChosen = true; }
    public static void Lvl28() { lvl = 28; lvlChosen = true; }
    public static void Lvl29() { lvl = 29; lvlChosen = true; }
    public static void Lvl30() { lvl = 30; lvlChosen = true; }
    public static void Lvl31() { lvl = 31; lvlChosen = true; }
    public static void Lvl32() { lvl = 32; lvlChosen = true; }
    public static void Lvl33() { lvl = 33; lvlChosen = true; }
    public static void Lvl34() { lvl = 34; lvlChosen = true; }
    public static void Lvl35() { lvl = 35; lvlChosen = true; }
    public static void Lvl36() { lvl = 36; lvlChosen = true; }
    public static void Lvl37() { lvl = 37; lvlChosen = true; }
    public static void Lvl38() { lvl = 38; lvlChosen = true; }
    public static void Lvl39() { lvl = 39; lvlChosen = true; }
    public static void Lvl40() { lvl = 40; lvlChosen = true; }
    public static void Lvl41() { lvl = 41; lvlChosen = true; }
    public static void Lvl42() { lvl = 42; lvlChosen = true; }
    public static void Lvl43() { lvl = 43; lvlChosen = true; }
    public static void Lvl44() { lvl = 44; lvlChosen = true; }
    public static void Lvl45() { lvl = 45; lvlChosen = true; }
    public static void Lvl46() { lvl = 46; lvlChosen = true; }
    public static void Lvl47() { lvl = 47; lvlChosen = true; }
    public static void Lvl48() { lvl = 48; lvlChosen = true; }
    public static void Lvl49() { lvl = 49; lvlChosen = true; }
    public static void Lvl50() { lvl = 50; lvlChosen = true; }
    public static void Lvl51() { lvl = 51; lvlChosen = true; }
    public static void Lvl52() { lvl = 52; lvlChosen = true; }
    public static void Lvl53() { lvl = 53; lvlChosen = true; }
    public static void Lvl54() { lvl = 54; lvlChosen = true; }
    public static void Lvl55() { lvl = 55; lvlChosen = true; }
    public static void Lvl56() { lvl = 56; lvlChosen = true; }
    public static void Lvl57() { lvl = 57; lvlChosen = true; }
    public static void Lvl58() { lvl = 58; lvlChosen = true; }
    public static void Lvl59() { lvl = 59; lvlChosen = true; }
    public static void Lvl60() { lvl = 60; lvlChosen = true; }
    public static void Lvl61() { lvl = 61; lvlChosen = true; }
    public static void Lvl62() { lvl = 62; lvlChosen = true; }
    public static void Lvl63() { lvl = 63; lvlChosen = true; }
    public static void Lvl64() { lvl = 64; lvlChosen = true; }
    public static void Lvl65() { lvl = 65; lvlChosen = true; }
    public static void Lvl66() { lvl = 66; lvlChosen = true; }
    public static void Lvl67() { lvl = 67; lvlChosen = true; }
    public static void Lvl68() { lvl = 68; lvlChosen = true; }
    public static void Lvl69() { lvl = 69; lvlChosen = true; }
    public static void Lvl70() { lvl = 70; lvlChosen = true; }
    public static void Lvl71() { lvl = 71; lvlChosen = true; }
    public static void Lvl72() { lvl = 72; lvlChosen = true; }
    public static void Lvl73() { lvl = 73; lvlChosen = true; }
    public static void Lvl74() { lvl = 74; lvlChosen = true; }
    public static void Lvl75() { lvl = 75; lvlChosen = true; }
    public static void Lvl76() { lvl = 76; lvlChosen = true; }
    public static void Lvl77() { lvl = 77; lvlChosen = true; }
    public static void Lvl78() { lvl = 78; lvlChosen = true; }
    public static void Lvl79() { lvl = 79; lvlChosen = true; }
    public static void Lvl80() { lvl = 80; lvlChosen = true; }
    public static void Lvl81() { lvl = 81; lvlChosen = true; }
    public static void Lvl82() { lvl = 82; lvlChosen = true; }
    public static void Lvl83() { lvl = 83; lvlChosen = true; }
    public static void Lvl84() { lvl = 84; lvlChosen = true; }
    public static void Lvl85() { lvl = 85; lvlChosen = true; }
    public static void Lvl86() { lvl = 86; lvlChosen = true; }
    public static void Lvl87() { lvl = 87; lvlChosen = true; }
    public static void Lvl88() { lvl = 88; lvlChosen = true; }
    public static void Lvl89() { lvl = 89; lvlChosen = true; }
    public static void Lvl90() { lvl = 90; lvlChosen = true; }
    public static void Lvl91() { lvl = 91; lvlChosen = true; }
    public static void Lvl92() { lvl = 92; lvlChosen = true; }
    public static void Lvl93() { lvl = 93; lvlChosen = true; }
    public static void Lvl94() { lvl = 94; lvlChosen = true; }
    public static void Lvl95() { lvl = 95; lvlChosen = true; }
    public static void Lvl96() { lvl = 96; lvlChosen = true; }
    public static void Lvl97() { lvl = 97; lvlChosen = true; }
    public static void Lvl98() { lvl = 98; lvlChosen = true; }
    public static void Lvl99() { lvl = 99; lvlChosen = true; }
    public static void Lvl100() { lvl = 100; lvlChosen = true; }
    public static void Lvl101() { lvl = 101; lvlChosen = true; }
    public static void Lvl102() { lvl = 102; lvlChosen = true; }
    public static void Lvl103() { lvl = 103; lvlChosen = true; }
    public static void Lvl104() { lvl = 104; lvlChosen = true; }
    public static void Lvl105() { lvl = 105; lvlChosen = true; }
    public static void Lvl106() { lvl = 106; lvlChosen = true; }
    public static void Lvl107() { lvl = 107; lvlChosen = true; }
    public static void Lvl108() { lvl = 108; lvlChosen = true; }
    public static void Lvl109() { lvl = 109; lvlChosen = true; }
    public static void Lvl110() { lvl = 110; lvlChosen = true; }
    public static void Lvl111() { lvl = 111; lvlChosen = true; }
    public static void Lvl112() { lvl = 112; lvlChosen = true; }
    public static void Lvl113() { lvl = 113; lvlChosen = true; }
    public static void Lvl114() { lvl = 114; lvlChosen = true; }
    public static void Lvl115() { lvl = 115; lvlChosen = true; }
    public static void Lvl116() { lvl = 116; lvlChosen = true; }
    public static void Lvl117() { lvl = 117; lvlChosen = true; }
    public static void Lvl118() { lvl = 118; lvlChosen = true; }
    public static void Lvl119() { lvl = 119; lvlChosen = true; }
    public static void Lvl120() { lvl = 120; lvlChosen = true; }

}
