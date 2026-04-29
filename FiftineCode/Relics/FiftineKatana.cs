using KatanaZeroMod.Fiftine.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatanaZeroMod.Fiftine.Relics;

public class FiftineKatana : FiftineRelics
{
	public override RelicRarity Rarity => RelicRarity.Starter;

    public override async Task BeforeCombatStart()
    {
        await PowerCmd.Apply<StrengthPower>(Owner.Creature, 3m, Owner.Creature, null);//PowerCmd.Apply<RellyPower>(Owner.Creature, 2m, Owner.Creature, null);
    }

    public override RelicModel? GetUpgradeReplacement() => ModelDb.Relic<ShortKnife>(); // 实现方法。自己更改类型。
}
