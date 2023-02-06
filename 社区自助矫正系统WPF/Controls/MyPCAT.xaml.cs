using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace 社区自助矫正系统WPF.Controls
{
    /// <summary>
    /// MyPCAT.xaml 的交互逻辑
    /// </summary>
    public partial class MyPCAT : UserControl
    {
        public delegate void PCADataDelegate(string provinceCode, string cityCode, string areaCode, string townCode, string provinceName, string cityName, string areaName, string townName);
        public event PCADataDelegate PCAData_enent;
        private string provinceName { get; set; }
        private string cityName { get; set; }
        private string areaName { get; set; }
        private string townName { get; set; }

        private string provinceCode { get; set; }
        private string cityCode { get; set; }
        private string areaCode { get; set; }
        private string townCode { get; set; }
        List<Province> provinces;
        List<City> cities;
        List<Area> areas;
        List<Town> towns;
        public void init()
        {
            provinces = JsonConvert.DeserializeObject<List<Province>>(App.provString);
            cities = JsonConvert.DeserializeObject<List<City>>(App.cityString);
            areas = JsonConvert.DeserializeObject<List<Area>>(App.areaString);
            towns = JsonConvert.DeserializeObject<List<Town>>(App.townString);

            provinces.Insert(0, new Province() { areaName = "--请选择省--", parentId = "-1" });
            this.com_pro.ItemsSource = provinces.ToList();

            this.com_pro.DisplayMemberPath = "areaName";
            this.com_pro.SelectedValuePath = "areaId";
            this.com_pro.SelectedIndex = 0;
            //this.com_city.Items.Add("--请选择市--");
            //this.com_area.Items.Add("--请选择区县--");
            //this.com_town.Items.Add("--请选择乡镇--");

        }
        public MyPCAT()
        {
            InitializeComponent();
        }
        class Province
        {
            public int idint { get; set; }
            public string areaId { get; set; }
            public string areaName { get; set; }
            public string areaCode { get; set; }
            public string areaLevel { get; set; }
            public string zxs { get; set; }
            public string zgx { get; set; }
            public int childCount { get; set; }
            public string parentId { get; set; }
            public string id { get; set; }
        }
        class City
        {
            public string areaId { get; set; }
            public string areaName { get; set; }
            public string areaCode { get; set; }
            public string areaLevel { get; set; }
            public string zxs { get; set; }
            public string zgx { get; set; }
            public string parentId { get; set; }
            public string id { get; set; }
        }

        class Area
        {
            public string areaId { get; set; }
            public string areaName { get; set; }
            public string areaCode { get; set; }
            public string areaLevel { get; set; }
            public string zxs { get; set; }
            public string zgx { get; set; }

            public string parentId { get; set; }
            public string id { get; set; }
        }
        class Town
        {
            public string areaId { get; set; }
            public string areaName { get; set; }
            public string areaCode { get; set; }
            public string areaLevel { get; set; }
            public string zxs { get; set; }
            public string zgx { get; set; }
            public string parentId { get; set; }
            public string id { get; set; }
        }

        private void com_pro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (com_pro.SelectedIndex == -1)
            {
                return;
            }
            if (com_pro.SelectedIndex != 0)
            {
                string province_code = com_pro.SelectedValue.ToString();
                this.provinceCode = province_code;


                this.provinceName = (com_pro.SelectedItem as Province).areaName.ToString();

                List<City> cits = cities.Where(cit => cit.parentId == province_code).ToList<City>();
                cits.Insert(0, new City() { areaName = "--请选择市--", parentId = "-1" });
                this.com_city.ItemsSource = cits;
                this.com_city.DisplayMemberPath = "areaName";
                this.com_city.SelectedValuePath = "areaId";
                this.com_city.SelectedIndex = 0;
            }
        }

        private void com_city_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (com_city.SelectedIndex == -1)
            {
                return;
            }
            if (com_city.SelectedIndex != 0)
            {

                string city_code = com_city.SelectedValue.ToString();
                this.cityCode = city_code;
                this.cityName = (com_city.SelectedItem as City).areaName.ToString();

                List<Area> ares = areas.Where(re => re.parentId == city_code).ToList<Area>();
                ares.Insert(0, new Area() { areaName = "--请选择县/区--", parentId = "-1" });
                this.com_area.ItemsSource = ares;
                this.com_area.DisplayMemberPath = "areaName";
                this.com_area.SelectedValuePath = "areaId";
                this.com_area.SelectedIndex = 0;
            }
        }

        private void com_area_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (com_area.SelectedIndex == -1)
            {
                return;
            }
            if (com_area.SelectedIndex != 0)
            {
                string area_code = com_area.SelectedValue.ToString();
                this.areaCode = area_code;
                this.areaName = (com_area.SelectedItem as Area).areaName.ToString();

                List<Town> twns = towns.Where(re => re.parentId == area_code).ToList<Town>();
                twns.Insert(0, new Town() { areaName = "--请选择乡镇--", parentId = "-1" });
                this.com_town.ItemsSource = twns;
                this.com_town.DisplayMemberPath = "areaName";
                this.com_town.SelectedValuePath = "areaId";
                this.com_town.SelectedIndex = 0;
            }
        }
        private void com_town_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (com_town.SelectedIndex == -1)
                {
                    return;
                }
                Town etxt = (Town)com_town.SelectedItem;
                if (etxt.areaName == "--请选择乡镇--")
                {
                    this.townCode = "";
                    this.townName = "";
                    PCAData_enent(provinceCode, cityCode, areaCode, townCode, provinceName, cityName, areaName, townName);
                    return;
                }
                if (com_town.SelectedIndex != 0)
                {
                    this.townCode = com_town.SelectedValue.ToString();
                    this.townName = (com_town.SelectedItem as Town).areaName.ToString();

                    PCAData_enent(provinceCode, cityCode, areaCode, townCode, provinceName, cityName, areaName, townName);
                }
            }
            catch (Exception)
            {

                return;
            }

        }
    }
}
