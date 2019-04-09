using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArmorLeatherArmor : Armor {

    public int dexterity;

    public Text textDexterity;

    private void Start() {
        textBlockDamage.text = blockDamage.ToString();
        textDexterity.text = dexterity.ToString();
    }

    public override bool Use() {
        return false;
    }
}
