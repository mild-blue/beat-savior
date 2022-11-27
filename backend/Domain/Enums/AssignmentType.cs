namespace BildMlue.Domain.Enums;

public enum AssignmentType
{
    /// <summary>
    /// Nothing is assigned
    /// </summary>
    None,

    /// <summary>
    /// Go to patient and perform resuscitation.
    /// </summary>
    Resuscitation,

    /// <summary>
    /// Pickup defibrillator and go to patient.
    /// Return the AED back later.
    /// </summary>
    DefibrillatorPickup,

    /// <summary>
    /// Return the defibrillator to the pickup location.
    /// </summary>
    DefibrillatorReturn,

    /// <summary>
    /// Go to patient and assist other first responders.
    /// </summary>
    Assisting,
}