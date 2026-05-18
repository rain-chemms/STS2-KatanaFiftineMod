using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using System.Diagnostics;
using System.Threading.Tasks;

namespace KatanaZeroMod.Fiftine.Powers;

//能力:活力之拥,获得活力时抽一张牌
public sealed class VigorEmbracePower : FiftinePower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
    public override string? CustomPackedIconPath => "res://Fiftine/Images/Powers/vigor_embrace_power.png";
    public override string? CustomBigIconPath => "res://Fiftine/Images/Powers/vigor_embrace_power.png";
    public override Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature applier, CardModel cardSource)
    {
        if (power == this && applier == Owner)
        {
            VigorEmbraceNodePatch.GetCachedVigorEmbraceNode().GetNodeOrNull<RichTextLabel>("VigorEmbraceValue").Text = Amount.ToString();
        }
        if (power is VigorPower) 
        {
            CardPileCmd.Draw(null, base.Amount, base.Owner.Player);
        }
        return Task.CompletedTask;
    }
}

