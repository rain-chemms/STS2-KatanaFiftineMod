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

namespace KatanaZeroMod.Fiftine.Powers;

public sealed class ChronosPower : FiftinePower
{
    public override PowerType Type => PowerType.Buff;
    public override bool AllowNegative => true;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override string? CustomPackedIconPath => "res://Fiftine/Images/Powers/chronos_power.png";
    public override string? CustomBigIconPath => "res://Fiftine/Images/Powers/chronos_power.png";
    public override Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature applier, CardModel cardSource)
    {
        if (power == this && applier == Owner)
        {
            ChronosNodePatch.GetCachedChronosNode().GetNodeOrNull<RichTextLabel>("ChronosValue").Text = Amount.ToString();
        }
        return Task.CompletedTask;
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == base.Owner.Side && !base.Owner.IsDead)
        {
            Flash();
            if (base.Amount > 0)
            {
                await CreatureCmd.Heal(base.Owner, 1);
            }
            else
            {
                if (base.Amount < 0)
                    await CreatureCmd.Damage(choiceContext, base.Owner, Math.Abs(base.Amount), ValueProp.Unblockable | ValueProp.Unpowered, null, null);
                ModelId id = base.Owner.ModelId;//获取玩家的角色Id
            }
            base.Owner.GetPower<ChronosPower>().SetAmount(base.Owner.GetPowerAmount<ChronosPower>() - 1);
            //await PowerCmd.Apply<ChronosPower>(base.Owner, -1, null, null);
        }
    }

    ///*测试不受伤的状态效果
    //public override async Task BeforeDamageReceived(PlayerChoiceContext choiceContext, Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
    //{
    //    amount = 0;
    //}
    //*/

}