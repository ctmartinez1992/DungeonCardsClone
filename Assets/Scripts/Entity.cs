using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum DamageType {
    None = 0,
    Fire,
    Thunder,
    Ice,
    Poison
}

public abstract class Entity : MonoBehaviour {

    public string entityName;

    public GameObject panelCardsParent;         //To be assigned by SpawnEntity in the Dungeon class.

    //If the player clicked on it, don't process it.
    public bool caught = false;

    public bool dead = false;

    //Chance to disappear. This does not concern gold, where the chance to disappear is based on player's luck.
    public int baseChanceToDisappear;
    public int chanceToDisappearIncrement;

    //When you click on the entity that is left, right, top or bottom.
    //Returns true if the entity is removed from the tile.
    public abstract bool Click();

    //When you click on an equipped weapon, armor, hat or accessory.
    //Returns true if the entity is used.
    public abstract bool Use();

    //When a turn passes, the entity will do something. By default, it does nothing.
    public virtual void Process() {
        //Chance to disappear.
        Player player = GetPlayer();

        int chanceToDisappear = baseChanceToDisappear + chanceToDisappearIncrement;

        if(chanceToDisappear >= 100 || Random.Range(0, 101) < chanceToDisappear) {
            Dungeon.SpawnDamage("BYE", Dungeon.DamageColor.Information, gameObject);

            baseChanceToDisappear += chanceToDisappearIncrement;

            Dead(true);
        }
    }

    public void Dead(bool destroy) {
        if(!dead) {
            dead = true;

            Dungeon dungeon = (GameObject.Find("Dungeon") as GameObject).GetComponent<Dungeon>();
            Debug.Log(gameObject.name + "  Pause  Dead");
            dungeon.Pause();

            Animator animator = panelCardsParent.GetComponent<Animator>();

            if(destroy) {
                animator.SetBool("FadeOutAndDestroy", true);
            }
            else {
                animator.SetBool("FadeOut", true);
            }

            TurnCycle(true);
        }
    }

    public void TurnCycle(bool spawn) {
        Dungeon dungeon = (GameObject.Find("Dungeon") as GameObject).GetComponent<Dungeon>();
        if(dungeon.turnEnemy) {
            if(panelCardsParent.name == "ButtonLeftMid") {
                dungeon.spawnNewLeft = spawn;
                dungeon.PassTurnTop();
            }
            else if(panelCardsParent.name == "ButtonMidTop") {
                dungeon.spawnNewTop = spawn;
                dungeon.PassTurnRight();
            }
            else if(panelCardsParent.name == "ButtonRightMid") {
                dungeon.spawnNewRight = spawn;
                dungeon.PassTurnBot();
            }
            else if(panelCardsParent.name == "ButtonMidBot") {
                dungeon.spawnNewBot = spawn;
            }
        }
    }

    protected Player GetPlayer() {
        return((GameObject.Find("Canvas/PanelCards/PanelPlayer/Player") as GameObject).GetComponent<Player>() as Player);
    }
}