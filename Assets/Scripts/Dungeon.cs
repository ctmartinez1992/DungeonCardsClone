using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SpawnType {
    Enemy = 0,
    Item,
    Accessory,
    Weapon,
    Armor,
    Hat,
    Gold
}

public enum SpawnTypeEnemy {
    Bat = 1,
    Goblin,
    Skeleton,
    Length
}

public enum SpawnTypeItem {
    HealthPotion = 1,
    Pizza,
    Whetstone,
    Length
}

public enum SpawnTypeWeapon {
    FireStaff = 1,
    SteelSword,
    WoodenBow,
    Length
}

public enum SpawnTypeHat {
    Beanie = 1,
    SteelHelmet,
    WizardHat,
    Cool,
    Length
}

public enum SpawnTypeArmor {
    LeatherArmor = 1,
    SteelArmor,
    WizardRobe,
    Length
}

public enum SpawnTypeAccessory {
    ChestKey = 1,
    FourLeafClover,
    GoldenRing,
    Length
}

public class Dungeon : MonoBehaviour {

    //These are the game objects that have the button behaviour, not the prefabs to be instantiated.
    public GameObject buttonLeftMidGO;
    public GameObject buttonMidTopGO;
    public GameObject buttonMidBotGO;
    public GameObject buttonRightMidGO;
    public GameObject buttonWeaponGO;
    public GameObject buttonArmorGO;
    public GameObject buttonHatGO;
    public GameObject buttonAccessoryGO;

    //The prefabs that are spawned by this class.
    public GameObject leftMidGO;
    public GameObject midTopGO;
    public GameObject midBotGO;
    public GameObject rightMidGO;

    //The weapon prefabs that are set by the Player class upon clicking on it.
    public GameObject weaponGO;
    public GameObject armorGO;
    public GameObject hatGO;
    public GameObject accessoryGO;

    public bool spawnNewLeft;
    public bool spawnNewRight;
    public bool spawnNewTop;
    public bool spawnNewBot;

    public bool checkSpawns;

    public int busy;                             //Whatever is the number of things that are happening, this counter serves to only resume the game (paused = false), when all is done.
    public bool paused;                          //The game is paused between animations, and the user can't click on anything.

    public bool turnEnemy;

    public Dictionary<SpawnType, int> spawnTypeWeights;

    void Start() {
        busy = 0;
        paused = false;

        turnEnemy = false;

        buttonLeftMidGO = GameObject.Find("Canvas/PanelCards/ButtonLeftMid") as GameObject;
        buttonMidTopGO = GameObject.Find("Canvas/PanelCards/ButtonMidTop") as GameObject;
        buttonMidBotGO = GameObject.Find("Canvas/PanelCards/ButtonMidBot") as GameObject;
        buttonRightMidGO = GameObject.Find("Canvas/PanelCards/ButtonRightMid") as GameObject;
        buttonWeaponGO = GameObject.Find("Canvas/PanelCards/ButtonWeapon") as GameObject;
        buttonArmorGO = GameObject.Find("Canvas/PanelCards/ButtonArmor") as GameObject;
        buttonHatGO = GameObject.Find("Canvas/PanelCards/ButtonHat") as GameObject;
        buttonAccessoryGO = GameObject.Find("Canvas/PanelCards/ButtonAccessory") as GameObject;

        spawnTypeWeights = new Dictionary<SpawnType, int>();
        spawnTypeWeights.Add(SpawnType.Accessory, 0);
        spawnTypeWeights.Add(SpawnType.Armor, 0);
        spawnTypeWeights.Add(SpawnType.Enemy, 0);
        spawnTypeWeights.Add(SpawnType.Gold, 0);
        spawnTypeWeights.Add(SpawnType.Hat, 0);
        spawnTypeWeights.Add(SpawnType.Item, 0);
        spawnTypeWeights.Add(SpawnType.Weapon, 0);

        leftMidGO = SpawnEntity("Prefabs/Weapons/WeaponFireStaff", ref buttonLeftMidGO);
        midTopGO = SpawnGold(ref buttonMidTopGO);
        midBotGO = SpawnGold(ref buttonMidBotGO);
        rightMidGO = SpawnGold(ref buttonRightMidGO);
        //leftMidGO = SpawnRandomEntity(ref buttonLeftMidGO);
        //midTopGO = SpawnRandomEntity(ref buttonMidTopGO);
        //midBotGO = SpawnRandomEntity(ref buttonMidBotGO);
        //rightMidGO = SpawnRandomEntity(ref buttonRightMidGO);

        spawnNewLeft = spawnNewRight = spawnNewTop = spawnNewBot = checkSpawns = false;
    }
    
