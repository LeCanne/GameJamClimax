using UnityEngine;

public static class ScreenShake
{
    public static ScreenShakeController controller;

    public static void Shake(float amplitude = 1f, float fadeDuration = 1f)
    {
        controller.Shake(amplitude, fadeDuration);
    }
}
