using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAim
{
    public abstract void EnterState(PlayerAttack aim);
    public abstract void UpdateState(PlayerAttack aim);
}

public class Aim : BaseAim
{
    public override void EnterState(PlayerAttack aim)
    {
        // aim.anim.SetBool("Aiming", true);
        aim.currentFov = aim.adsFov;
    }

    public override void UpdateState(PlayerAttack aim)
    {
        if (Input.GetKeyUp(KeyCode.Mouse1)) { 
            aim.SwitchState(aim.hip); 
        }
    }
}

public class Fire : BaseAim
{
    public override void EnterState(PlayerAttack aim)
    {
        aim.anim.SetBool("Aiming", false);
        aim.currentFov = aim.hipFov;
    }

    public override void UpdateState(PlayerAttack aim)
    {
        if (Input.GetKey(KeyCode.Mouse1)) { 
            aim.SwitchState(aim.aim); 
        }
    }
}