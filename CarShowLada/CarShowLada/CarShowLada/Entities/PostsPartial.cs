namespace CarShowLada.Entities
{
    public partial class Post
    {
        public string SalaryText
        {
            get
            {
                return $"Зарплата: {Salary:N2} рублей";
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
            return $"{Name_post} {Salary:N2}";
        }

        public string ToStringIntoPDF()
        {
            return $"{Name_post}\n{SalaryText}";
        }
    }
}
