using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Assets.scripts.service
{
    public static class VectorWorker
    {
        public static float FindRotationByTarget(Vector3 rotatable, Vector3 target)
        {
            var trigonometryAlignment = rotatable.x - target.x < 0 ? 180 : 0;
            var aimRotationAngle = -Math.Atan(
                                       (target.y - rotatable.y)
                                       / (rotatable.x - target.x))
                                   * Mathf.Rad2Deg
                                   + trigonometryAlignment;
            aimRotationAngle = aimRotationAngle < 0 ? 360 + aimRotationAngle : aimRotationAngle;
            return (float)aimRotationAngle;
        }
    }
}
