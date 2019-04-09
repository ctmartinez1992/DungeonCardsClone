using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponSteelSword : Weapon {

    public int cooldown;
    private int currentCooldown = 0;

    public Text cooldownText;
    public GameObject cooldownFade;

    private void Start() {
        textDamage.text = damage.ToString();

        cooldownText.text = currentCooldown.ToString();
        cooldownFade.SetActive(false);
    }

    public override bool Use() {
        if(currentCooldown <= 0) {
            currentCooldown = cooldown;
            cooldownText.text = currentCooldown.ToString();

            panelCardsParent.GetComponent<Button>().interactable = false;
            cooldownFade.SetActive(true);

            Dungeon dungeon = ((GameObject.Find("Dungeon") as GameObject).GetComponent<Dungeon>() as Dungeon);

            if(dungeon.leftMidGO.GetComponent<Entity>() is Enemy) {
                ((Enemy)dungeon.leftMidGO.GetComponent<Entity>()).TakeDamage(GetPlayer().GetAttackDamage(), GetPlayer().GetAttackDamageType());
            }
            if(dungeon.rightMidGO.GetComponent<Entity>() is Enemy) {
                ((Enemy)dungeon.rightMidGO.GetComponent<Entity>()).TakeDamage(GetPlayer().GetAttackDamage(), GetPlayer().GetAttackDamageType());
            }
            if(dungeon.midTopGO.GetComponent<Entity>() is Enemy) {
                ((Enemy)dungeon.midTopGO.GetComponent<Entity>()).TakeDamage(GetPlayer().GetAttackDamage(), GetPlayer().GetAttackDamageType());
            }
            if(dungeon.midBotGO.GetComponent<Entity>() is Enemy) {
                ((Enemy)dungeon.midBotGO.GetComponent<Entity>()).TakeDamage(GetPlayer().GetAttackDamage(), GetPlayer().GetAttackDamageType());
            }

            return true;
        }

        return false;
    }

    public override void Process() {
        if(caught) {
            currentCooldown--;
            cooldownText.text = currentCooldown.ToString();

            if(currentCooldown <= 0) {
                currentCooldown = 0;
                cooldownText.text = currentCooldown.ToString();

                panelCardsParent.GetComponent<Button>().interactable = true;
                cooldownFade.SetActive(false);
            }
        }
        else {
            base.Process();
        }
    }
}
