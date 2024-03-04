namespace Cikoria.EggTimer
{
    public enum TimeStyle
    {
        Scaled, // Makes the action's duration depend on the time scale and update every frame
        Unscaled, // Makes the action's duration independent of the time scale and update every frame
        FixedScaled, // Makes the action's duration depend on the time scale and update every fixed frame (physics update).
        FixedUnscaled // Makes the action's duration independent of the time scale and update every fixed frame (physics update).
    }
}