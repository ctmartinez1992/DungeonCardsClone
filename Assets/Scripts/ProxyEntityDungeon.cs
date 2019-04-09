using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxyEntityDungeon : MonoBehaviour {
    public void StartTurnTop() {
        Dungeon dungeon = (GameObject.Find("Dungeon") as GameObject).GetComponent<Dungeon>();
        dungeon.PassTurnTop();
    }
    public void StartTurnRight() {
        Dungeon dungeon = (GameObject.Find("Dungeon") as GameObject).GetComponent<Dungeon>();
        dungeon.PassTurnRight();
    }
    public void StartTurnBot() {
        Dungeon dungeon = (GameObject.Find("Dungeon") as GameObject).GetComponent<Dungeon>();
        dungeon.PassTurnBot();
    }
    public void EndTurn() {
        Dungeon dungeon = (GameObject.Find("Dungeon") as GameObject).GetComponent<Dungeon>();
    }

    public void AttackPlayer() {
        ((Enemy)transform.GetChild(0).GetComponent<Enemy>()).AttackPlayer();
    }
}
