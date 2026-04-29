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
public static class TreatmentNodePatch//柯罗诺斯节点补丁
{
    private static Control _cachedTreatmentNode;
    public static void Postfix(NCombatUi __instance)
    {
        var old = __instance.GetNodeOrNull<Control>("TreatmentNode");
        if (old == null)
        {
            _cachedTreatmentNode = GD.Load<PackedScene>("res://Fiftine/Scenes/TreatmentNode.tscn").Instantiate<Control>();
            _cachedTreatmentNode.Name = "TreatmentNode";
            //__instance.EnergyCounterContainer.AddChild(_cachedTreatmentNode, false);
        }
    }
    public static Control GetCachedTreatmentNode() => _cachedTreatmentNode;
}

[HarmonyPatch(typeof(NCombatUi), "Activate")]
public static class TreatmentNodeActivatePatch
{
    public static void Postfix(CombatState state)
    {
        Player player = LocalContext.GetMe(state);
        Log.Info(">>>[ManboMod]Character.Id is " + player.Character.Id.ToString());
        if (player.Character.Id.ToString() == "CHARACTER.KATANAZEROMOD-FIFTINE")
        {
            TreatmentNodePatch.GetCachedTreatmentNode().Visible = true;
        }
    }
}