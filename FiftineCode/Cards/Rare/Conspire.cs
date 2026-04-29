using System;
using System.Collections.Generic;
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


namespace KatanaZeroMod.Fiftine.Cards;

public class Conspire() : FiftineCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    public override HashSet<CardKeyword> CanonicalKeywords => new HashSet<CardKeyword> {
        CardKeyword.Exhaust,
        CardKeyword.Retain
    };
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Defend];
    protected override IEnumerable<DynamicVar> CanonicalVars => [
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<VigorPower>()
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<VigorPower>(Owner.Creature, Owner.Creature.GetPowerAmount<VigorPower>(), null, this);
    }

    protected override void OnUpgrade()
    {
        //CanonicalKeywords.Remove(CardKeyword.Exhaust);
        this.MockSetEnergyCost(new CardEnergyCost(this, 0, false));
    }
}
