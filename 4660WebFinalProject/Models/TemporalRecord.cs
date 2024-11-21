using System;
using Newtonsoft.Json;

namespace _4660FinalProject.Models
{
    public class TemporalRecord
    {
        public int EMP_NO { get; set; }

        [JsonProperty("Salary")]
        public decimal Salary { get; set; }

        [JsonProperty("TSTART")]
        public DateTime TSTART { get; set; }

        [JsonProperty("END")]
        public DateTime END { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is TemporalRecord other)
            {
                return EMP_NO == other.EMP_NO &&
                       Salary == other.Salary &&
                       TSTART == other.TSTART &&
                       END == other.END;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(EMP_NO, Salary, TSTART, END);
        }

    }

    public class RecordWrapper
    {
        public ObjectIdWrapper id { get; set; }  // Nested id field with $oid
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string data { get; set; }
    }

    public class ObjectIdWrapper
    {
        [JsonProperty("$oid")]
        public string Oid { get; set; }  // Maps the $oid field in JSON to Oid
    }

}
