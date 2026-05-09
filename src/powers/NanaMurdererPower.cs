using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SlayTheNANA;

public sealed class NanaMurdererPower : PowerModel
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromPower<NanaKarma>())];

    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Single;

    private HashSet<CardModel>? _karmaGrantedForAttackCardsThisTurn;

    public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (dealer != base.Owner || target == null || target == base.Owner)
        {
            return 1m;
        }

        if (!props.IsPoweredAttack_())
        {
            return 1m;
        }

        if (cardSource == null || cardSource.Type != CardType.Attack)
        {
            return 1m;
        }

        return 1.5m;
    }

    public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext, Creature? dealer, DamageResult result, ValueProp props, Creature target, CardModel? cardSource)
    {
        if (dealer != base.Owner || cardSource == null || cardSource.Type != CardType.Attack)
        {
            return;
        }

        if (!props.IsPoweredAttack_())
        {
            return;
        }

        _karmaGrantedForAttackCardsThisTurn ??= new HashSet<CardModel>();
        if (!_karmaGrantedForAttackCardsThisTurn.Add(cardSource))
        {
            return;
        }

        await PowerCmd.Apply<NanaKarma>(base.Owner, base.Amount, base.Owner, cardSource);
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == base.Owner.Side)
        {
            _karmaGrantedForAttackCardsThisTurn?.Clear();
        }
    }
}
