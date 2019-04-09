using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Armor : Entity {

    public int blockDamage;

    public Text textBlockDamage;

    public override bool Click() {
        Player player = GetPlayer();
        player.SetArmor(gameObject);

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