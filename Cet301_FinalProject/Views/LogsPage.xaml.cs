using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cet301_FinalProject.Models;
using Cet301_FinalProject.Services;

namespace Cet301_FinalProject.Views;

public partial class LogsPage : ContentPage
{
    private LocalDBService _dbService = new LocalDBService();
    public LogsPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var logs = await _dbService.GetOperationLog();
        Logs.ItemsSource = logs.OrderByDescending(log => log.Date).ToList();
    }

    public async void FilterLog(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var type = button.CommandParameter.ToString();

        var allLogs = await _dbService.GetOperationLog();
        var filteredLogs = allLogs.Where(x => x.OperationType == type).ToList();
        Logs.ItemsSource = filteredLogs.OrderByDescending(log => log.Date).ToList();
    }
}