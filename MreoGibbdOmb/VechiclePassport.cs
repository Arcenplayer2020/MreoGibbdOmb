using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MreoGibbdOmb
{
    internal class VechiclePassport
    {
        public string Name;
        public string Equipment;
        public string Color;
        public int Mileage;
        public string EngineType;
        public string VinCode;
        public int CarOwners;
        public int CountDTPs;
        public int AutoStatus;
        public int CarAge;
        public string? CarModifications;

        public VechiclePassport(string Name, string Equipment, string Color,string EngineType,int AutoStatus, int CarAge,string? CarModifications = null)
        {
            this.Name = Name;
            this.Equipment = Equipment;
            this.Color = Color;
            this.EngineType = EngineType;
            this.AutoStatus = AutoStatus;
            this.CarAge = CarAge;
            if (CarModifications != null)
            {
                this.CarModifications = CarModifications;
            }
            else
            {
                this.CarModifications = "--";
            }
            Mileage = GenerateMileage(AutoStatus);
            CountDTPs = GenerateCountDTPS(AutoStatus);
            CarOwners = GenerateCarOwners(AutoStatus);
            VinCode = GenerateVinCode();
            //this.ToString();
            
        }

        public override string ToString()
        {
            string stringAutoStatus;
            string carPermissionToModificate;
            if (AutoStatus == 2)
            {
                stringAutoStatus = "Битый";
            }
            else
            {
                stringAutoStatus = "Чистый";
            }
            if (CarModifications == null)
            {
                carPermissionToModificate = "--";
            }
            else
            {
                carPermissionToModificate= "+";
            }
            
            string stringPts = $"Марка, Модель, Год автомобиля: {Name}, {CarAge}\r\n\r\nКомплектация автомобиля: {Equipment} \r\n\r\nЦвет автомобиля: {Color}\r\n\r\nОдометр: {Mileage.ToString("#.#", CultureInfo.InvariantCulture)}\r\n\r\nТип Двигателя: {EngineType}\r\n\r\nVIN Номер: {VinCode}\r\n\r\nРазрешение на изменения в конструкции автомобиля = {carPermissionToModificate}\r\n\r\nВсе изменения в конструкции автомобиля = {CarModifications}\r\n\r\nЗаписей владельцев = {CarOwners}\r\n\r\nУчастие в ДТП = {CountDTPs} ДТП\r\n\r\nНомерной знак: ---\r\n\r\nСтатус авто: {stringAutoStatus}";
            return stringPts;
        }

        private int GenerateMileage(int autoStatus)
        {
            Random random = new Random();
            if (autoStatus == 0)
            {
                return random.Next(100000, 150000);
            }
            else if (autoStatus == 1)
            {
                return random.Next(150000, 300000);
            }
            else
            {
                return random.Next(300000, 1000000);
            }

        }

        private int GenerateCarOwners(int autoStatus)
        {
            Random random = new Random();
            if (autoStatus == 0)
            {
                return 1;
            }
            else if (autoStatus == 1)
            {
                return random.Next(2, 6);
            }
            else
            {
                return random.Next(6, 10);
            }
        }

        private int GenerateCountDTPS(int autoStatus)
        {
            Random random = new Random();
            if (autoStatus == 0)
            {
                return 0;
            }
            else if (autoStatus == 1)
            {
                return random.Next(1, 3);
            }
            else
            {
                return random.Next(3, 15);
            }
        }

        internal static async Task GeneratePts(SocketSlashCommand command)
        {
            var options = command.Data.Options.ToList();
            VechiclePassport passport = new VechiclePassport( Convert.ToString(options[0].Value)!, Convert.ToString(options[1].Value)!, Convert.ToString(options[2].Value)!, Convert.ToString(options[3].Value)!, Convert.ToInt32(options[4].Value), Convert.ToInt32(options[5].Value), Convert.ToString(options[6].Value));
            await command.RespondAsync(passport.ToString());
        }

        private string GenerateVinCode()
        {
            string vinCode = string.Empty;
            List<char> list = new List<char>();
            for (char c = 'A'; c <= 'Z'; ++c)
            {
                if (c != 'O' && c != 'I' && c != '!')
                {
                    list.Add(c);
                }
                
            }
            list.AddRange("0123456789012345678901234567".ToCharArray());
            
            Random random = new Random();
            for (int i = 0; i < 17; i++)
            {
                vinCode = vinCode + list[random.Next(list.Count)];
            }
            return vinCode;
            
        }
    }
   
}
