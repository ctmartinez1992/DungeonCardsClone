using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivatable : MonoBehaviour {
    public void DeactivateThis() {
        gameObject.SetActive(false);
    }
}
