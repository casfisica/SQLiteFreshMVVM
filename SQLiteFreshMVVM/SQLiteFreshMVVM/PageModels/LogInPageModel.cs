using System.Threading.Tasks;
using System.Windows.Input;
using FreshMvvm;
using SQLiteFreshMVVM.Models;
using SQLiteFreshMVVM.Services;
using Xamarin.Forms; 

namespace SQLiteFreshMVVM.PageModels
{
    public class LogInPageModel : FreshBasePageModel
    {


        public ICommand EnterCommand
        {
            get
            {
                return new Command(async () => {
                    if (true)
                    {
                        await CoreMethods.PushPageModel<UserListPageModel>();
                    }
                });
            }
        }
    }
}
