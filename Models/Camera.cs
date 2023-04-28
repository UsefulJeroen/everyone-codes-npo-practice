namespace Models
{
    public class Camera
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }

        public Camera(int id, string name, double latitude, double longtitude)
        {
            Id = id;
            Name = name;
            Latitude = latitude;
            Longtitude = longtitude;
        }

        public Camera() { }
    }
}