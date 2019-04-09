using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HatBeanie : Hat {

    public int strength;

    public Text textStrength;

    private void Start() {
        textStrength.text = strength.ToString();
    }

    public override bool Use() {
        return false;
    }
}