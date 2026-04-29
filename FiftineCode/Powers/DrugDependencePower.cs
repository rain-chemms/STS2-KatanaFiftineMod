using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using KatanaZeroMod.Fiftine.Cards;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using System.Threading.Tasks;

namespace KatanaZeroMod.Fiftine.Powers;

public sealed class DrugDependencePower : FiftinePower
{
    public override PowerType Type => PowerType.Buff;
    public override bool AllowNegative => false;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override string? CustomPackedIconPath => "res://Fiftine/Images/Powers/drug_dependence_power.png";
    public override string? CustomBigIconPath => "res://Fiftine/Images/Powers/drug_dependence_power.png";
    public override Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature applier, CardModel cardSource)
    {
        if (power == this && applier == Owner)
        {
            DrugDependenceNodePatch.GetCachedDrugDependenceNode().GetNodeOrNull<RichTextLabel>("DrugDependenceValue").Text = Amount.ToString();
        }
        return Task.CompletedTask;
    }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        await PowerCmd.Apply<VigorPower>(player.Creature, player.Creature.GetPowerAmount<ChronosPower>() * base.Amount, null, null);
    }
}