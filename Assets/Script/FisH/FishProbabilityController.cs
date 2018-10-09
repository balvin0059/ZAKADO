using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class FishProbabilityController
{
    public static int ChoiceRandom(FishSet[] fishSets)
    {
        Array.Sort(fishSets, (x, y) => -x.CompareTo(y));
        var array = new float[fishSets.Length];

        for (int index = 0; index < fishSets.Length; index++)
            array[index] = fishSets[index].Percent;

        var choice = GetRandom(array);

        return choice;
    }

    public static int GetRandom(float[] probability)
    {
        float total = 0;

        for (int index = 0; index < probability.Length; index++)
            total += probability[index];

        if (total > 1)
            throw new Exception("Overall probability is greater than 1");

        var randomPoint = Random.value * total;

        for (int i = 0; i < probability.Length; i++)
        {
            if (randomPoint <= probability[i])
                return i;

            randomPoint -= probability[i];

        }

        return probability.Length - 1;
    }
}
