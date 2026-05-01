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
public static class VigorEmbraceNodePatch//柯罗诺斯节点补丁
{
    private static Control _cachedVigorEmbraceNode;
    public static void Postfix(NCombatUi __instance)
    {
        var old = __instance.GetNodeOrNull<Control>("VigorEmbraceNode");
        if (old == null)
        {
            _cachedVigorEmbraceNode = GD.Load<PackedScene>("res://Fiftine/Scenes/VigorEmbraceNode.tscn").Instantiate<Control>();
            _cachedVigorEmbraceNode.Name = "VigorEmbraceNode";
            //__instance.EnergyCounterContainer.AddChild(_cachedVigorEmbraceNode, false);
        }
    }
    public static Control GetCachedVigorEmbraceNode() => _cachedVigorEmbraceNode;
}

[HarmonyPatch(typeof(NCombatUi), "Activate")]
public static class VigorEmbraceNodeActivatePatch
{
    public static void Postfix(CombatState state)
    {
        Player player = LocalContext.GetMe(state);
        Log.Info(">>>[ManboMod]Character.Id is " + player.Character.Id.ToString());
        if (player.Character.Id.ToString() == "CHARACTER.KATANAZEROMOD-FIFTINE")
        {
            VigorEmbraceNodePatch.GetCachedVigorEmbraceNode().Visible = true;
        }
    }
}