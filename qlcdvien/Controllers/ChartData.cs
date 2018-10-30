namespace qlcdvien.Controllers
{
    public class ChartData
    {


        public string id { get; set; }
        public string name { get; set; }
        public string pid { get; set; }



        public ChartData(string v1, string name, string v2)
        {
            this.id = v1;
            this.name = name;
            this.pid = v2;
        }
    }
}