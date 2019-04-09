using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArmorWizardRobe : Armor {
    
    public int intelligence;
    
    public Text textIntelligence;

    private void Start() {
        textBlockDamage.text = blockDamage.ToString();
        textIntelligence.text = intelligence.ToString();
    }

    public override bool Use() {
        return false;
    }
}