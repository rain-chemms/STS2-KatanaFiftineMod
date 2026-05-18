using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Threading.Tasks;
using KatanaZeroMod.Fiftine.Character;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace KatanaZeroMod.Fiftine.Powers;

//动量:每次攻击敌人时获得活力
public sealed class MomentumPower : FiftinePower
{
    public override PowerType Type => PowerType.Buff;
    public override bool AllowNegative => false;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override string? CustomPackedIconPath => "res://Fiftine/Images/Powers/momentum_power.png";
    public override string? CustomBigIconPath => "res://Fiftine/Images/Powers/momentum_power.png";
    public override Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature applier, CardModel cardSource)
    {
        if (power == this && applier == Owner)
        {
            MomentumNodePatch.GetCachedMomentumNode().GetNodeOrNull<RichTextLabel>("MomentumValue").Text = Amount.ToString();
        }
        return Task.CompletedTask;
    }

    public override async Task AfterCardPlayedLate(PlayerChoiceContext choiceContext, CardPlay cardPlay)   
    {
        await PowerCmd.Apply<VigorPower>(base.Owner, base.Amount,null,null);
    }
}