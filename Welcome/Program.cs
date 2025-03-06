
using Welcome.Model;
using Welcome.View;
using Welcome.ViewModel;

public class Program
{
    public static void Main(string[] args)
    {
        User user = new User();
        UserViewModel viewModel = new UserViewModel(user);
        UserView view = new UserView(viewModel);

        view.Display();
    }
}


