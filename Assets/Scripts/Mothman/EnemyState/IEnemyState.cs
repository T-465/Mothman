using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void Enter(Mothman moth); // Called when entering the state

    void Update(Mothman moth); // Called every frame in this state
    void Exit(Mothman moth); // Called when exiting the state
}
