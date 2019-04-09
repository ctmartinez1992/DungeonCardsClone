using UnityEngine;
using System.Collections;

public class AccessoryNone : Accessory {

    public override bool Click() {
        return false;
    }

    public override bool Use() {
        return false;
    }
}
