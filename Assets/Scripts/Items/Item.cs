using UnityEngine;
using System.Collections;

public abstract class Item : Entity {

    public override bool Use() {
        return false;
    }

    public override void Process() {
        if(!dead && !caught) {
            base.Process();
        }

        if(!dead && !caught) {
            TurnCycle(false);
        }
    }
}
