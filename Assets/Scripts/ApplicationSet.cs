using UnityEngine;
using System.Collections;

public class ApplicationSet : MonoBehaviour {

    private void Awake() {
        Application.targetFrameRate = 60;
    }
}
