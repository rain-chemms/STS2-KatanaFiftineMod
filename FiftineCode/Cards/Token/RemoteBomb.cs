using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KatanaZeroMod.Fiftine.Cards;

//遥控炸弹
public class RemoteBomb() : FiftineCard(3,
    CardType.Attack, CardRarity.Token,
    TargetType.AllEnemies)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTag.None];
    public override HashSet<CardKeyword> CanonicalKeywords => new HashSet<CardKeyword> {
        CardKeyword.Exhaust,
        CardKeyword.Retain
    };
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(10,ValueProp.Move)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromKeyword(CardKeyword.Exhaust),
        HoverTipFactory.FromKeyword(CardKeyword.Retain)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardAttack(this, cardPlay.Target).Execute(choiceContext);
    }

    public override Task OnTurnEndInHand(PlayerChoiceContext choiceContext)
    {
        DynamicVars.Damage.UpgradeValueBy(DynamicVars.Damage.ToUInt32(null));
        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        this.MockSetEnergyCost(new CardEnergyCost(this, 2, false));
    }
}
