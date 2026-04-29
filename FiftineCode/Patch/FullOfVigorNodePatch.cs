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
public static class FullOfVigorNodePatch//柯罗诺斯节点补丁
{
    private static Control _cachedFullOfVigorNode;
    public static void Postfix(NCombatUi __instance)
    {
        var old = __instance.GetNodeOrNull<Control>("FullOfVigorNode");
        if (old == null)
        {
            _cachedFullOfVigorNode = GD.Load<PackedScene>("res://Fiftine/Scenes/FullOfVigorNode.tscn").Instantiate<Control>();
            _cachedFullOfVigorNode.Name = "FullOfVigorNode";
            //__instance.EnergyCounterContainer.AddChild(_cachedChronosNode, false);
        }
    }
    public static Control GetCachedFullOfVigorNode() => _cachedFullOfVigorNode;
}

[HarmonyPatch(typeof(NCombatUi), "Activate")]
public static class FullOfVigorNodeActivatePatch
{
    public static void Postfix(CombatState state)
    {
        Player player = LocalContext.GetMe(state);
        Log.Info(">>>[KatanaFiftineMod]Character.Id is " + player.Character.Id.ToString());
        if (player.Character.Id.ToString() == "CHARACTER.KATANAZEROMOD-FIFTINE")
        {
            FullOfVigorNodePatch.GetCachedFullOfVigorNode().Visible = true;
        }
    }
}