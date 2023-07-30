using Nac.Models.Entities;

namespace Nac.Lib.Utilities;

public static class ModelExtensions
{
    public static double GetSum(this CashStatus status)
    {
        return
            status._500
            + status._200
            + status._100
            + status._50
            + status._20
            + status._10
            + status._5
            + status._2
            + status._1
            + status._050
            + status._020
            + status._010
            + status._005
            + status._002
            + status._001
            ;
    }

}
