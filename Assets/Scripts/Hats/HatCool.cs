using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HatCool : Hat {

    public int perception;

    public Text textPerception;

    private void Start() {
        textPerception.text = perception.ToString();
    }

    public override bool Use() {
        return false;
    }
}