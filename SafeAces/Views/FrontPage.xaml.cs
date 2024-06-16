namespace SafeAces.Views;

public partial class FrontPage : ContentPage
{
	public FrontPage()
	{
		InitializeComponent();
	}

    private void btnadd_Clicked(object sender, EventArgs e)
    {
        Application.Current!.MainPage = new ScanPage();
    }
}