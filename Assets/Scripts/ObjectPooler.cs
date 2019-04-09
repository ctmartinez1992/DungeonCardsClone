﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ObjectPoolItem {
    public GameObject objectToPool;
    public int amountToPool;
    public bool shouldExpand;
}

public class ObjectPooler : MonoBehaviour {
    public static ObjectPooler SharedInstance;
    public List<ObjectPoolItem> itemsToPool;
    public List<GameObject> pooledObjects;

    public Transform parent;

    void Awake() {
        SharedInstance = this;
    }
    
    void Start() {
        pooledObjects = new List<GameObject>();

        foreach(ObjectPoolItem item in itemsToPool) {
            for(int i = 0; i < item.amountToPool; i++) {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.transform.SetParent(parent, false);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(string tag) {
        for(int i = 0; i < pooledObjects.Count; i++) {
            if(!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag) {
                return pooledObjects[i];
            }
        }

        foreach(ObjectPoolItem item in itemsToPool) {
            if(item.objectToPool.tag == tag) {
                if(item.shouldExpand) {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.transform.SetParent(parent, false);
                    obj.SetActive(false);

                    pooledObjects.Add(obj);

                    return obj;
                }
            }
        }

        return null;
    }
}