using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFadeAnimationEnd : MonoBehaviour {

    public void FadeInAnimationOver() {
        Dungeon dungeon = (GameObject.Find("Dungeon") as GameObject).GetComponent<Dungeon>();
        Debug.Log(gameObject.name + "  Resume  FadeIn");
        dungeon.Resume();

        GetComponent<Animator>().SetBool("FadeIn", false);
    }

    public void FadeOutAnimationOver() {
        Dungeon dungeon = (GameObject.Find("Dungeon") as GameObject).GetComponent<Dungeon>();
        Debug.Log(gameObject.name + "  Resume  FadeOut");
        dungeon.Resume();

        GetComponent<Animator>().SetBool("FadeOut", false);
    }

    public void FadeOutAndDestroyAnimationOver() {
        GameObject childToDestroy = transform.GetChild(0).gameObject;
        Destroy(childToDestroy);

        Dungeon dungeon = (GameObject.Find("Dungeon") as GameObject).GetComponent<Dungeon>();
        Debug.Log(gameObject.name + "  Resume  FadeOutAndDestroy");
        dungeon.Resume();

        GetComponent<Animator>().SetBool("FadeOutAndDestroy", false);
    }
}
