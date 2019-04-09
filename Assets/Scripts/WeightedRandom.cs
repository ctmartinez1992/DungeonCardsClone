using System.Collections.Generic;
using System;

public static class WeightedRandomizer {
    public static WeightedRandomizer<R> From<R>(Dictionary<R, int> spawnRate) {
        return new WeightedRandomizer<R>(spawnRate);
    }
}

public class WeightedRandomizer<T> {
    private static Random _random = new Random();
    private Dictionary<T, int> _weights;
    
    public WeightedRandomizer(Dictionary<T, int> weights) {
        _weights = weights;
    }

    //Randomizes one item
    //spawnRate - An ordered list withe the current spawn rates. The list will be updated so that selected items will have a smaller chance of being repeated.</param>
    //Returns the randomized item.
    public T TakeOne() {
        var sortedSpawnRate = Sort(_weights);
        
        int sum = 0;
        foreach(var spawn in _weights) {
            sum += spawn.Value;
        }
        
        int roll = _random.Next(0, sum);

        //Finds chosen item based on spawn rate.
        T selected = sortedSpawnRate[sortedSpawnRate.Count - 1].Key;
        foreach(var spawn in sortedSpawnRate) {
            if(roll < spawn.Value) {
                selected = spawn.Key;
                break;
            }

            roll -= spawn.Value;
        }
        
        return selected;
    }

    private List<KeyValuePair<T, int>> Sort(Dictionary<T, int> weights) {
        var list = new List<KeyValuePair<T, int>>(weights);

        //Sorts the Spawn Rate List for randomization later.
        list.Sort(delegate(KeyValuePair<T, int> firstPair, KeyValuePair<T, int> nextPair) {
            return firstPair.Value.CompareTo(nextPair.Value);
        });

        return list;
    }
}