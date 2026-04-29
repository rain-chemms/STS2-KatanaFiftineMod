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
public static class ChronosNodePatch//柯罗诺斯节点补丁
{
    private static Control _cachedChronosNode;
    public static void Postfix(NCombatUi __instance)
    {
        var old = __instance.GetNodeOrNull<Control>("ChronosNode");
        if (old == null)
        {
            _cachedChronosNode = GD.Load<PackedScene>("res://Fiftine/Scenes/ChronosNode.tscn").Instantiate<Control>();
            _cachedChronosNode.Name = "ChronosNode";
            //__instance.EnergyCounterContainer.AddChild(_cachedChronosNode, false);
        }
    }
    public static Control GetCachedChronosNode() => _cachedChronosNode;
}

[HarmonyPatch(typeof(NCombatUi), "Activate")]
public static class ChronosNodeActivatePatch
{
    public static void Postfix(CombatState state)
    {
        Player player = LocalContext.GetMe(state);
        Log.Info(">>>[KatanaFiftineMod]Character.Id is " + player.Character.Id.ToString());
        if (player.Character.Id.ToString() == "CHARACTER.KATANAZEROMOD-FIFTINE")
        {
            ChronosNodePatch.GetCachedChronosNode().Visible = true;
        }
    }
}