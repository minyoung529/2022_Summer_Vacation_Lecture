using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    public abstract void Execute(Animator anim, bool forward);
}

// 아무것도 안 함, 전방이동, 점프, 킥, 펀치

public class PerformJump : Command
{
    public override void Execute(Animator anim, bool forward)
    {
        anim.SetTrigger(string.Format("isJumping{0}", forward ? "" : "R"));
    }
}

public class MoveForward : Command
{
    public override void Execute(Animator anim, bool forward)
    {
        anim.SetTrigger(string.Format("isWalking{0}", forward ? "" : "R"));
    }
}

public class PerformKick : Command
{
    public override void Execute(Animator anim, bool forward)
    {
        anim.SetTrigger(string.Format("isKicking{0}", forward ? "" : "R"));
    }
}

public class PerformPunch : Command
{
    public override void Execute(Animator anim, bool forward)
    {
        anim.SetTrigger(string.Format("isPunching{0}", forward ? "" : "R"));
    }
}

public class DoNothing : Command
{
    public override void Execute(Animator anim, bool forward)
    {
    }
}