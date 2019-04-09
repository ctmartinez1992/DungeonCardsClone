using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gold : Entity {

    public int baseWorth;
    public float worthModifier;

    public Text textWorth;

    private void Start() {
        Player player = (GameObject.Find("Canvas/PanelCards/PanelPlayer/Player") as GameObject).GetComponent<Player>();
        textWorth.text = GetWorth(player.GetLuck()).ToString();
    }

    public int GetWorth(int luck) {
        float value = baseWorth;

        value += luck;
        value *= worthModifier;

        return Mathf.FloorToInt(value);
    }

    public override bool Click() {
        Player player = (GameObject.Find("Canvas/PanelCards/PanelPlayer/Player") as GameObject).GetComponent<Player>();

        int amount = GetWorth(player.GetLuck());
        player.AddGold(amount);

        Dungeon.GetAudio().PlayCoin();
        Dungeon.SpawnDamage(amount, Dungeon.DamageColor.Gold, gameObject);

        caught = true;
        Dead(true);

        return true;
    }

    public override bool Use() {
        return false;
    }

    //Gold disappears based on luck.
    public override void Process() {
        if(!caught && !dead) {
            Player player = (GameObject.Find("Canvas/PanelCards/PanelPlayer/Player") as GameObject).GetComponent<Player>();

            int baseChance = 70;
            int chanceToDisappear = baseChance - Mathf.FloorToInt(player.GetLuck() * 2.5f);

            if(Random.Range(0, 101) < chanceToDisappear) {
                Dungeon.SpawnDamage("LOST", Dungeon.DamageColor.Information, gameObject);

                Dead(true);
            }
        }

        if(!caught && !dead) {
            TurnCycle(false);
        }
    }
}