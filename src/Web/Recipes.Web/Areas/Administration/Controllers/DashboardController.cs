﻿namespace Recipes.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Recipes.Services.Data.Contracts;
    using Recipes.Web.ViewModels.Administration.Dashboard;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;

        public DashboardController(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
            return this.View(viewModel);
        }
    }
}
