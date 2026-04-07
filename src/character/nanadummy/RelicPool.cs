using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Timeline.Epochs;
using MegaCrit.Sts2.Core.Unlocks;
using SlayTheNANA;
using System.Collections.Generic;
using System.Linq;

namespace MegaCrit.Sts2.Core.Models.CardPools;

public sealed class NanaDummyRelicPool : RelicPoolModel
{
    public override string EnergyColorName => "nana_dummy";

    public override Color LabOutlineColor => new Color("E6D000");

    protected override IEnumerable<RelicModel> GenerateAllRelics()
    {
        return (new RelicModel[11]
        {
            ModelDb.Relic<Brimstone>(),
            ModelDb.Relic<BurningBlood>(),
            ModelDb.Relic<CharonsAshes>(),
            ModelDb.Relic<DemonTongue>(),
            ModelDb.Relic<PaperPhrog>(),
            ModelDb.Relic<RedSkull>(),
            ModelDb.Relic<RuinedHelmet>(),
            ModelDb.Relic<SelfFormingClay>(),
            ModelDb.Relic<NanaRedTridentRelic>(),
            ModelDb.Relic<NanaJudgeRelic>(),
            ModelDb.Relic<NanaRelicTest>()
        });
    }

    public override IEnumerable<RelicModel> GetUnlockedRelics(UnlockState unlockState)
    {
        List<RelicModel> list = base.AllRelics.ToList();
        if (!unlockState.IsEpochRevealed<Ironclad3Epoch>())
        {
            list.RemoveAll((RelicModel r) => Ironclad3Epoch.Relics.Any((RelicModel relic) => relic.Id == r.Id));
        }
        if (!unlockState.IsEpochRevealed<Ironclad6Epoch>())
        {
            list.RemoveAll((RelicModel r) => Ironclad6Epoch.Relics.Any((RelicModel relic) => relic.Id == r.Id));
        }
        return list;
    }
}