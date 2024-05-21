﻿// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Windows;
using System.Windows.Controls;
using ProjectsEditor.Models;
using ProjectsEditor.ViewModels;

namespace ProjectsEditor
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private MainViewModel _mainViewModel;

        public MainPage(MainViewModel mainViewModel)
        {
            InitializeComponent();
            _mainViewModel = mainViewModel;
            this.DataContext = _mainViewModel;
        }

        private /*async*/ void NewProjectButton_Click(object sender, RoutedEventArgs e)
        {
            _mainViewModel.AddNewProject();
        }

        private void EditButtonClicked(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Project selectedProject = button.DataContext as Project;
            _mainViewModel.EditProject(selectedProject);
        }

        private void DeleteButtonClicked(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            Button button = sender as Button;
            Project selectedProject = button.DataContext as Project;
            _mainViewModel.DeleteProject(selectedProject);
        }

        private void MoreButton_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            Button button = sender as Button;
            Project project = button.DataContext as Project;
            project.IsPopupVisible = true;
        }

        private void LaunchButton_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            Button button = sender as Button;
            Project project = button.DataContext as Project;
            _mainViewModel.LaunchProject(project);
        }
    }
}
