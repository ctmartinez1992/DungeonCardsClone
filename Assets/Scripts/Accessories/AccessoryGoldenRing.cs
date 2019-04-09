using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Randomly switches all 4 tiles.
public class AccessoryGoldenRing : Accessory {

    public int cooldown;
    private int currentCooldown = 0;

    public Text cooldownText;
    public GameObject cooldownFade;

    private void Start() {
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

            Entity leftEntity = dungeon.leftMidGO.GetComponent<Entity>();
            Entity rightEntity = dungeon.rightMidGO.GetComponent<Entity>();
            Entity topEntity = dungeon.midTopGO.GetComponent<Entity>();
            Entity botEntity = dungeon.midBotGO.GetComponent<Entity>();

            if(leftEntity) {
                dungeon.leftMidGO.GetComponent<Entity>().Dead(true);
            }
            if(rightEntity) {
                dungeon.rightMidGO.GetComponent<Entity>().Dead(true);
            }
            if(topEntity) {
                dungeon.midTopGO.GetComponent<Entity>().Dead(true);
            }
            if(botEntity) {
                dungeon.midBotGO.GetComponent<Entity>().Dead(true);
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
