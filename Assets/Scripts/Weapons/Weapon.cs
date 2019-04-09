using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : Entity {

    public DamageType damageType;
    public int damage;

    public Text textDamage;

    public string effectName;               //The name of the gameobject in the folder Prefabs/Effects.

    public override bool Click() {
        Player player = GetPlayer();
        player.SetWeapon(gameObject);

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