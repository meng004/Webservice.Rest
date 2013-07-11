using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace WebService.Rest.WpfClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        #region 属性

        /// <summary>
        /// 主机地址
        /// </summary>
        const string _baseAddress = "http://localhost:1206/";
        /// <summary>
        /// 请求Uri
        /// </summary>
        const string _provinceRequestUri = "api/Provinces";
        const string _cityRequestUri = "api/Cities";
        const string _districtRequestUri = "api/Districts";
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            //同步调用
            //SetSync();
            //异步调用
            //SetAsync();
        }

        #region 加载策略

        /// <summary>
        /// 同步处理
        /// </summary>
        private void SetSync()
        {
            cmbProvince.SelectionChanged += cmbProvince_SelectionChanged;
            cmbCity.SelectionChanged += cmbCity_SelectionChanged;
            SetProvinces();
            cmbProvince.SelectedIndex = 0;
        }

        /// <summary>
        /// 异步处理
        /// </summary>
        private void SetAsync()
        {
            cmbProvince.SelectionChanged += cmbProvince_SelectionChangedAsync;
            cmbCity.SelectionChanged += cmbCity_SelectionChangedAsync;
            SetProvincesAsync();
            cmbProvince.SelectedIndex = 0;
        }

        #endregion

        #region 数据绑定 同步

        /// <summary>
        /// 取省份
        /// RestSharp 同步调用
        /// </summary>
        /// <returns></returns>
        private void SetProvinces()
        {
            //创建RestClient，指定主机地址
            var client = new RestClient(_baseAddress);
            //创建请求，指定请求地址、Http Method、数据传输格式
            var request = new RestRequest(_provinceRequestUri, Method.GET);
            request.AddHeader("Accept", "application/json");
            //执行请求
            var response = client.Execute(request);
            //返回结果
            var array = JArray.Parse(response.Content).OrderBy(t => t["ProSort"]);
            var data = array.Select(t =>
                new
                {
                    ProvinceId = t["ProvinceId"],
                    ProName = t["ProName"]
                });
            //绑定
            if (data.Count() > 0)
            {
                cmbProvince.ItemsSource = data;
            }
            else
            {
                cmbProvince.Items.Clear();
                cmbProvince.Items.Add(new { ProName = "没有数据" });
            }
        }

        /// <summary>
        /// 取城市
        /// 同步调用
        /// </summary>
        /// <param name="provinceId"></param>
        private void SetCityByProvinceId(int provinceId)
        {
            //创建RestClient，指定主机地址
            var client = new RestClient(_baseAddress);
            //创建请求，指定请求地址、Http Method、数据传输格式
            var request = new RestRequest(String.Format("{0}/{1}", _cityRequestUri, provinceId), Method.GET);
            request.AddHeader("Accept", "application/json");
            //执行请求
            var response = client.Execute(request);
            //返回结果
            var array = JArray.Parse(response.Content).OrderBy(t => t["CitySort"]);
            var data = array.Select(t =>
                new
                {
                    CityId = t["CityId"],
                    CityName = t["CityName"]
                });
            //绑定
            if (data.Count() > 0)
            {
                cmbCity.ItemsSource = data;
            }
            else
            {
                cmbCity.Items.Clear();
                cmbCity.Items.Add(new { CityName = "没有数据" });
            }
        }

        /// <summary>
        /// 取县区
        /// 同步调用
        /// </summary>
        /// <param name="cityId"></param>
        private void SetDistrictByCityId(int cityId)
        {
            //创建RestClient，指定主机地址
            var client = new RestClient(_baseAddress);
            //创建请求，指定请求地址、Http Method、数据传输格式
            var request = new RestRequest(String.Format("{0}/{1}", _districtRequestUri, cityId), Method.GET);
            request.AddHeader("Accept", "application/json");
            //执行请求
            var response = client.Execute(request);
            //返回结果
            var array = string.IsNullOrWhiteSpace(response.Content) ? new JArray() : JArray.Parse(response.Content);
            var data = array.Select(t =>
                new
                {
                    DistrictId = t["DistrictId"],
                    DisName = t["DisName"]
                });
            //绑定
            if (data.Count() > 0)
            {
                cmbDistrict.ItemsSource = data;
            }
            else
            {
                cmbDistrict.Items.Clear();
                cmbDistrict.Items.Add(new { DisName = "没有数据" });
            }
        }
        #endregion

        #region 数据绑定 异步

        /// <summary>
        /// 取省份
        /// </summary>
        /// <returns></returns>
        private void SetProvincesAsync()
        {
            //创建RestClient，指定主机地址
            var client = new RestClient(_baseAddress);
            //创建请求，指定请求地址、Http Method、数据传输格式
            var request = new RestRequest(_provinceRequestUri, Method.GET);
            request.AddHeader("Accept", "application/json");
            //执行请求
            client.ExecuteAsync(request, response =>
            {
                //返回结果
                var array = JArray.Parse(response.Content).OrderBy(t => t["ProSort"]);
                var data = array.Select(t =>
                    new
                    {
                        ProvinceId = t["ProvinceId"],
                        ProName = t["ProName"]
                    });
                //绑定                    
                cmbProvince.Dispatcher.
                    BeginInvoke(new Action(() =>
                    {
                        if (data.Count() > 0)
                        {
                            cmbProvince.ItemsSource = data;
                        }
                        else
                        {
                            cmbProvince.Items.Clear();
                            cmbProvince.Items.Add(new { ProName = "没有数据" });
                        }
                    }));
            });
        }

        /// <summary>
        /// 取城市
        /// </summary>
        /// <param name="provinceId"></param>
        private void SetCityByProvinceIdAsync(int provinceId)
        {
            //创建RestClient，指定主机地址
            var client = new RestClient(_baseAddress);
            //创建请求，指定请求地址、Http Method、数据传输格式
            var request = new RestRequest(String.Format("{0}/{1}", _cityRequestUri, provinceId), Method.GET);
            request.AddHeader("Accept", "application/json");
            //执行请求
            client.ExecuteAsync(request, response =>
                {
                    //返回结果
                    var array = JArray.Parse(response.Content).OrderBy(t => t["CitySort"]);
                    var data = array.Select(t =>
                        new
                        {
                            CityId = t["CityId"],
                            CityName = t["CityName"]
                        });
                    //绑定
                    cmbCity.Dispatcher.BeginInvoke(new Action(() =>
                                                   {
                                                       if (data.Count() > 0)
                                                       {
                                                           cmbCity.ItemsSource = data;
                                                       }
                                                       else
                                                       {
                                                           cmbCity.Items.Clear();
                                                           cmbCity.Items.Add(new { CityName = "没有数据" });
                                                       }
                                                   }));
                });

        }

        /// <summary>
        /// 取县区
        /// </summary>
        /// <param name="cityId"></param>
        private void SetDistrictByCityIdAsync(int cityId)
        {
            //创建RestClient，指定主机地址
            var client = new RestClient(_baseAddress);
            //创建请求，指定请求地址、Http Method、数据传输格式
            var request = new RestRequest(String.Format("{0}/{1}", _districtRequestUri, cityId), Method.GET);
            request.AddHeader("Accept", "application/json");
            //执行请求
            client.ExecuteAsync(request, response =>
                {
                    //返回结果
                    var array = string.IsNullOrWhiteSpace(response.Content) ? new JArray() : JArray.Parse(response.Content);
                    var data = array.Select(t =>
                        new
                        {
                            DistrictId = t["DistrictId"],
                            DisName = t["DisName"]
                        });
                    //绑定
                    cmbDistrict.Dispatcher.BeginInvoke(new Action(() =>
                                                       {
                                                           if (data.Count() > 0)
                                                           {
                                                               cmbDistrict.ItemsSource = data;
                                                           }
                                                           else
                                                           {
                                                               cmbDistrict.Items.Clear();
                                                               cmbDistrict.Items.Add(new { DisName = "没有数据" });
                                                           }
                                                       }));
                });
        }
        #endregion

        #region 事件处理 同步

        private void cmbProvince_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = cmbProvince.SelectedValue as JToken;
            if (item != null)
            {
                SetCityByProvinceId(item.Value<int>());
                cmbCity.SelectedIndex = 0;
            }
            else
            {
                cmbCity.ItemsSource = null;
            }
        }

        private void cmbCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = cmbCity.SelectedValue as JToken;
            if (item != null)
            {
                SetDistrictByCityId(item.Value<int>());
                cmbDistrict.SelectedIndex = 0;
            }
            else
            {
                cmbDistrict.ItemsSource = null;
            }
        }
        #endregion

        #region 事件处理 异步

        private void cmbProvince_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {
            var item = cmbProvince.SelectedValue as JToken;
            if (item != null)
            {
                SetCityByProvinceIdAsync(item.Value<int>());
                cmbCity.SelectedIndex = 0;
            }
            else
            {
                cmbCity.ItemsSource = null;
            }
        }

        private void cmbCity_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {
            var item = cmbCity.SelectedValue as JToken;
            if (item != null)
            {
                SetDistrictByCityIdAsync(item.Value<int>());
                cmbDistrict.SelectedIndex = 0;
            }
            else
            {
                cmbDistrict.ItemsSource = null;
            }
        }
        #endregion

        #region 策略选择

        private void rdoAsync_Click(object sender, RoutedEventArgs e)
        {
            //var state = (bool)rdoAsync.IsChecked;
            //if (state)
            //{
            //    SetAsync();

            //}
            //else
            //{
            //    SetSync();

            //}
            SetAsync();
        }

        private void rdoSync_Click(object sender, RoutedEventArgs e)
        {
            //var state = (bool)rdoSync.IsChecked;
            //if (state)
            //{
            //    SetSync();

            //}
            //else
            //{
            //    SetAsync();

            //}
            SetSync();
        }
        #endregion

    }
}
