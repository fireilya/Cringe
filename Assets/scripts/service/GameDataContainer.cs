using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDataContainer
{
    public static int BossState;
    public static int PlayerCurrentLife;
    public static void PackContainer(int state, int currentLife)
    {
        BossState = state;
        PlayerCurrentLife = currentLife;
    }

    public static void ResetContainer()
    {
        BossState=0; 
        PlayerCurrentLife=0;
    }
}
