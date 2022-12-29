using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeeleBaseStates
{
    public abstract void EnterState(MeeleStateManager meele);
    public abstract void UpdateState(MeeleStateManager meele);
}
