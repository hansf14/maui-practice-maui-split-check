using System.Diagnostics;
using System.Text.RegularExpressions;

namespace maui_split_check
{
  public partial class MainPage : ContentPage
  {
    decimal totalBill = 0;
    int tipPercent = 0;
    int cntPeople = 1;

    public MainPage()
    {
      InitializeComponent();
    }

    private void TextTotalBill_Completed(object sender, EventArgs e)
    {
      var input = textTotalBill.Text;
      input = Regex.Replace(input, @"\s+", "");
      input = string.IsNullOrEmpty(input) ? "0.0" : input;

      totalBill = decimal.Parse(input);
      CalculateTotal();
    }

    private void CalculateTotal()
    {
      var totalTip = totalBill * ((decimal)tipPercent / 100);
      Debug.Write(totalBill);
      Debug.Write(totalTip);

      var tipByPerson = totalTip / cntPeople;
      labelTipPerPerson.Text = $"{tipByPerson:C}";

      var subtotal = totalBill / cntPeople;
      labelSubtotal.Text = $"{subtotal:C}";

      var totalByPerson = (totalBill + totalTip) / cntPeople;
      labelTotalBill.Text = $"{totalByPerson:C}";
    }

    private void SliderTipPercent_ValueChanged(object sender, ValueChangedEventArgs e)
    {
      tipPercent = (int)sliderTipPercent.Value;
      labelTipPercent.Text = $"Tip: {tipPercent}%";
      CalculateTotal();
    }

    private void ButtonTipPercent_Clicked(object sender, EventArgs e)
    {
      if (sender is not Button)
      {
        return;
      }

      var button = (Button)sender;
      var _tipPercent = int.Parse(button.Text.Replace("%", ""));
      sliderTipPercent.Value = _tipPercent;
    }

    private void ButtonTotalCntPeopleMinus_Clicked(object sender, EventArgs e)
    {
      if (cntPeople > 1)
      {
        cntPeople--;
      }
      labelTotalCntPeople.Text = cntPeople.ToString();
      CalculateTotal();
    }

    private void ButtonTotalCntPeoplePlus_Clicked(object sender, EventArgs e)
    {
      cntPeople++;
      labelTotalCntPeople.Text = cntPeople.ToString();
      CalculateTotal();
    }
  }
}
