using Godot;
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

//能力:治愈性药物
public sealed class TreatmentPower : FiftinePower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
    public override string? CustomPackedIconPath => "res://Fiftine/Images/Powers/treatment_power.png";
    public override string? CustomBigIconPath => "res://Fiftine/Images/Powers/treatment_power.png";
    public override Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature applier, CardModel cardSource)
    {
        if (power == this && applier == Owner)
        {
            TreatmentNodePatch.GetCachedTreatmentNode().GetNodeOrNull<RichTextLabel>("TreatmentValue").Text = Amount.ToString();
        }
        return Task.CompletedTask;
    }

    public override Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        player.Creature.GetPower<ChronosPower>().SetAmount(player.Creature.GetPowerAmount<ChronosPower>()+player.Creature.GetPowerAmount<TreatmentPower>());
        return Task.CompletedTask;
    }
}

