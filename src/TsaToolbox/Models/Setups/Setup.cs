namespace TsaToolbox.Models.Setups;

public class Setup
{
    public FftSetup Fft { get; } = new FftSetup();

    public WaveletSetup Wavelet { get; } = new WaveletSetup();
}
