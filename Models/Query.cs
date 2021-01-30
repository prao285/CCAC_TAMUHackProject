using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace hackathon
{ 
    public class Query
    {
        [StringLength(50)]
        public string StartingLocation { get; set; }
        [StringLength(75)]
        public string EndingLocation { get; set; }
        public int  NumConnections{ get; set; }
        public string ConnectingLocations { get; set; }  
    }
}