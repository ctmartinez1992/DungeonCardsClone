using UnityEngine;
using System.Collections;

public abstract class Hat : Entity {
    
    public override bool Click() {
        Player player = GetPlayer();
        player.SetHat(gameObject);

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
