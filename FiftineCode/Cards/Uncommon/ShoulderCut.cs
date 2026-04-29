using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using KatanaZeroMod.Fiftine;
using KatanaZeroMod.Fiftine.DynamicVars;
using KatanaZeroMod.Fiftine.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.GameInfo.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands.Builders;


namespace KatanaZeroMod.Fiftine.Cards;

//断臂:造成10伤害,提供10点防御,时停2,时停:同时给予目标1层虚弱,升级后两层虚弱,15防御15伤
public class ShoulderCut() : FiftineCard(2,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(10, ValueProp.Move),
        new BlockVar(10,ValueProp.Move),
        new PowerVar<WeakPower>(1),
        new TimeHaltVar(2),
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<WeakPower>(),
        HoverTipFactory.FromPower<ChronosPower>()
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        //先防再打
        await CommonActions.CardBlock(this, cardPlay);
        await CommonActions.CardAttack(this, cardPlay.Target).Execute(choiceContext);
        if (DynamicVars.TryGetValue("FIFTINE-TIMEHALT", out DynamicVar fth))
        {
            int timeHaltNeedChronos = fth.IntValue;//时间效果发动所需要的柯罗诺斯层数
            if (Owner.HasPower<ChronosPower>())
            {
                int nowChronos = Owner.Creature.GetPowerAmount<ChronosPower>();
                if (nowChronos >= timeHaltNeedChronos)
                {
                    await CommonActions.Apply<WeakPower>(cardPlay.Target, this);//给予敌人虚弱
                    Owner.Creature.GetPower<ChronosPower>().SetAmount(nowChronos - timeHaltNeedChronos);//扣除相应的柯罗诺斯层数
                }
            }
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(5m);
        DynamicVars.Block.UpgradeValueBy(5m);
        DynamicVars.Power<WeakPower>().UpgradeValueBy(1m);
    }
}
