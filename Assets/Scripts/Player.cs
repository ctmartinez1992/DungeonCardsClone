using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {
    
    [SerializeField] private int strength;        //melee damage, health and melee resistance.
    [SerializeField] private int dexterity;       //crit, crit chance, dodge.
    [SerializeField] private int perception;      //% to find secrets, crit chance, escaping.
    [SerializeField] private int intelligence;    //magic damage, magic resistance.
    [SerializeField] private int luck;            //% of finding better things.

    public int hp;
    public int maxHP;
    public int xp;
    public int maxXP;
    public int level;
    public int gold;

    public Weapon weapon;
    public Armor armor;
    public Hat hat;
    public Accessory accessory;

    public Text panelBotStrengthText;
    public Text panelBotDexterityText;
    public Text panelBotPerceptionText;
    public Text panelBotIntelligenceText;
    public Text panelBotLuckText;

    public Text panelBotHPText;
    public Text panelBotXPText;
    public Text panelBotLevelText;
    public Text panelBotGoldText;

    void Start() {
        UpdateStats();

        UpdateHP();
        UpdateXP();
        UpdateLevel();
        UpdateGold();
    }

    public void UpdateStats() {
        panelBotStrengthText.text = GetStrength().ToString();
        panelBotDexterityText.text = GetDexterity().ToString();
        panelBotPerceptionText.text = GetPerception().ToString();
        panelBotIntelligenceText.text = GetIntelligence().ToString();
        panelBotLuckText.text = GetLuck().ToString();
    }

    public void AddGold(int amount) {
        gold += amount;
        panelBotGoldText.text = gold.ToString();
    }

    public void Heal(int amount) {
        hp += amount;
        Dungeon.SpawnDamage(amount, Dungeon.DamageColor.Heal, gameObject);

        if(hp > maxHP) {
            hp = maxHP;
        }

        UpdateHP();
    }

    public void TakeDamage(int amount, DamageType damageType) {
        int damageReceived = amount - GetBlockDamage();

        if(damageReceived < 0) {
            damageReceived = 0;
        }

        hp -= damageReceived;
        Dungeon.SpawnDamage(damageReceived, Dungeon.DamageColor.Damage, gameObject);

        if(hp <= 0) {
            hp = 0;

            Debug.Log("Game Over");
        }

        UpdateHP();
    }

    public void SetWeapon(GameObject weaponGO) {
        Dungeon dungeon = ((GameObject.Find("Dungeon") as GameObject).GetComponent<Dungeon>() as Dungeon);

        //It's above 1 because the None child is always there.
        if(dungeon.buttonWeaponGO.transform.childCount > 1 && weapon) {
            Destroy(dungeon.weaponGO);
        }

        GameObject panel = GameObject.Find("Canvas/PanelCards/ButtonWeapon") as GameObject;
        weaponGO.transform.SetParent(panel.transform, false);

        ((GameObject.Find("Dungeon") as GameObject).GetComponent<Dungeon>() as Dungeon).weaponGO = weaponGO;

        weapon = weaponGO.GetComponent<Weapon>();

        UpdateStats();
        dungeon.UpdateGoldTilesIfThereAreAny();

        GameObject.Find("Canvas/PanelCards/ButtonWeapon/WeaponNone").SetActive(false);
    }

    public void SetArmor(GameObject armorGO) {
        Dungeon dungeon = ((GameObject.Find("Dungeon") as GameObject).GetComponent<Dungeon>() as Dungeon);

        //It's above 1 because the None child is always there.
        if(dungeon.buttonArmorGO.transform.childCount > 1 && armor) {
            Destroy(dungeon.armorGO);
        }

        GameObject panel = GameObject.Find("Canvas/PanelCards/ButtonArmor") as GameObject;
        armorGO.transform.SetParent(panel.transform, false);

        ((GameObject.Find("Dungeon") as GameObject).GetComponent<Dungeon>() as Dungeon).armorGO = armorGO;

        armor = armorGO.GetComponent<Armor>();

        UpdateStats();
        dungeon.UpdateGoldTilesIfThereAreAny();

        GameObject.Find("Canvas/PanelCards/ButtonArmor/ArmorNone").SetActive(false);
    }

    public void SetHat(GameObject hatGO) {
        Dungeon dungeon = ((GameObject.Find("Dungeon") as GameObject).GetComponent<Dungeon>() as Dungeon);

        //It's above 1 because the None child is always there.
        if(dungeon.buttonHatGO.transform.childCount > 1 && hat) {
            Destroy(dungeon.hatGO);
        }

        GameObject panel = GameObject.Find("Canvas/PanelCards/ButtonHat") as GameObject;
        hatGO.transform.SetParent(panel.transform, false);

        ((GameObject.Find("Dungeon") as GameObject).GetComponent<Dungeon>() as Dungeon).hatGO = hatGO;

        hat = hatGO.GetComponent<Hat>();

        UpdateStats();
        dungeon.UpdateGoldTilesIfThereAreAny();
        
        GameObject.Find("Canvas/PanelCards/ButtonHat/HatNone").SetActive(false);
    }

    public void SetAccessory(GameObject accessoryGO) {
        Dungeon dungeon = ((GameObject.Find("Dungeon") as GameObject).GetComponent<Dungeon>() as Dungeon);

        //It's above 1 because the None child is always there.
        if(dungeon.buttonAccessoryGO.transform.childCount > 1 && accessory) {
            Destroy(dungeon.accessoryGO);
        }

        GameObject panel = GameObject.Find("Canvas/PanelCards/ButtonAccessory") as GameObject;
        accessoryGO.transform.SetParent(panel.transform, false);

        ((GameObject.Find("Dungeon") as GameObject).GetComponent<Dungeon>() as Dungeon).accessoryGO = accessoryGO;

        accessory = accessoryGO.GetComponent<Accessory>();

        UpdateStats();
        dungeon.UpdateGoldTilesIfThereAreAny();

        GameObject.Find("Canvas/PanelCards/ButtonAccessory/AccessoryNone").SetActive(false);
    }

    public int GetAttackDamage() {
        int value = GetStrength();

        if(weapon && !(weapon is WeaponNone)) {
            value += weapon.damage;
        }

        if(value <= 0) {
            value = 1;
        }

        return value;
    }

    public DamageType GetAttackDamageType() {
        DamageType value = DamageType.None;

        if(weapon) {
            value = weapon.damageType;
        }

        return value;
    }

    public int GetBlockDamage() {
        int value = Mathf.FloorToInt(GetStrength() / 2);

        if(armor && !(armor is ArmorNone)) {
            value += armor.blockDamage;
        }
        if(hat && !(hat is HatNone)) {
            if(hat is HatSteelHelmet) {
                value += ((HatSteelHelmet)hat).blockDamage;
            }
        }

        if(value < 0) {
            value = 0;
        }

        return value;
    }

    public void ReceiveXP(int xp) {
        this.xp += xp;

        int overflowXP = 0;

        if(this.xp > maxXP) {
            overflowXP = this.xp - maxXP;
            
            level++;
            maxXP = Mathf.FloorToInt(maxXP * (1.25f + (level / 10.0f)) - (level * 1.1f));

            xp = overflowXP;
        }

        panelBotLevelText.text = level.ToString();
        panelBotXPText.text = this.xp.ToString() + "/" + maxXP.ToString();
    }

    public int GetStrength() {
        Dungeon dungeon = ((GameObject.Find("Dungeon") as GameObject).GetComponent<Dungeon>() as Dungeon);

        int value = strength;
        
        if(hat && hat is HatBeanie) {
            value += ((HatBeanie)hat).strength;
        }

        return value;
    }
    public int GetDexterity() {
        Dungeon dungeon = ((GameObject.Find("Dungeon") as GameObject).GetComponent<Dungeon>() as Dungeon);

        int value = dexterity;

        if(armor && armor is ArmorLeatherArmor) {
            value += ((ArmorLeatherArmor)armor).dexterity;
        }

        return value;
    }
    public int GetPerception() {
        Dungeon dungeon = ((GameObject.Find("Dungeon") as GameObject).GetComponent<Dungeon>() as Dungeon);

        int value = perception;

        if(hat && hat is HatCool) {
            value += ((HatCool)hat).perception;
        }

        return value;
    }
    public int GetIntelligence() {
        Dungeon dungeon = ((GameObject.Find("Dungeon") as GameObject).GetComponent<Dungeon>() as Dungeon);

        int value = intelligence;

        if(hat && hat is HatWizardHat) {
            value += ((HatWizardHat)hat).intelligence;
        }
        if(armor && armor is ArmorWizardRobe) {
            value += ((ArmorWizardRobe)armor).intelligence;
        }

        return value;
    }
    public int GetLuck() {
        Dungeon dungeon = ((GameObject.Find("Dungeon") as GameObject).GetComponent<Dungeon>() as Dungeon);

        int value = luck;

        if(accessory && accessory is AccessoryFourLeafClover) {
            value += ((AccessoryFourLeafClover)accessory).luck;
        }

        return value;
    }

    private void UpdateHP() {
        panelBotHPText.text = hp.ToString();
    }
    private void UpdateXP() {
        panelBotXPText.text = xp.ToString();
    }
    private void UpdateLevel() {
        panelBotLevelText.text = level.ToString();
    }
    private void UpdateGold() {
        panelBotGoldText.text = gold.ToString();
    }
}