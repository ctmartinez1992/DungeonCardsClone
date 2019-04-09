using UnityEngine;
using System.Collections;

public class WeaponNone : Weapon {
    
    public override bool Click() {
        return false;
    }

    public override bool Use() {
        return false;
    }
}
