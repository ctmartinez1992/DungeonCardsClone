using UnityEngine;
using System.Collections;

public class ArmorNone : Armor {
    
    public override bool Click() {
        return false;
    }

    public override bool Use() {
        return false;
    }
}
