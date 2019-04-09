using UnityEngine.UI;
using System.Collections;

public class HatWizardHat : Hat {

    public int intelligence;

    public Text textIntelligence;

    private void Start() {
        textIntelligence.text = intelligence.ToString();
    }

    public override bool Use() {
        return false;
    }
}
