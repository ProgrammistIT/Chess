using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;

namespace Chess;

public partial class GamePage : ContentPage
{
    public GamePage()
    {
        InitializeComponent();
    }

    private async void Back_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}