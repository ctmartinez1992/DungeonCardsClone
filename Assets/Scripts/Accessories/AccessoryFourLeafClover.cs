using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AccessoryFourLeafClover : Accessory {

    public int luck;

    public Text textLuck;

    private void Start() {
        textLuck.text = luck.ToString();
    }

    public override bool Use() {
        return false;
    }
}