using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponFireStaff : Weapon {

    private void Start() {
        textDamage.text = damage.ToString();
    }

    public override bool Use() {
        return false;
    }
}