    void Update() {
    }

    public void DoSpawns() {
        if(leftMidGO == null && spawnNewLeft) {
            leftMidGO = SpawnRandomEntity(ref buttonLeftMidGO);
            spawnNewLeft = false;
        }
        if(rightMidGO == null && spawnNewRight) {
            rightMidGO = SpawnRandomEntity(ref buttonRightMidGO);
            spawnNewRight = false;
        }
        if(midTopGO == null && spawnNewTop) {
            midTopGO = SpawnRandomEntity(ref buttonMidTopGO);
            spawnNewTop = false;
        }
        if(midBotGO == null && spawnNewBot) {
            midBotGO = SpawnRandomEntity(ref buttonMidBotGO);
            spawnNewBot = false;
        }

        Debug.Log(gameObject.name + "  Resume  DoSpawns");
        Resume();
    }

    public void OnClickLeftMid() {
        if(buttonLeftMidGO.transform.childCount > 0) {
            if(Click(leftMidGO)) {
                leftMidGO = null;
                spawnNewLeft = true;
            }
            PassTurns();
        }
    }
    public void OnClickMidTop() {
        if(buttonMidTopGO.transform.childCount > 0) {
            if(Click(midTopGO)) {
                midTopGO = null;
                spawnNewTop = true;
            }
            PassTurns();
        }
    }
    public void OnClickMidBot() {
        if(buttonMidBotGO.transform.childCount > 0) {
            if(Click(midBotGO)) {
                midBotGO = null;
                spawnNewBot = true;
            }
            PassTurns();
        }
    }
    public void OnClickRightMid() {
        if(buttonRightMidGO.transform.childCount > 0) {
            if(Click(rightMidGO)) {
                rightMidGO = null;
                spawnNewRight = true;
            }
            PassTurns();
        }
    }

    public void OnClickWeapon() {
        //It's above 1 because the None child is always there.
        if(buttonWeaponGO.transform.childCount > 1) {
            if(Use(weaponGO)) {
                PassTurns();
            }
        }
    }
    public void OnClickArmor() {
        //It's above 1 because the None child is always there.
        if(buttonArmorGO.transform.childCount > 1) {
            if(Use(armorGO)) {
                PassTurns();
            }
        }
    }
    public void OnClickHat() {
        //It's above 1 because the None child is always there.
        if(buttonHatGO.transform.childCount > 1) {
            if(Use(hatGO)) {
                PassTurns();
            }
        }
    }
    public void OnClickAccessory() {
        //It's above 1 because the None child is always there.
        if(buttonAccessoryGO.transform.childCount > 1) {
            if(Use(accessoryGO)) {
                PassTurns();
            }
        }
    }

    public bool Click(GameObject go) {
        Entity entity = go.GetComponentInChildren<Entity>();
        return entity.Click();
    }
    public bool Use(GameObject go) {
        Entity entity = go.GetComponentInChildren<Entity>();
        return entity.Use();
    }

