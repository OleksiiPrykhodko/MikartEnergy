using MikartEnergy.DAL.Entities;
using System.Xml.Linq;

namespace MikartEnergy.DAL.Context.ETIM_files_reading
{
    public class EtimFeaturesAndValuesXmlReader : IEtimFeaturesAndValuesXmlReader
    {
        private readonly string _pathToFile = string.Empty;
        private IEnumerable<EtimFeature> _features = new EtimFeature[0];
        private IEnumerable<EtimValue> _values = new EtimValue[0];
        private int _featuresNumber;
        private int _valuesNumber;

        public EtimFeaturesAndValuesXmlReader(string pathToFile)
        {
            _pathToFile = pathToFile;
        }

        public IEnumerable<EtimFeature> GetFeatures()
        {
            if (_pathToFile is null)
            {
                throw new NullReferenceException($"{nameof(_pathToFile)} cann not be null.");
            }
            if (string.IsNullOrWhiteSpace(_pathToFile))
            {
                throw new ArgumentOutOfRangeException($"{nameof(_pathToFile)} cann not be empty or white space.");
            }

            if (_features.Count() == 0 || _values.Count() == 0)
            {
                GetFeaturesAndValuesFromXmlFile();
                _featuresNumber = _features.Count();
                _valuesNumber = _values.Count();
                return _features;
            }
            return _features;
        }

        public IEnumerable<EtimValue> GetValues()
        {
            if (_pathToFile is null)
            {
                throw new NullReferenceException($"{nameof(_pathToFile)} cann not be null.");
            }
            if (string.IsNullOrWhiteSpace(_pathToFile))
            {
                throw new ArgumentOutOfRangeException($"{nameof(_pathToFile)} cann not be empty or white space.");
            }

            if (_values.Count() == 0 || _features.Count() == 0)
            {
                GetFeaturesAndValuesFromXmlFile();
                _featuresNumber = _features.Count();
                _valuesNumber = _values.Count();
                return _values;
            }
            return _values;
        }

        public int CountFeatures()
        {
            if (_featuresNumber == 0)
            {
                GetFeaturesAndValuesFromXmlFile();
            }
            return _featuresNumber;
        }

        public int CountValues()
        {
            if (_valuesNumber == 0)
            {
                GetFeaturesAndValuesFromXmlFile();
            }
            return _valuesNumber;
        }

        private void GetFeaturesAndValuesFromXmlFile()
        {
            var xDocument = XDocument.Load(_pathToFile);
            _features = xDocument.Descendants().Where(d => d.Name.LocalName == "Feature")
                .Select(f =>
                {
                    var elements = f.Elements();
                    return new EtimFeature()
                    {
                        Code = elements.First(e => e.Name.LocalName == "Code").Value,
                        Type = elements.First(e => e.Name.LocalName == "Type").Value,
                        Deprecated = bool.Parse(elements.First(e => e.Name.LocalName == "Deprecated").Value),
                        Description = elements.First(e => e.Name.LocalName == "Description").Value
                    };
                });

            _values = xDocument.Descendants().Where(d => d.Name.LocalName == "Value")
                .Select(v =>
                {
                    var elements = v.Elements();
                    return new EtimValue()
                    {
                        Code = elements.First(e => e.Name.LocalName == "Code").Value,
                        Deprecated = bool.Parse(elements.First(e => e.Name.LocalName == "Deprecated").Value),
                        Description = elements.First(e => e.Name.LocalName == "Description").Value
                    };
                });
        }


    }
}
