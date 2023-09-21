using MikartEnergy.DAL.Entities;

namespace MikartEnergy.DAL.Context.ETIM_files_reading
{
    public interface IEtimFeaturesAndValuesXmlReader
    {
        int CountFeatures();
        int CountValues();
        IEnumerable<EtimFeature> GetFeatures();
        IEnumerable<EtimValue> GetValues();
    }
}