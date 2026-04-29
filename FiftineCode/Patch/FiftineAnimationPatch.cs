using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.RestSite;
using KatanaZeroMod.Fiftine.Character;
using static Godot.Node;
using static Godot.PackedScene;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.DevConsole.ConsoleCommands;
using MegaCrit.Sts2.Core.Models.Badges;


[HarmonyPatch(typeof(NCreature), "StartDeathAnim")]
public static class FiftineAnimationPatch1
{
    public static void Postfix(NCreature __instance)
    {
        if (__instance.Entity == null || !__instance.Entity.IsPlayer)
            return;

        if (__instance.Entity.ModelId.ToString() == "CHARACTER.KATANAZEROMOD-FIFTINE")
            Log.Info("[>>>KatanaFiftineMode AnimeTrigger(PostFix)] StartDeathAnim ");
        FAPFunc.PlayAnim(__instance, "die", false);
    }
}

[HarmonyPatch(typeof(NCreature), "SetAnimationTrigger")]
public static class FiftineAnimationPatch2
{
    public static void Postfix(NCreature __instance, string trigger)
	{

		if (__instance.Entity == null || !__instance.Entity.IsPlayer)
			return;

		if (__instance.Entity.ModelId.ToString() == "CHARACTER.KATANAZEROMOD-FIFTINE")
			Log.Info("[>>>KatanaFiftineMode AnimeTrigger(PostFix)]" + trigger);
		switch (trigger)
		{
            case "Hit":
                FAPFunc.PlayAnim(__instance, "hurt", false);
				break;

			case "Attack":
                FAPFunc.PlayAnim(__instance, "attack", false);
				break;

			case "Cast":
                FAPFunc.PlayAnim(__instance, "cast", true);
				break;

			case "Dead":
				FAPFunc.PlayAnim(__instance, "die", false);
				break;

			default:
				FAPFunc.PlayAnim(__instance, "idle_loop", false);
				break;
		}
	}
    /*
    public static void Perfix(NCreature __instance, string trigger)
    {
        if (__instance.Entity == null || !__instance.Entity.IsPlayer)
            return;

        if (__instance.Entity.ModelId.ToString() == "CHARACTER.KATANAZEROMOD-FIFTINE")
            Log.Info("[>>>KatanaFiftineMode AnimeTrigger(PreFix)]" + trigger);
        switch (trigger)
        {
            case "Hit":
                PlayAnim(__instance, "hurt", false);
                break;

            case "Attack":
                PlayAnim(__instance, "attack", false);
                break;

            case "Cast":
                PlayAnim(__instance, "cast", true);
                break;

            case "Dead":
                PlayAnim(__instance, "die", false);
                break;

            default:
                PlayAnim(__instance, "idle_loop", false);
                break;
        }
    }
    */
    
}

public class FAPFunc
{
    public static void PlayAnim(NCreature node, string animName, bool fromEnd)
    {
        var visual = node.GetNodeOrNull<Node2D>("Fiftine");
        if (visual == null) return;

        var anim = visual.GetNodeOrNull<AnimatedSprite2D>("Visuals");
        if (anim == null) return;

        // 切换动画
        anim.Frame = 0;
        anim.Play(animName, 1f, fromEnd);

        if (animName != "die")
        {
            // 动画结束后回到 Idle
            anim.Connect("animation_finished", Callable.From(() =>
            {
                anim.Play("idle_loop");
            }), 4u);
        }
    }
}