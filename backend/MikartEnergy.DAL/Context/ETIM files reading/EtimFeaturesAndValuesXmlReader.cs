using MikartEnergy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikartEnergy.DAL.Context.ETIM_files_reading
{
    public class EtimFeaturesAndValuesXmlReader
    {
        private readonly string _pathToFile = string.Empty;
        private IEnumerable<Product> _products;
        private int _featuresNumber;
        private int _valuesNumber;

        public EtimFeaturesAndValuesXmlReader(string pathToFile)
        {
            _pathToFile = pathToFile;
        }

        public IEnumerable<Product> GetProducts()
        {
            if (_pathToFile is null)
            {
                throw new NullReferenceException($"{nameof(_pathToFile)} cann not be null.");
            }
            if (string.IsNullOrWhiteSpace(_pathToFile))
            {
                throw new ArgumentOutOfRangeException($"{nameof(_pathToFile)} cann not be empty or white space.");
            }

            if (_products is null)
            {
                _products = GetProductsFromEtimXmlFile();
                _productsNumber = _products.Count();
                return _products;
            }
            return _products;
        }

        public int CountFeatures()
        {
            return _featuresNumber;
        }

        public int CountValues()
        {
            return _valuesNumber;
        }

    }
}
