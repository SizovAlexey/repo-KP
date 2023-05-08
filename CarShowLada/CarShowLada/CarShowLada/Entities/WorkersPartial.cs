using System;

namespace CarShowLada.Entities
{
    public partial class Worker
    {
        public string BirthdateWorkerText
        {
            get
            {
                return $"Дата рождения: " + Convert.ToDateTime(Birthdate_worker).ToString("dd MMMM yyyy");
            }
        }

        public string PostWorkerText
        {
            get
            {
                return $"Должность: {Post.Name_post}";
            }
        }

        public string NumberPhoneText
        {
            get
            {
                return $"Номер телефона: {Telephone_number_worker}";
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
            return $"{Surname_worker} {Name_worker} {Patronymic_worker}";
        }

        public string ToStringIntoPDF()
        {
            return $"{Surname_worker} {Name_worker} {Patronymic_worker}\n{BirthdateWorkerText}\n{PostWorkerText}\n{NumberPhoneText}";
        }
    }
}
