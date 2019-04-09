using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemHealthPotion : Item {

    public int healAmount;

    public Text textHeal;

    private void Start() {
        textHeal.text = healAmount.ToString();
    }

    public override bool Click() {
        Player player = (GameObject.Find("Canvas/PanelCards/PanelPlayer/Player") as GameObject).GetComponent<Player>();
        player.Heal(healAmount);

        Dungeon.GetAudio().PlayRandomHeal();

        caught = true;

        Dead(true);

        return true;
    }
}