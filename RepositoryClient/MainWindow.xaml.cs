﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RepositoryClient.Models;
using System.IO;

namespace RepositoryClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<RepositoryMetadata> metadatas;
        public MainWindow()
        {
            InitializeComponent();
            repo_list.ItemsSource = get_repo_list();
        }

        [DllImport("../../../Debug/ApplicationInterfaces.dll",EntryPoint = "sendRequest", CallingConvention = CallingConvention.Cdecl)]
        //public static extern class CApplicationInterfaces;
        public static extern void sendRequest(string s, StringBuilder b, int length);

        private void label_Loaded(object sender, RoutedEventArgs e)
        {
            //dynamic request = new {
            //    resource = "test",
            //    formData = new  Object[]{ new { key = "123", value = "value" } },
            //    files = new string[] { "12345"}

            //};
            //var rS = JsonConvert.SerializeObject(request);
            //var result = new StringBuilder(100000);

            ////sendRequest(rS, result, 100000);
            //label.Content =  result;
            //var s = result.ToString();
            //dynamic obj = JsonConvert.DeserializeObject(s);
            ////label.Content = ApplicationInterfaces.Class1.Add(1, 2);
        }

        private void upload_btn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();
            label.Content = dialog.SelectedPath;
            if (String.IsNullOrEmpty(dialog.SelectedPath)) return;  // If use cancel selection then return
            List<String> files = Directory.GetFiles(dialog.SelectedPath, "*.cpp").ToList();
            files.AddRange(Directory.GetFiles(dialog.SelectedPath, "*.h"));
            dynamic request = new
            {
                Resource = "/repository/checkin",
                FormData = new Object[] { new { key = "ModuleName", value = new DirectoryInfo(dialog.SelectedPath).Name } },
                Files = files.ToArray()
            };

            var rS = JsonConvert.SerializeObject(request);
            var result = new StringBuilder(100000);
            sendRequest(rS, result, 100000);
            repo_list.ItemsSource = get_repo_list();
        }

        private void list_btn_Click(object sender, RoutedEventArgs e)
        {
            repo_list.ItemsSource = get_repo_list();
        }

        private List<RepositoryMetadata> get_repo_list(){
            List<RepositoryMetadata> metadatas = new List<RepositoryMetadata>();
            dynamic request = new
            {
                Resource = "/repository/list",
                FormData = new Object[] { },
                Files = new string[] { }

            };

            var rS = JsonConvert.SerializeObject(request);
            //sendRequest(rS, result, 100000);
            var result = new StringBuilder(100000);
            sendRequest(rS, result, 100000);
            var resultString = result.ToString();
            //List<dynamic> jsonResult = JsonConvert.DeserializeObject<List<dynamic>>(resultString);
            List<RepositoryMetadata> jsonResult = JsonConvert.DeserializeObject<List<RepositoryMetadata>>(resultString);
            foreach (var o in jsonResult)
            {
                metadatas.Add(o);
            }
            this.metadatas = metadatas;
            return metadatas;
        }

        private void list_btn_Copy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void view_detail_btn_Click(object sender, RoutedEventArgs e)
        {
            if(repo_list.SelectedItems.Count == 0){
                MessageBox.Show(this, "Please select one record in the list below.", "Cannot get detail");
                return;
            }
            DetailWindow detailWindow = new DetailWindow((RepositoryMetadata)repo_list.SelectedItems[0]);
            detailWindow.ShowDialog();
        }
    }


}
