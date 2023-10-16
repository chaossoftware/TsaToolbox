namespace TsaToolbox.Models.Setups;

public class WaveletSetup
{
    public bool Enabled { get; set; }

    public double OmegaFrom { get; set; }

    public double OmegaTo { get; set; }

    public bool UseRadians { get; set; }

    public int Thinning { get; set; }

    public WvlFamily Family { get; set; }

    public WvlColorMap ColorMap { get; set; }

    public enum WvlFamily
    {
        Gaus8,
        Morl,
        Db10,
        Haar,
        Sym8,
        Mexh,
        Meyr,
        Cgau8
    }

    public enum WvlColorMap
    {
        Pink,
        Parula
    }
}
