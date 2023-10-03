using System;
using System.Collections.Generic;
using System.Linq;
using TsaToolbox.Models.Setups;

namespace TsaToolbox.ViewModels;

public class WaveletViewModel : ViewModelBase
{
    private readonly WaveletSetup parameters;

    public WaveletViewModel(WaveletSetup parameters)
    {
        this.parameters = parameters;

        // Default values.
        Enabled = false;
        OmegaFrom = 0.5;
        OmegaTo = 10;
        UseRadians = false;
        Thinning = 10;
        Family = WaveletSetup.WvlFamily.Morl;
        ColorMap = WaveletSetup.WvlColorMap.Parula;
    }

    public bool Enabled
    {
        get => parameters.Enabled;

        set
        {
            parameters.Enabled = value;
            OnPropertyChanged(nameof(Enabled));
        }
    }

    public double OmegaFrom
    {
        get => parameters.OmegaFrom;

        set
        {
            parameters.OmegaFrom = value;
            OnPropertyChanged(nameof(OmegaFrom));
        }
    }

    public double OmegaTo
    {
        get => parameters.OmegaTo;

        set
        {
            parameters.OmegaTo = value;
            OnPropertyChanged(nameof(OmegaTo));
        }
    }

    public int Thinning
    {
        get => parameters.Thinning;

        set
        {
            parameters.Thinning = value;
            OnPropertyChanged(nameof(Thinning));
        }
    }

    public bool UseRadians
    {
        get => parameters.UseRadians;

        set
        {
            parameters.UseRadians = value;
            OnPropertyChanged(nameof(UseRadians));
        }
    }

    public IEnumerable<WaveletSetup.WvlFamily> Families { get; } =
        Enum.GetValues(typeof(WaveletSetup.WvlFamily)).Cast<WaveletSetup.WvlFamily>();

    public WaveletSetup.WvlFamily Family
    {
        get => parameters.Family;

        set
        {
            parameters.Family = value;
            OnPropertyChanged(nameof(Family));
        }
    }

    public IEnumerable<WaveletSetup.WvlColorMap> ColorMaps { get; } =
        Enum.GetValues(typeof(WaveletSetup.WvlColorMap)).Cast<WaveletSetup.WvlColorMap>();

    public WaveletSetup.WvlColorMap ColorMap
    {
        get => parameters.ColorMap;

        set
        {
            parameters.ColorMap = value;
            OnPropertyChanged(nameof(ColorMap));
        }
    }
}
