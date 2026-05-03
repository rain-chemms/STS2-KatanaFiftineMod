using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using KatanaZeroMod.Fiftine;
using KatanaZeroMod.Fiftine.DynamicVars;
using KatanaZeroMod.Fiftine.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Events;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Multiplayer.Messages.Lobby;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.GameInfo.Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace KatanaZeroMod.Fiftine.Cards;

//切换一个敌人的意图
public class SmokeBomb() : FiftineCard(0,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTag.None];
    protected override IEnumerable<DynamicVar> CanonicalVars => [
    ];
    public override HashSet<CardKeyword> CanonicalKeywords => new HashSet<CardKeyword> {
        CardKeyword.Exhaust
    };

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        MonsterModel monster = cardPlay.Target.Monster;
        if (monster != null)
        {
            //获取意图列表
            //Dictionary<string, MonsterState> moveStateDict = monster.MoveStateMachine.States;
            //产生随机意图
            //List<MonsterState> moveStateList = monster.MoveStateMachine.StateLog;
            //moveStateList = new List<MonsterState>(moveStateDict.Values);
            //按下面这种方式切换意图不会触发切换意图生效的效果
            MoveState targetState = null;
            MonsterState nowMonsterState = monster.MoveStateMachine.RollMove(null,cardPlay.Target,null);
            string nextStateName = nowMonsterState.GetNextState(cardPlay.Target,null);
            MonsterState targetMonsterState = monster.MoveStateMachine.States[nextStateName];
            //触发效果的修改怪物意图
            //monster.MoveStateMachine.ForceCurrentState(targetMonsterState);
            if (targetMonsterState!=null && targetMonsterState.IsMove)
            {
                targetState = targetMonsterState as MoveState;
                monster.SetMoveImmediate(targetState);
            }
        }
    }

    protected override void OnUpgrade()
    {
        RemoveKeyword(CardKeyword.Exhaust);
    }
}

