namespace c_diez_16
{
    internal class Program
    {
        public delegate void PPPP(Object k);
        public interface ITemperatureControl
        {
            public void MonitorTemperature(OOO brrr);
            public void CoolDown(OOO brrr);
        }
        public abstract class OOO
        {
            public string Name { get; set; }
            public double Temperature { get; set; }
            public virtual void MostrName()
            {
                Console.WriteLine(" "+Name);
            }
            public virtual void MostrTemp()
            {
                Console.WriteLine(" Температура : " + Temperature);
            }
        }
        public class Processor :OOO
        {
            public string Name { get; set; }
            public double Temperature { get; set; }
            public Processor() { Name = "processor";Temperature = 0; }
            public override void MostrName()
            {
                Console.WriteLine(" " + Name);
            }

        }
        public class GraphicsCard:OOO
        {
            public string Name { get; set; }
            public double Temperature { get; set; }
            public GraphicsCard() { Name = "GraphicsCard";Temperature = 0; }
            public override void MostrName()
            {
                Console.WriteLine(" " + Name);
            }

        }
        public class Cooler: ITemperatureControl
        {
            public event PPPP HardTemp;
            public string Name { get; set; }
            public double Temperature { get; set; }
            public List<double> SoS { get; set; }
            public Cooler()
            {
                Name = " Cooler";Temperature = 0;
                SoS = new List<double>();
            }
            public void MonitorTemperature(OOO brrr)
            {
                SoS.Clear();
                Random ran = new Random();
                while (true)
                {
                    brrr.Temperature = ran.Next(0, 150);
                    double b = brrr.Temperature;
                    SoS.Add(b);

                    brrr.MostrTemp();
                    if (brrr.Temperature > 100)
                    {

                        CoolDown(brrr);return;
                    }
                }
            }
            public void CoolDown(OOO brrr)
            {
                while (brrr.Temperature > 60)
                {
                    HardTemp?.Invoke(this);
                    Console.WriteLine(" Уменьшение температуры ");
                    brrr.Temperature -= 10;
                }
                brrr.MostrTemp();
                Console.WriteLine("-------");
                return;
            }
        }
        public class ComputerSystem
        {
            public Processor PROC { get; set; }
            public GraphicsCard GRC { get; set; }
            public List<OOO> Cards { get; set; }
            public Cooler COO { get; set; }
            
            public ComputerSystem()
            {
                 PROC = new Processor();GRC = new GraphicsCard();COO = new Cooler();Cards = new List<OOO> { PROC,GRC};
            }
            public void AddList(OOO ob) { Cards.Add(ob); }
            public void Monitoring()
            {
                foreach(var T in Cards)
                {
                    T.MostrName();
                    COO.MonitorTemperature(T);

                }
                var RR = COO.SoS.Max(s => s);
                //var g = Cards.Where(s => s.Temperature >= RR).Select(s => s);
                foreach (var D in COO.SoS) {
                    if (D >= RR)
                    {
                        Console.WriteLine(" Максимальная температура " + D);
                    }
                }
            }

        }
        static void Main(string[] args)
        {
            ComputerSystem CS = new ComputerSystem();

            CS.Monitoring();
        }
    }
}