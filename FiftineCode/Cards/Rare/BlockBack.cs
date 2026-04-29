using BaseLib.Extensions;
using BaseLib.Utils;
using KatanaZeroMod.Fiftine.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KatanaZeroMod.Fiftine.Cards;

public class BlockBack() : FiftineCard(2,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Defend];
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<BlockBackPower>(1)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<BlockBackPower>()
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.ApplySelf<BlockBackPower>(this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<BlockBackPower>().UpgradeValueBy(1m);
    }
}
