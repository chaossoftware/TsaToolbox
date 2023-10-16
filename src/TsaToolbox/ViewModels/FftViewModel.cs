using TsaToolbox.Models.Setups;

namespace TsaToolbox.ViewModels;

public class FftViewModel : ViewModelBase
{
    private readonly FftSetup parameters;

    public FftViewModel(FftSetup parameters)
    {
        this.parameters = parameters;

        // Default values.
        Enabled = false;
        Dt = double.NaN;
        OmegaFrom = 0.5;
        OmegaTo = 10;
        UseRadians = false;
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

    public double Dt
    {
        get => parameters.Dt;

        set
        {
            parameters.Dt = value;
            OnPropertyChanged(nameof(Dt));
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

    public bool UseRadians
    {
        get => parameters.UseRadians;

        set
        {
            parameters.UseRadians = value;
            OnPropertyChanged(nameof(UseRadians));
        }
    }
}
