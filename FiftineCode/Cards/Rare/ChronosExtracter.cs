using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using KatanaZeroMod.Fiftine.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Models.Monsters.Mocks;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KatanaZeroMod.Fiftine.Cards;

//Chronos提取器:8伤害,每攻击一个敌人获得3层Chronos,消耗,升级后不消耗
public class ChronosExtracter() : FiftineCard(1,
    CardType.Attack, CardRarity.Rare,
    TargetType.AllEnemies)
{
    //添加消耗关键字
    public override HashSet<CardKeyword> CanonicalKeywords => new HashSet<CardKeyword> {
        CardKeyword.Exhaust
    };
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(8, ValueProp.Move),
        new PowerVar<ChronosPower>(3)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<ChronosPower>(),
        HoverTipFactory.FromKeyword(CardKeyword.Exhaust)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int enermyCount = CombatState.Enemies.Count;
        await CommonActions.CardAttack(this, cardPlay.Target).Execute(choiceContext);
        for (int i = 0; i < enermyCount; i++)
        {
            await CommonActions.Apply<ChronosPower>(Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        CanonicalKeywords.Remove(CardKeyword.Exhaust);
    }
}
