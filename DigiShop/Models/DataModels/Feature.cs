using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiShop.Models.DataModels
{
    public class Feature
    {
        public Guid FeatureId { get; set; }
        public string FeatureName { get; set; }
        public string FeatureValue { get; set; }
    }
}
