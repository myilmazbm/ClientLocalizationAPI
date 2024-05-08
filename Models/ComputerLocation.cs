namespace ClientLocalizationAPI.Models
{
    public class ComputerLocation
    {
        //bir bilgisayarın konumunu koordinatlarla, bilgisayar ismiyle, kaydedilen zamanla ve kullanıcı ismiyle belirlemek için kullanılan model
        public int ID { get; set; }
        public string ComputerName { get; set; }
        public string UserName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime SavedTime { get; set; }


    }
}
