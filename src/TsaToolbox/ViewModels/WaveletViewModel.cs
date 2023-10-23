using System;
using System.Collections.Generic;
using System.Linq;
using TsaToolbox.Models.Setups;

namespace TsaToolbox.ViewModels;

public class WaveletViewModel
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

    [Notify]
    public bool Enabled
    {
        get => parameters.Enabled;
        set => parameters.Enabled = value;
    }

    [Notify]
    public double OmegaFrom
    {
        get => parameters.OmegaFrom;
        set => parameters.OmegaFrom = value;
    }

    [Notify]
    public double OmegaTo
    {
        get => parameters.OmegaTo;
        set => parameters.OmegaTo = value;
    }

    [Notify]
    public int Thinning
    {
        get => parameters.Thinning;
        set => parameters.Thinning = value;
    }

    [Notify]
    public bool UseRadians
    {
        get => parameters.UseRadians;
        set => parameters.UseRadians = value;
    }

    public IEnumerable<WaveletSetup.WvlFamily> Families { get; } =
        Enum.GetValues(typeof(WaveletSetup.WvlFamily)).Cast<WaveletSetup.WvlFamily>();

    [Notify]
    public WaveletSetup.WvlFamily Family
    {
        get => parameters.Family;
        set => parameters.Family = value;
    }

    public IEnumerable<WaveletSetup.WvlColorMap> ColorMaps { get; } =
        Enum.GetValues(typeof(WaveletSetup.WvlColorMap)).Cast<WaveletSetup.WvlColorMap>();

    [Notify]
    public WaveletSetup.WvlColorMap ColorMap
    {
        get => parameters.ColorMap;
        set => parameters.ColorMap = value;
    }
}
