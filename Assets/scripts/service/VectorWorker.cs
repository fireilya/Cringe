using System;
using UnityEngine;

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

        public static float RotateToTargetWithSpeed(float currentRotation, float neededRotation, float rotationDelta)
        {
            var isClampUp = neededRotation >= currentRotation;
            var variant1 = currentRotation - neededRotation;
            var variant2 = 360 - Math.Abs(currentRotation - neededRotation);
            var direction =
                Math.Abs(variant1) < Math.Abs(variant2) && currentRotation - neededRotation > 0
                || Math.Abs(variant1) > Math.Abs(variant2) && currentRotation - neededRotation < 0
                    ? -1
                    : 1;
            currentRotation += rotationDelta * direction;
            currentRotation = !isClampUp
                ? Mathf.Clamp(currentRotation, neededRotation, currentRotation)
                : Mathf.Clamp(currentRotation, currentRotation, neededRotation);
            return currentRotation;
        }
    }
}