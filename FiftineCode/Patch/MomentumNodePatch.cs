using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Nodes.Combat;
using System;
using System.Reflection.Metadata;

[HarmonyPatch(typeof(NCombatUi), "_Ready")]
public static class MomentumNodePatch//柯罗诺斯节点补丁
{
    private static Control _cachedMomentumNode;
    public static void Postfix(NCombatUi __instance)
    {
        var old = __instance.GetNodeOrNull<Control>("MomentumNode");
        if (old == null)
        {
            _cachedMomentumNode = GD.Load<PackedScene>("res://Fiftine/Scenes/MomentumNode.tscn").Instantiate<Control>();
            _cachedMomentumNode.Name = "MomentumNode";
            //__instance.EnergyCounterContainer.AddChild(_cachedMomentumNode, false);
        }
    }
    public static Control GetCachedMomentumNode() => _cachedMomentumNode;
}

[HarmonyPatch(typeof(NCombatUi), "Activate")]
public static class MomentumNodeActivatePatch
{
    public static void Postfix(CombatState state)
    {
        Player player = LocalContext.GetMe(state);
        Log.Info(">>>[KatanaFiftineMod]Character.Id is " + player.Character.Id.ToString());
        if (player.Character.Id.ToString() == "CHARACTER.KATANAZEROMOD-FIFTINE")
        {
            MomentumNodePatch.GetCachedMomentumNode().Visible = true;
        }
    }
}