using System;
using System.Security.Cryptography;
using Microsoft.Maui.Controls;
using SafeAces.Models;
using IronSoftware.Drawing;
using IronBarCode;
namespace SafeAces.Views;

public partial class ScanPage : ContentPage
{
    private string _barcodeId;
    private Label _resultLabel;
    private DateTime _timeIn;
    private DateTime _timeOut;
    private DateTime _datetime;
    Visitor _visitor = new();
    public ScanPage()
    {
        InitializeComponent();
        cameraView.BarCodeOptions = new()
        {
            PossibleFormats = { ZXing.BarcodeFormat.QR_CODE }
        };
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
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            //var barcodeText = $"{args.Result[0].BarcodeFormat}: {args.Result[0].Text}";
            var barcodeText = args.Result[0].Text;
            var barcodeId = ExtractBarcodeId(barcodeText);
            var currentTime = DateTime.Now;
            var currentDate = DateTime.Now;

      

            if (!string.IsNullOrEmpty(barcodeId))
            {

                _barcodeId = barcodeId;
                barcodeResultID.Text = barcodeId;



                _datetime = currentDate;
                barcodeResultDate.Text = $"{_datetime.ToString("yyyy-MM-dd")}";

                _timeIn = currentTime;

                barcodeResult.Text = $"{_timeIn.ToString("hh:mm tt")}";

                barcodeResultOut.Text = $"Visitor Didnt CheckedOut Yet";

            }
            var visitor = await _visitor.GetVisitor(barcodeId);
            if (visitor != null)
            {
                {
                    barcodeResultID.Text = visitor.BarcodeId;
                    txtpurpose.Text = visitor.Purpose;
                    txtvisitor.Text = visitor.FullName;
                    barcodeResult.Text = visitor.TimeIn;
                    barcodeResultDate.Text = visitor.Date;

                    _timeOut = currentTime;

                    barcodeResultOut.Text = $"{_timeOut.ToString("hh:mm tt")}";
                }

            }
        });

    }

    private string ExtractBarcodeId(string barcodeText)
    {
        // Assuming the barcode ID is in the format "Date: <ID>"
        var parts = barcodeText.Split(':');
        if (parts.Length > 1)
        {
            return parts[1].Trim();
        }
        return string.Empty;
    }

    private async void btnadd_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(barcodeResultID.Text))
        {
            await DisplayAlert("Data validation", "Please Enter Purpose of Visit", "Got it");
            return;
        }
        else if
            (string.IsNullOrEmpty(txtpurpose.Text))
        {
            await DisplayAlert("Data validation", "Please Enter Purpose of Visit", "Got it");
            return;
        }
        else if (string.IsNullOrEmpty(txtvisitor.Text))
        {
            await DisplayAlert("Data validation", "Please Enter Name", "Got it");
            return;
        }

        bool a;
        a = await _visitor.GetVisID(txtvisitor.Text);
        if (!a)
        {
            await DisplayAlert("Time In validation", "You Have Already Timed In", "Got it");
        }
        else
        {

            await _visitor._AddVisitor(barcodeResultID.Text, txtpurpose.Text, txtvisitor.Text, barcodeResultDate.Text, barcodeResult.Text, barcodeResultOut.Text);
            barcodeResultID.Text = string.Empty;
            txtpurpose.Text = string.Empty;
            txtvisitor.Text = string.Empty;
            barcodeResultDate.Text = string.Empty;
            barcodeResult.Text = string.Empty;
            barcodeResultOut.Text = string.Empty;

            await DisplayAlert(" ", "Time In Successfully!!!", "OK");

            Application.Current!.MainPage = new ScanPage();
            return;


        }

    }


    private async void btntimeOut_Clicked(object sender, EventArgs e)
    {

        bool a;
        a = await _visitor.GetVisID(barcodeResultOut.Text);
        if (!a)
        {
            await DisplayAlert("Time Out validation", "You Have Already Timed Out", "Got it");
        }
        else
        {

            var visitor = await _visitor.GetVisitor(barcodeResultID.Text);
            if (visitor != null)
            {
                visitor.TimeOut = DateTime.Now.ToString("hh:mm tt");
                await _visitor.EditVisitor(visitor.Id, visitor.Purpose, visitor.FullName, visitor.Date, visitor.TimeIn, visitor.TimeOut);
                barcodeResultOut.Text = visitor.TimeOut;
                await DisplayAlert(" ", "Time Out Successfully!!!", "OK");
                Application.Current!.MainPage = new ScanPage();
                return;
            }

        }
    }
}

    //private async Task UpdateTimeout(string barcodeId)
    //{

    //    var visitor = await _visitor.GetVisitor(barcodeId);
    //    if (visitor != null)
    //    {
    //        visitor.TimeOut = DateTime.Now.ToString("hh:mm tt");
    //        await _visitor.EditVisitor(visitor.ID, visitor.Purpose, visitor.FullName, visitor.Date, visitor.TimeIn, visitor.TimeOut);
    //        barcodeResultOut.Text = visitor.TimeOut;
    //    }

    //}


