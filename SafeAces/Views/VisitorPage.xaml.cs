using SafeAces.Models;
using System.Threading;
using static SafeAces.Includes.GlobalVariables;
using CommunityToolkit.Maui.Alerts;
using IronBarCode;
namespace SafeAces.Views;

public partial class VisitorPage : ContentPage
{
    //    Visitor _visitorslist = new Visitor();
    //    public VisitorPage()
    //    {
    //        InitializeComponent();
    //    }
    //    protected override async void OnAppearing()
    //    {
    //        base.OnAppearing();
    //        await FillList();
    //    }
    //    private async Task FillList()
    //    {
    //        ListStudents.ItemsSource = await _visitorslist.GetVisitor();
    //    }



    //    private async void itemdeletestudent_OnInvoked(object sender, EventArgs e)
    //    {
    //        var item = sender as SwipeItemView;
    //        if (!(item?.BindingContext is Visitor getID)) return;
    //        id = getID.ID;
    //        if (!await DisplayAlert("Delete Record",
    //                "You are about to delete the Record." +
    //                "Do you really want to Delete?", "Yes", "No")) return;
    //        //progressLoading.IsVisible = true;
    //        var a = await _visitorslist.DeleteVisitor(id);
    //        if (a)
    //        {

    //            var toast = Toast.Make("Student was removed Successfully!");
    //            await FillList();
    //        }
    //        else
    //        {
    //            var toast = Toast.Make("Something went wrong with your request. Please try again later!");
    //        }
    //    }


    //    private void ImageButton_Clicked(object sender, EventArgs e)
    //    {
    //        Application.Current!.MainPage = new ScanPage();
    //    }

    //    private async void itemeditvisitor_Invoked(object sender, EventArgs e)
    //    {
    //        var item = sender as SwipeItemView;
    //        if (item == null) return;
    //        if (item.BindingContext is Visitor details)
    //        {
    //            visitorkey = await _visitorslist.GetVisitorKey(details.ID);
    //            await DisplayAlert("Test", visitorkey, "OK");
    //            Application.Current!.MainPage = new VisitorTimeOut();

    //        }
    //    }
}
