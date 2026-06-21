using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MegaCrit.Sts2.Core.Models.CardPools;
using BaseLib.Abstracts;

namespace SlayTheNANA;

[Pool(typeof(NanaDummyCardPool))]
public sealed class NanaTrial : NanaCardModel, ITranscendenceCard
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("NanaKarma", 8m)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromPower<NanaKarma>())];

    public NanaTrial()
        : base(1, CardType.Skill, CardRarity.Basic, TargetType.AnyEnemy)
    {
    }

    public CardModel GetTranscendenceTransformedCard() => ModelDb.Card<NanaVerdict>();

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {

        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.AttackAnimDelay);
        await PowerCmd.Apply<NanaKarma>(choiceContext, cardPlay.Target, base.DynamicVars["NanaKarma"].BaseValue, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["NanaKarma"].UpgradeValueBy(4m);
    }
}
