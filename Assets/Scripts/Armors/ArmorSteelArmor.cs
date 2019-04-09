using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArmorSteelArmor : Armor {

    private void Start() {
        textBlockDamage.text = blockDamage.ToString();
    }

    public override bool Use() {
        return false;
    }
}