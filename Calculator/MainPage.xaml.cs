using System.Data;
using System;
using System.Text.RegularExpressions;

namespace Calculator;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
    }

    private void onSymbolClicked(object sender, EventArgs e)
    {
        if (ResultEntry.Text == "0")
        {
            ResultEntry.Text = "";
        }
        ResultEntry.Text += ((Button)sender).Text;
    }
    
    private void clearInput(object sender, EventArgs e)
    {
        ResultEntry.Text = "0";
    }
    
    private void backSpace(object sender, EventArgs e)
    {
        if (ResultEntry.Text.Length > 0)
        {
            ResultEntry.Text = ResultEntry.Text.Remove(ResultEntry.Text.Length - 1);
        }
    }
    
    private void calculateResult(object sender, EventArgs e)
    {
        // Regular expression to find any decimal point not followed by a digit
        string pattern = @"\.\b(?!\d)";

        // Replace any match with ".0"
        ResultEntry.Text = Regex.Replace(ResultEntry.Text, pattern, ".0");
        
        try
        {
            DataTable table = new DataTable();
            table.Columns.Add("expression", typeof(string), (ResultEntry.Text.Replace('÷', '/')));
            DataRow row = table.NewRow();
            table.Rows.Add(row);
            ResultEntry.Text = double.Parse((string)row["expression"]).ToString();
        }
        catch (SyntaxErrorException)
        {
            ResultEntry.Text = "Syntax Error";
        }
        catch (DivideByZeroException)
        {
            ResultEntry.Text = "Cannot divide by 0";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}