    private GameObject SpawnEntity(string name, ref GameObject parent) {
        Debug.Log(parent.name + "  Pause  SpawnEntity");
        Pause();

        GameObject go = Instantiate(Resources.Load(name), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        go.transform.SetParent(parent.transform, false);

        Entity entity = go.GetComponent<Entity>();
        entity.panelCardsParent = parent;

        Animator animator = entity.panelCardsParent.GetComponent<Animator>();
        animator.SetBool("FadeIn", true);
        
        return go;
    }

    private void ChangeStateButtons(bool state) {
        (buttonLeftMidGO.GetComponent<Button>() as Button).interactable = state;
        (buttonMidTopGO.GetComponent<Button>() as Button).interactable = state;
        (buttonMidBotGO.GetComponent<Button>() as Button).interactable = state;
        (buttonRightMidGO.GetComponent<Button>() as Button).interactable = state;
        (buttonWeaponGO.GetComponent<Button>() as Button).interactable = state;
        (buttonArmorGO.GetComponent<Button>() as Button).interactable = state;
        (buttonHatGO.GetComponent<Button>() as Button).interactable = state;
        (buttonAccessoryGO.GetComponent<Button>() as Button).interactable = state;
    }

    public void Pause() {
        busy++;
        paused = true;

        ChangeStateButtons(false);
    }

    public void Resume() {
        busy--;

        if(busy == 0) {
            paused = false;

            ChangeStateButtons(true);
        }
    }

    public void PassTurns() {
        turnEnemy = true;

        //Note: Pause to be unpaused by the DoSpawns function.
        Debug.Log(gameObject.name + "  Pause  PassTurns");
        Pause();

        PassTurnEquipment();
        PassTurnLeft();
    }

    public void PassTurnLeft() {
        if(!PassTurn(ref leftMidGO)) {
            PassTurnTop();
        }
    }
    public void PassTurnTop() {
        if(!PassTurn(ref midTopGO)) {
            PassTurnRight();
        }
    }
    public void PassTurnRight() {
        if(!PassTurn(ref rightMidGO)) {
            PassTurnBot();
        }
    }
    public void PassTurnBot() {
        PassTurn(ref midBotGO);

        turnEnemy = false;
        Invoke("DoSpawns", 1);
    }
    public void PassTurnEquipment() {
        PassTurn(ref weaponGO);
        PassTurn(ref armorGO);
        PassTurn(ref hatGO);
        PassTurn(ref accessoryGO);
    }

    public bool PassTurn(ref GameObject go) {
        if(go) {
            Entity goEntity = go.GetComponent<Entity>();
            if(goEntity) {
                goEntity.Process();
                return true;
            }
        }

        return false;
    }

    public GameObject SpawnRandomEntity(ref GameObject buttonGO) {
        Player player = (GameObject.Find("Canvas/PanelCards/PanelPlayer/Player") as GameObject).GetComponent<Player>();

        //Base chance of enemy is always 62%.
        int chanceOfEnemy = 62 - (player.GetLuck() * 2);
        int chanceOfItem = Mathf.FloorToInt((100 - chanceOfEnemy) / 5) - 1;
        int chanceOfAccessory = chanceOfItem;
        int chanceOfWeapon = chanceOfAccessory;
        int chanceOfArmor = chanceOfWeapon;
        int chanceOfHat = chanceOfArmor;
        int chanceOfGold = 100 - chanceOfEnemy - chanceOfItem - chanceOfAccessory - chanceOfWeapon - chanceOfArmor - chanceOfHat;

        spawnTypeWeights[SpawnType.Enemy] = chanceOfEnemy;
        spawnTypeWeights[SpawnType.Item] = chanceOfItem;
        spawnTypeWeights[SpawnType.Accessory] = chanceOfAccessory;
        spawnTypeWeights[SpawnType.Weapon] = chanceOfWeapon;
        spawnTypeWeights[SpawnType.Armor] = chanceOfArmor;
        spawnTypeWeights[SpawnType.Hat] = chanceOfHat;
        spawnTypeWeights[SpawnType.Gold] = chanceOfGold;
        
        SpawnType spawnType = WeightedRandomizer.From(spawnTypeWeights).TakeOne();
        
        switch(spawnType) {
            case SpawnType.Enemy:               return SpawnRandomEnemy(ref buttonGO);
            case SpawnType.Item:                return SpawnRandomItem(ref buttonGO);
            case SpawnType.Accessory:           return SpawnRandomAccessory(ref buttonGO);
            case SpawnType.Weapon:              return SpawnRandomWeapon(ref buttonGO);
            case SpawnType.Armor:               return SpawnRandomArmor(ref buttonGO);
            case SpawnType.Hat:                 return SpawnRandomHat(ref buttonGO);
            case SpawnType.Gold:                return SpawnGold(ref buttonGO);
            default:                            return SpawnRandomEnemy(ref buttonGO);
        }
    }

    public GameObject SpawnRandomEnemy(ref GameObject buttonGO) {
        SpawnTypeEnemy value = (SpawnTypeEnemy)Random.Range(1, (int)SpawnTypeEnemy.Length);
        switch(value) {
            case SpawnTypeEnemy.Bat:
                return SpawnEntity("Prefabs/Enemies/EnemyBat", ref buttonGO);
            case SpawnTypeEnemy.Goblin:
                return SpawnEntity("Prefabs/Enemies/EnemyGoblin", ref buttonGO);
            case SpawnTypeEnemy.Skeleton:
                return SpawnEntity("Prefabs/Enemies/EnemySkeleton", ref buttonGO);
            default:
                return SpawnEntity("Prefabs/Enemies/EnemySkeleton", ref buttonGO);

        }
    }
    public GameObject SpawnRandomItem(ref GameObject buttonGO) {
        SpawnTypeItem value = (SpawnTypeItem)Random.Range(1, (int)SpawnTypeItem.Length);
        switch(value) {
            case SpawnTypeItem.HealthPotion:
                return SpawnEntity("Prefabs/Items/ItemHealthPotion", ref buttonGO);
            case SpawnTypeItem.Pizza:
                return SpawnEntity("Prefabs/Items/ItemPizza", ref buttonGO);
            case SpawnTypeItem.Whetstone:
                return SpawnEntity("Prefabs/Items/ItemWhetstone", ref buttonGO);
            default:
                return SpawnEntity("Prefabs/Items/ItemPizza", ref buttonGO);

        }
    }
    public GameObject SpawnRandomWeapon(ref GameObject buttonGO) {
        SpawnTypeWeapon value = (SpawnTypeWeapon)Random.Range(1, (int)SpawnTypeWeapon.Length);
        switch(value) {
            case SpawnTypeWeapon.FireStaff:
                return SpawnEntity("Prefabs/Weapons/WeaponFireStaff", ref buttonGO);
            case SpawnTypeWeapon.SteelSword:
                return SpawnEntity("Prefabs/Weapons/WeaponSteelSword", ref buttonGO);
            case SpawnTypeWeapon.WoodenBow:
                return SpawnEntity("Prefabs/Weapons/WeaponWoodenBow", ref buttonGO);
            default:
                return SpawnEntity("Prefabs/Weapons/WeaponSteelSword", ref buttonGO);

        }
    }
    public GameObject SpawnRandomArmor(ref GameObject buttonGO) {
        SpawnTypeArmor value = (SpawnTypeArmor)Random.Range(1, (int)SpawnTypeArmor.Length);
        switch(value) {
            case SpawnTypeArmor.LeatherArmor:
                return SpawnEntity("Prefabs/Armors/ArmorLeatherArmor", ref buttonGO);
            case SpawnTypeArmor.SteelArmor:
                return SpawnEntity("Prefabs/Armors/ArmorSteelArmor", ref buttonGO);
            case SpawnTypeArmor.WizardRobe:
                return SpawnEntity("Prefabs/Armors/ArmorWizardRobe", ref buttonGO);
            default:
                return SpawnEntity("Prefabs/Armors/ArmorSteelArmor", ref buttonGO);

        }
    }
    public GameObject SpawnRandomHat(ref GameObject buttonGO) {
        SpawnTypeHat value = (SpawnTypeHat)Random.Range(1, (int)SpawnTypeHat.Length);
        switch(value) {
            case SpawnTypeHat.Beanie:
                return SpawnEntity("Prefabs/Hats/HatBeanie", ref buttonGO);
            case SpawnTypeHat.SteelHelmet:
                return SpawnEntity("Prefabs/Hats/HatSteelHelmet", ref buttonGO);
            case SpawnTypeHat.WizardHat:
                return SpawnEntity("Prefabs/Hats/HatWizardHat", ref buttonGO);
            case SpawnTypeHat.Cool:
                return SpawnEntity("Prefabs/Hats/HatCool", ref buttonGO);
            default:
                return SpawnEntity("Prefabs/Hats/HatSteelHelmet", ref buttonGO);

        }
    }
    public GameObject SpawnRandomAccessory(ref GameObject buttonGO) {
        SpawnTypeAccessory value = (SpawnTypeAccessory)Random.Range(1, (int)SpawnTypeAccessory.Length);
        switch(value) {
            case SpawnTypeAccessory.ChestKey:
                return SpawnEntity("Prefabs/Accessories/AccessoryChestKey", ref buttonGO);
            case SpawnTypeAccessory.FourLeafClover:
                return SpawnEntity("Prefabs/Accessories/AccessoryFourLeafClover", ref buttonGO);
            case SpawnTypeAccessory.GoldenRing:
                return SpawnEntity("Prefabs/Accessories/AccessoryGoldenRing", ref buttonGO);
            default:
                return SpawnEntity("Prefabs/Accessories/AccessoryFourLeafClover", ref buttonGO);

        }
    }
    public GameObject SpawnGold(ref GameObject buttonGO) {
        return SpawnEntity("Prefabs/Gold/GoldCoin", ref buttonGO);
    }

    public void UpdateGoldTilesIfThereAreAny() {
        Entity leftEntity = leftMidGO.GetComponent<Entity>();
        Entity rightEntity = rightMidGO.GetComponent<Entity>();
        Entity topEntity = midTopGO.GetComponent<Entity>();
        Entity botEntity = midBotGO.GetComponent<Entity>();

        if(leftEntity && leftEntity is Gold) {
            Player player = (GameObject.Find("Canvas/PanelCards/PanelPlayer/Player") as GameObject).GetComponent<Player>();
            Gold gold = ((Gold)leftMidGO.GetComponent<Gold>());
            gold.textWorth.text = gold.GetWorth(player.GetLuck()).ToString();
        }
        if(rightEntity && rightEntity is Gold) {
            Player player = (GameObject.Find("Canvas/PanelCards/PanelPlayer/Player") as GameObject).GetComponent<Player>();
            Gold gold = ((Gold)rightMidGO.GetComponent<Gold>());
            gold.textWorth.text = gold.GetWorth(player.GetLuck()).ToString();
        }
        if(topEntity && topEntity is Gold) {
            Player player = (GameObject.Find("Canvas/PanelCards/PanelPlayer/Player") as GameObject).GetComponent<Player>();
            Gold gold = ((Gold)midTopGO.GetComponent<Gold>());
            gold.textWorth.text = gold.GetWorth(player.GetLuck()).ToString();
        }
        if(botEntity && botEntity is Gold) {
            Player player = (GameObject.Find("Canvas/PanelCards/PanelPlayer/Player") as GameObject).GetComponent<Player>();
            Gold gold = ((Gold)midBotGO.GetComponent<Gold>());
            gold.textWorth.text = gold.GetWorth(player.GetLuck()).ToString();
        }
    }

    public void PlayEffect(Transform parent, Weapon weaponUsed) {
        if(weaponUsed) {
            GameObject effectGO = Instantiate(Resources.Load("Prefabs/Effects/" + weaponUsed.effectName), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            effectGO.transform.SetParent(parent, false);

            ParticleSystem effectGOPS = effectGO.GetComponent<ParticleSystem>();
            effectGOPS.Emit(1);
        }
    }

    public static Audio GetAudio() {
        return ((GameObject.Find("Audio") as GameObject).GetComponent<Audio>() as Audio);
    }

    protected static ObjectPooler GetInformationUIObjectPooler() {
        return ((GameObject.Find("InformationUI") as GameObject).GetComponent<ObjectPooler>() as ObjectPooler);
    }

    public enum DamageColor {
        None = 0,
        Damage,
        Heal,
        Gold,
        XP,
        Information
    }

    public static void SpawnDamage(int damage, DamageColor damageColor, GameObject parent) {
        SpawnDamage(damage.ToString(), damageColor, parent);
    }

    public static void SpawnDamage(string damage, DamageColor damageColor, GameObject parent) {
        GameObject damageGO = GetInformationUIObjectPooler().GetPooledObject("Damage");
        Text text = damageGO.transform.Find("Text").GetComponent<Text>();
        text.text = damage;

        switch(damageColor) {
            case DamageColor.Damage:
                text.text = "-" + damage;
                text.color = new Color32(255, 0, 0, 255);
                break;
            case DamageColor.Heal:
                text.text = "+" + damage;
                text.color = new Color32(0, 255, 0, 255);
                break;
            case DamageColor.Gold:
                text.text = "+" + damage;
                text.color = new Color32(255, 255, 0, 255);
                break;
            case DamageColor.XP:
                text.color = new Color32(127, 63, 255, 255);
                break;
            case DamageColor.Information:
                text.color = new Color32(255, 255, 255, 255);
                break;
            default:
                text.color = new Color32(255, 255, 255, 255);
                break;
        }

        int newX = Random.Range((int)parent.transform.position.x - 50, (int)parent.transform.position.x + 50);
        int newY = Random.Range((int)parent.transform.position.y - 50, (int)parent.transform.position.y + 50);
        damageGO.transform.position = new Vector2(newX, newY + 25);

        damageGO.SetActive(true);
    }
}