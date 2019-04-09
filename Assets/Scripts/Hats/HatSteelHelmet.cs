using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HatSteelHelmet : Hat {

    public int blockDamage;

    public Text textBlockDamage;

    private void Start() {
        textBlockDamage.text = blockDamage.ToString();
    }

    public override bool Use() {
        return false;
    }
}
