using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BaseLib.Utils;
using KatanaZeroMod.Fiftine.Character;
using KatanaZeroMod.Fiftine.Extensions;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Potions;

using KatanaZeroMod.Fiftine.Powers;
using MegaCrit.Sts2.Core.Models.Powers;
using BaseLib.Extensions;
using Godot;
using KatanaZeroMod.Fiftine.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;

namespace KatanaZeroMod.Fiftine.Potions;

public abstract class VigorPotion : FiftinePotion
{
    // 稀有度
    public override PotionRarity Rarity => PotionRarity.Common;

    // 使用方式，CombatOnly表示只能在战斗中使用。
    public override PotionUsage Usage => PotionUsage.CombatOnly;

    // 目标类型
    public override TargetType TargetType => TargetType.AnyPlayer;

    // 定义动态变量
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<VigorPower>(9)
    ];

    // 这里显示预览卡牌灵魂。或者你可以添加提示关键词
    public override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<VigorPower>()
    ];

    // 药水图片。不一定svg，只要最终能变成Texture的格式就行。
    public override string? CustomPackedImagePath =>"viogr_potion.svg".PotionImagePath();
    public override string? CustomPackedOutlinePath => "vigor_potion_outline.svg".PotionImagePath();

    // 打出时的效果逻辑:给予目标9点活力

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        await CommonActions.Apply<VigorPower>(target,null,DynamicVars.Power<VigorPower>().IntValue);
    }
}