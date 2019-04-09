using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Accessory : Entity {
    
    public override bool Click() {
        Player player = GetPlayer();
        player.SetAccessory(gameObject);

        Dungeon.GetAudio().PlaySelect();

        caught = true;

        return true;
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