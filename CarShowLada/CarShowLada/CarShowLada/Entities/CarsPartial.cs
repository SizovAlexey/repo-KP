using System.Windows;

namespace CarShowLada.Entities
{
    public partial class Car
    {
        public string CostText
        {
            get
            {
                return $"Цена: {Cost:N2} рублей";
            }
        }

        public string BackColor
        {
            get
            {
                if (CountCar == 0)
                    return "#ff6666";
                else
                    return "#75c1ff";
            }
        }

        public string ColorText
        {
            get
            {
                return $"Цвет: {Color.Name_color}";
            }
        }

        public string BodyText
        {
            get
            {
                return $"Тип кузова: {Body.Name_body}";
            }
        }

        public string MachineDriveText
        {
            get
            {
                return $"Привод: {Drive.Name_drive}";
            }
        }

        public string EnginePowerText
        {
            get
            {
                return $"Мощность двигателя: {Engine_power} л.с.";
            }
        }

        public string CountCarText
        {
            get
            {
                return $"Количество в наличии: {CountCar} шт.";
            }
        }

        public string AdminControlsVisibility
        {
            get
            {
                if (App.CurrentUser.id_Role == 1)
                {
                    return "Visible";
                }
                else
                {
                    return "Collapsed";
                }
            }
        }

        public string AvailabilityCarVisibility
        {
            get
            {
                if (CountCar > 0)
                {
                    return "Visible";
                }
                else
                {
                    return "Collapsed";
                }
            }
        }

        public override string ToString()
        {
            return $"{Model} {Body.Name_body} {Color.Name_color} В наличии: {CountCar}";
        }

        public string ToStringIntoPDF()
        {
            return $"{Model}\n{CostText}\n{ColorText}\n{BodyText}\n{MachineDriveText}\n{EnginePowerText}\n{CountCarText}";
        }
    }
}
