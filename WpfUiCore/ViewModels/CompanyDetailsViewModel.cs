using ThreeFourteen.Finnhub.Client.Model;

namespace WpfUiCore.ViewModels
{
    public class CompanyDetailsViewModel
    {
        public CompanyDetailsViewModel(Company2 company)
        {
            this.Company = company;
        }

        Company2 _company;

        public Company2 Company
        {
            get { return _company; }
            set { _company = value; }
        }
    }
}
