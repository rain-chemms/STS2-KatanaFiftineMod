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
public static class ShadeFormNodePatch//柯罗诺斯节点补丁
{
    private static Control _cachedShadeFormNode;
    public static void Postfix(NCombatUi __instance)
    {
        var old = __instance.GetNodeOrNull<Control>("ShadeFormNode");
        if (old == null)
        {
            _cachedShadeFormNode = GD.Load<PackedScene>("res://Fiftine/Scenes/ShadeFormNode.tscn").Instantiate<Control>();
            _cachedShadeFormNode.Name = "ShadeFormNode";
            //__instance.EnergyCounterContainer.AddChild(_cachedChronosNode, false);
        }
    }
    public static Control GetCachedShadeFormNode() => _cachedShadeFormNode;
}

[HarmonyPatch(typeof(NCombatUi), "Activate")]
public static class ShadeFormNodeActivatePatch
{
    public static void Postfix(CombatState state)
    {
        Player player = LocalContext.GetMe(state);
        Log.Info(">>>[KatanaFiftineMod]Character.Id is " + player.Character.Id.ToString());
        if (player.Character.Id.ToString() == "CHARACTER.KATANAZEROMOD-FIFTINE")
        {
            ShadeFormNodePatch.GetCachedShadeFormNode().Visible = true;
        }
    }
}