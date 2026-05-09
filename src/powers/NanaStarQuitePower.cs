using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
namespace SlayTheNANA;

public sealed class NanaStarQuietPower : TemporaryStrengthPower
{
    public override AbstractModel OriginModel => ModelDb.Card<NanaStarQuiet>();
}
