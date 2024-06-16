using SafeAces.Models;
using Microsoft.Maui.Controls;

using System;
using System.Security.Cryptography;
using static SafeAces.Includes.GlobalVariables;
namespace SafeAces.Views;

public partial class VisitorTimeOut : ContentPage
{
    private Label _resultLabel;
    private DateTime _timeIn;
    private DateTime _timeOut;
    Visitor _visitor = new();
    public VisitorTimeOut()
	{
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        barcodeResultID.Text = id;
        txtpurpose.Text = purpose;
        txtvisitor.Text = fullname;
        barcodeResultDate.Text = date;
        barcodeResult.Text = timein;
        barcodeResultOut.Text = timeout;
      



    }
    private void cameraView_CamerasLoaded(object sender, EventArgs e)
    {
        if (cameraView.Cameras.Count > 0)
        {
            cameraView.Camera = cameraView.Cameras.First();
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await cameraView.StopCameraAsync();
                await cameraView.StartCameraAsync();
            });
        }
    }
    private async void cameraView_BarcodeDetected(object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs args)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            var barcodeText = $"{args.Result[0].BarcodeFormat}: {args.Result[0].Text}";
            var currentTime = DateTime.Now;
            if (barcodeText.Contains("OUT"))
            {

                _timeOut = currentTime;
                barcodeResultOut.Text = $" {_timeOut.ToString("hh:mm tt")}";
                
            }
        });
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        //await _visitor.EditVisitor( barcodeResultOut.Text);
        await DisplayAlert("Update Successfully!!!", " ", "OK");
        Application.Current!.MainPage = new VisitorPage();
    }
}