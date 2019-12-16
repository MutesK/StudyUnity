using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty : MonoBehaviour
{
    public float MaxWalkSpeed = 4.5f;
    public float DecreaseWalkSpeed = 0.001f;
    public float JumpForce = 16;

    public float AnimTriggerRangeWalk;
    public float AnimTriggerRangeJump;

    public int Hp = 3;
}
