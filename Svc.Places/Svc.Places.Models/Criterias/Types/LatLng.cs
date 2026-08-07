using System;

namespace Svc.Places.Models.Criterias.Types;

/// <summary>
/// Lat Lng.
/// </summary>
public class LatLng
{
    private double? latitude;
    private double? longitude;

    /// <summary>
    /// Latitude.
    /// </summary>
    public virtual double Latitude
    {
        get => this.latitude ?? 0;
        set
        {
            if (value is < -90 or > 90)
            {
                throw new ArgumentOutOfRangeException(nameof(this.Latitude), @$"{nameof(this.Latitude)} must be between -90 and 90 degrees.");
            }
            this.latitude = value;
        }
    }

    /// <summary>
    /// Longitude.
    /// </summary>
    public virtual double Longitude
    {
        get => this.longitude ?? 0;
        set
        {
            if (value is < -180 or > 180)
            {
                throw new ArgumentOutOfRangeException(nameof(this.Longitude), @$"{this.Longitude} must be between -180 and 180 degrees.");
            }
            this.longitude = value;
        }
    }
}