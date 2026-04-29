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
using MegaCrit.Sts2.Core.Logging;

namespace KatanaZeroMod.Fiftine.Powers;

//下次受到的为0,并将伤害值反弹给所有敌人
public sealed class BlockBackPower : FiftinePower
{
    public override PowerType Type => PowerType.Buff;
    public override bool AllowNegative => false;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override string? CustomPackedIconPath => "res://Fiftine/Images/Powers/block_back_power.png";
    public override string? CustomBigIconPath => "res://Fiftine/Images/Powers/block_back_power.png";
    public override Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature applier, CardModel cardSource)
    {
        if (power == this && applier == Owner)
        {
            BlockBackNodePatch.GetCachedBlockBackNode().GetNodeOrNull<RichTextLabel>("BlockBackValue").Text = Amount.ToString();
        }
        return Task.CompletedTask;
    }

    public override decimal ModifyHpLostAfterOstyLate(Creature target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (target != base.Owner)
        {
            return amount;
        }
        return 0m;
    }

    public override async Task AfterModifyingHpLostAfterOsty()
    {
        await PowerCmd.Decrement(this);
        await PowerCmd.Apply<DexterityPower>(base.Owner, 1m, null, null);
    }
}