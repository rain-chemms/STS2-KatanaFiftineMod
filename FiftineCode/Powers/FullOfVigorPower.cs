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

public sealed class FullOfVigorPower : FiftinePower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
    public override string? CustomPackedIconPath => "res://Fiftine/Images/Powers/full_of_vigor_power.png";
    public override string? CustomBigIconPath => "res://Fiftine/Images/Powers/full_of_vigor_power.png";
    public override Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature applier, CardModel cardSource)
    {
        if (power == this && applier == Owner)
        {
            FullOfVigorNodePatch.GetCachedFullOfVigorNode().GetNodeOrNull<RichTextLabel>("FullOfVigorValue").Text = Amount.ToString();
        }
        return Task.CompletedTask;
    }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        await PowerCmd.Apply<VigorPower>(player.Creature,player.Creature.GetPowerAmount<FullOfVigorPower>(),null,null);
    }

}