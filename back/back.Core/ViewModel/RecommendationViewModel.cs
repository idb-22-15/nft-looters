using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace back.Core.ViewModel
{
    public class RecommendationViewModel
    {
        public Guid userId { get; set; }

        public string title { get; set; }

        public string text { get; set; }
    }
}
