using UnityEngine;
using System.Collections;

public class ItemWhetstone : Item {

    public int bonusAttackToWeapon;

    public override bool Click() {
        Dungeon dungeon = (GameObject.Find("Dungeon") as GameObject).GetComponent<Dungeon>();
        
        if(GetPlayer().weapon) {
            GetPlayer().weapon.damage += bonusAttackToWeapon;
            GetPlayer().weapon.textDamage.text = GetPlayer().weapon.damage.ToString();

            Dungeon.SpawnDamage(bonusAttackToWeapon, Dungeon.DamageColor.Heal, gameObject);
            Dungeon.GetAudio().PlayWhetstone();
        }
        
        caught = true;

        Dead(true);

        return true;
    }
}