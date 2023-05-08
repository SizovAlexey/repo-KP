using System;

namespace CarShowLada.Entities
{
    public partial class Sale
    {
        public string DateSaleText
        {
            get
            {
                return $"Дата продажи: " + Convert.ToDateTime(Date_sale).ToString("dd MMMM yyyy");
            }
        }

        public string BranchText
        {
            get
            {
                return $"Филиал: {Branch.Name_branch}";
            }
        }

        public string WorkerText
        {
            get
            {
                if ((id_Worker == 0) || (id_Worker == null))
                    return "Сотрудник был удалён из базы данных";
                else
                    return $"Сотрудник: {Worker.Surname_worker} {Worker.Name_worker} {Worker.Patronymic_worker}";
            }
        }

        public string ClientText
        {
            get
            {
                return $"Клиент: {Client.Surname_client} {Client.Name_client} {Client.Patronymic_client}";
            }
        }

        public string CarText
        {
            get
            {
                if ((id_Car == 0) || (id_Car == null))
                    return "Автомобиль был удалён из базы данных";
                else
                    return $"Автомобиль: {Car.Model} {Car.Body.Name_body} {Car.Color.Name_color}";
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

        public override string ToString()
        {
            return $"Дата: {Date_sale:dd MMMM yyyy}\nФилиал: {Branch.Name_branch}\nСотрудник: {WorkerText}\nКлиент: {Client.Surname_client} {Client.Name_client} {Client.Patronymic_client}\n{CarText}";
        }
    }
}
