using Welcome.ViewModel;

namespace Welcome.View
{
    public class UserView
    {
        private UserViewModel _viewModel;

        public UserView(UserViewModel viewModel)
        {
            this._viewModel = viewModel;   
        }

        public void Display()
        {
            Console.WriteLine("Name " + _viewModel.Name);
            Console.WriteLine("Role " + _viewModel.Role.ToString());        
        }
        
        public void DisplayError()
        {
            throw new Exception("Exception happened");      
        }
    }
}
