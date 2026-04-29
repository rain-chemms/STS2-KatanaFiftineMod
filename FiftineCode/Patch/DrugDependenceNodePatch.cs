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
public static class DrugDependenceNodePatch//柯罗诺斯节点补丁
{
    private static Control _cachedDrugDependenceNode;
    public static void Postfix(NCombatUi __instance)
    {
        var old = __instance.GetNodeOrNull<Control>("DrugDependenceNode");
        if (old == null)
        {
            _cachedDrugDependenceNode = GD.Load<PackedScene>("res://Fiftine/Scenes/DrugDependenceNode.tscn").Instantiate<Control>();
            _cachedDrugDependenceNode.Name = "DrugDependenceNode";
            //__instance.EnergyCounterContainer.AddChild(_cachedDrugDependenceNode, false);
        }
    }
    public static Control GetCachedDrugDependenceNode() => _cachedDrugDependenceNode;
}

[HarmonyPatch(typeof(NCombatUi), "Activate")]
public static class DrugDependenceNodeActivatePatch
{
    public static void Postfix(CombatState state)
    {
        Player player = LocalContext.GetMe(state);
        Log.Info(">>>[KatanaFiftineMod]Character.Id is " + player.Character.Id.ToString());
        if (player.Character.Id.ToString() == "CHARACTER.KATANAZEROMOD-FIFTINE")
        {
            DrugDependenceNodePatch.GetCachedDrugDependenceNode().Visible = true;
        }
    }
}