using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class Enemy : Entity {

    public int hp;
    public int maxHP;

    public DamageType damageType;
    public int damage;

    public DamageType resistance;
    public DamageType vulnerable;

    public int baseHP;
    public int baseDamage;

    public Text textHP;
    public Text textAttack;

    public int stunned;                             //The number of turns stunned. Decreases every turn.
    public bool onFire;                             //Take 1 dmg every turn. Take 1 extra dmg per fire attack.
    /*public bool onIce;*/                          //Ice attacks do double damage.
    public bool onPoison;                           //Take 1 dmg every turn. No one resists poison. Never kills anyone, always stops when hp = 1.
    
    protected void Start() {
        hp = maxHP;

        textHP.text = hp.ToString();
        textAttack.text = damage.ToString();
    }

    public override bool Click() {
        Player player = GetPlayer();

        Dungeon.GetAudio().PlaySoundAccordingToWeapon(player.weapon);

        TakeDamage(player.GetAttackDamage(), player.GetAttackDamageType());
        
        return hp == 0;
    }

    public override bool Use() {
        return false;
    }

    public override void Process() {
        if(!dead && !caught) {
            base.Process();
        }

        if(!dead && !caught) {
            if(onFire) {
                hp--;
                Dungeon.SpawnDamage(1, Dungeon.DamageColor.Damage, panelCardsParent);

                ParticleSystem firePS = (transform.Find("Fire(Clone)").gameObject as GameObject).GetComponent<ParticleSystem>();
                firePS.Emit(20);
                firePS.Play();

                CheckIfDead();
            }
            if(onPoison) {
                if(hp > 1) {
                    hp--;
                    Dungeon.SpawnDamage(1, Dungeon.DamageColor.Damage, panelCardsParent);
                    CheckIfDead();
                }
                else {
                    Destroy((GameObject)transform.Find("OnPoison").gameObject);
                }
            }

            if(stunned > 0) {
                stunned--;
            }
            else {
                Animator animator = panelCardsParent.GetComponent<Animator>();
                if(panelCardsParent.name == "ButtonLeftMid") {
                    animator.SetTrigger("AttackLeft");
                }
                else if(panelCardsParent.name == "ButtonMidTop") {
                    animator.SetTrigger("AttackTop");
                }
                else if(panelCardsParent.name == "ButtonRightMid") {
                    animator.SetTrigger("AttackRight");
                }
                else if(panelCardsParent.name == "ButtonMidBot") {
                    animator.SetTrigger("AttackBot");
                }
            }
        }
    }

    public void AttackPlayer() {
        Player player = GetPlayer();
        player.TakeDamage(damage, damageType);
    }

    public void TakeDamage(int damage, DamageType damageType) {
        int newDamage = damage;

        if(damageType != DamageType.None) {
            if(damageType == resistance) {
                newDamage = Mathf.FloorToInt(damage / 2);
            }
            if(damageType == vulnerable) {
                switch(vulnerable) {
                    case DamageType.Fire:
                        newDamage++;

                        GameObject fireIcon = Instantiate(Resources.Load("Prefabs/Stats/OnFire")) as GameObject;
                        fireIcon.transform.SetParent(transform, false);

                        if(!onFire) {
                            GameObject fire = Instantiate(Resources.Load("Prefabs/Effects/Fire")) as GameObject;
                            fire.transform.SetParent(transform, false);
                        }

                        onFire = true;

                        break;

                    case DamageType.Thunder:
                        stunned = Mathf.FloorToInt(Random.Range(-1, 4)) + Mathf.FloorToInt(GetPlayer().GetLuck() / 5);
                        if(stunned < 1) {
                            stunned = 1;
                        }

                        GameObject stun = Instantiate(Resources.Load("Prefabs/Stats/Stunned")) as GameObject;
                        stun.transform.SetParent(transform, false);

                        break;

                    case DamageType.Ice:
                        newDamage *= 2;

                        break;
                }
            }
            if(damageType == DamageType.Poison) {
                onPoison = true;

                GameObject poison = Instantiate(Resources.Load("Prefabs/Stats/OnPoison")) as GameObject;
                poison.transform.SetParent(transform, false);
            }
        }

        Dungeon.GetAudio().PlaySoundAccordingToDamageType(damageType);

        hp -= newDamage;
        Dungeon.SpawnDamage(newDamage, Dungeon.DamageColor.Damage, panelCardsParent);
        UpdateHP();
        
        Dungeon dungeon = ((GameObject.Find("Dungeon") as GameObject).GetComponent<Dungeon>() as Dungeon);
        dungeon.PlayEffect(transform, GetPlayer().weapon);

        CheckIfDead();
    }

    public bool CheckIfDead() {
        if(hp <= 0 && !dead) {
            hp = 0;
            UpdateHP();

            Dead(true);

            Player player = GetPlayer();

            int xp = GetXP();
            player.ReceiveXP(xp);

            Dungeon.SpawnDamage(xp, Dungeon.DamageColor.XP, player.gameObject);

            return true;
        }

        UpdateHP();

        return false;
    }

    public void UpdateHP() {
        textHP.text = hp.ToString();
    }

    public int GetXP() {
        int value = 1;

        Player player = GetPlayer();

        value += Mathf.FloorToInt((maxHP * 0.4f) + (damage * 0.75f) + (player.level * 0.6f));
        value += Mathf.FloorToInt(MathMore.Remap(player.GetLuck(), 1, 25, -1.0f, 3.0f));
        value += Random.Range(-2, 2);

        if(value < 1) {
            value = 1;
        }

        return value;
    }
}