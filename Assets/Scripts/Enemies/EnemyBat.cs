using UnityEngine;
using System.Collections;

public class EnemyBat : Enemy {
    private new void Start() {
        Player player = GetPlayer();

        float hpGainBasedOnLevel = 0.33f + player.level * 1.95f;
        float hpRandomGain = Random.Range(-5.9f, 11.9f);
        float hpGainBasedOnLuck = MathMore.Remap((player.GetLuck() / 1.5f), 1, 25, -3.1f, 2.9f);
        int finalHPGain = Mathf.FloorToInt(hpGainBasedOnLevel + hpRandomGain + hpGainBasedOnLuck);

        maxHP = baseHP + finalHPGain;
        if(maxHP < 1) {
            maxHP = 1;
        }

        float damageGainBasedOnLevel = 0.1f + player.level * 0.9f;
        float damageRandomGain = Random.Range(-1.1f, 3.1f);
        float damageGainBasedOnLuck = MathMore.Remap((player.GetLuck() / 1.5f), 1, 25, -1.3f, 3.3f);
        int finalDamageGain = Mathf.FloorToInt(damageGainBasedOnLevel + damageRandomGain + damageGainBasedOnLuck);

        damage = baseDamage + finalDamageGain;
        if(damage < 1) {
            damage = 1;
        }

        base.Start();
    }
}
