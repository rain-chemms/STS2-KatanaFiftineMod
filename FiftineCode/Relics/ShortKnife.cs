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

//短刀:古代品质,战斗开始获得3层力量和3层敏捷,由武士刀经欧巴洛斯之触摸升级获得
public class ShortKnife : FiftineRelics
{
    public override RelicRarity Rarity => RelicRarity.Starter;

    public override async Task BeforeCombatStart()
    {
        await PowerCmd.Apply<StrengthPower>(Owner.Creature, 3m, Owner.Creature, null);//PowerCmd.Apply<RellyPower>(Owner.Creature, 2m, Owner.Creature, null);
        await PowerCmd.Apply<DexterityPower>(Owner.Creature, 3m, Owner.Creature, null);//PowerCmd.Apply<RellyPower>(Owner.Creature, 2m, Owner.Creature, null);
    }
}
