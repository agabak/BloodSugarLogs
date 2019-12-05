namespace BloodSugarLog.Entities
{
    public class BloodSugarMeasurement
    {

        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public int MeasurementValue { get; set; }
    }
}
