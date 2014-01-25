using UnityEngine;
using System.Collections;

public class Utility {

    public static bool BitIsSet (int val, int bit)
    {
        return (1 & (val >> bit)) == 1;
    }

}
